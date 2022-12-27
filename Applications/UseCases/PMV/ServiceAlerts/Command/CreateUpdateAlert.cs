using Applications.UseCases.PMV.ServiceAlerts.DTO;
using Core.PMV.Alerts;

namespace Applications.UseCases.PMV.ServiceAlerts.Command;

public record CreateUpdateAlertRequest(ServiceAlertRequest AlertRequest) : IRequest<Result<Unit>>;


public class CreateUpdateAlertValidator : AbstractValidator<ServiceAlertRequest> {
    
    private readonly IUnitWork _unitWork;

    public CreateUpdateAlertValidator(IUnitWork unitWork)
    {
            _unitWork = unitWork;

            RuleFor(r => r.GroupName).MustAsync(async (groupName,cancellation) => {
                //check if the group is exist
                return await _unitWork.ServiceAlert.IsGroupExists(groupName); 
            }).WithMessage("Group Name must be unique");
    }
}

public class CreateUpdateAlertRequestHandler : IRequestHandler<CreateUpdateAlertRequest, Result<Unit>>
{
    private readonly IUnitWork _unitWork;
    private readonly IValidator<ServiceAlertRequest> _validator;
    
    public CreateUpdateAlertRequestHandler(IUnitWork unitWork, IValidator<ServiceAlertRequest> validator)
    {
        _validator = validator;
        _unitWork = unitWork;
    }
    
    public async Task<Result<Unit>> Handle(CreateUpdateAlertRequest request, CancellationToken cancellationToken)
    {
        try {

            ServiceAlert? alert = null;
            
            if(!string.IsNullOrEmpty(request.AlertRequest.Id)) {
                
                alert = await _unitWork.ServiceAlert.GetByIdAsync(Guid.Parse(request.AlertRequest.Id));
                if(alert == null) throw new Exception("Alert not found");
                foreach(var detail in request.AlertRequest.Details) {
                    if(detail.MarkIsDelete) {
                        alert.RemoveDetail(detail.Id);
                    }
                    else {
                        alert.UpdateDetail(detail.Id,detail.ServiceCode,detail.KmAlert,detail.KmInterval,"H22095411");
                    }
                }

                _unitWork.ServiceAlert.Update(alert);
                
            }
            else {
                //check the same
                var validation = await _validator.ValidateAsync(request.AlertRequest);
                
                if(!validation.IsValid) {
                    return Result.Fail(validation.Errors[0].ToString());
                }
                
                alert = new ServiceAlert(request.AlertRequest.GroupName,request.AlertRequest.Description);

                if(request.AlertRequest.Details != null && request.AlertRequest.Details.Count() > 0) {
                    
                    foreach(var alertDetail in request.AlertRequest.Details) {
                        alert.AddDetail(alertDetail.ServiceCode,alertDetail.KmAlert,alertDetail.KmInterval,"H22095411");
                    }
                }

                _unitWork.ServiceAlert.Add(alert);
            }
            
            await _unitWork.CommitSaveAsync();

            return Result.Ok(Unit.Value);

        }
        catch(Exception ex) {
            return Result.Fail(ex.Message);
        }

    }
}
