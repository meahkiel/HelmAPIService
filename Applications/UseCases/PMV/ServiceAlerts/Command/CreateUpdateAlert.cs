using Applications.UseCases.PMV.ServiceAlerts.DTO;
using Core.PMV.Alerts;

namespace Applications.UseCases.PMV.ServiceAlerts.Command;

public record CreateUpdateAlertRequest(ServiceAlertRequest AlertRequest) : IRequest<Result<Unit>>;


public class CreateUpdateAlertValidator : AbstractValidator<ServiceAlertRequest>
{

    private readonly IUnitWork _unitWork;

    public CreateUpdateAlertValidator(IUnitWork unitWork)
    {
        _unitWork = unitWork;

        RuleFor(r => r.GroupName).MustAsync(async (groupName, cancellation) =>
        {
            //check if the group is exist
            return (await _unitWork.ServiceAlert.IsGroupExists(groupName)) == true ? false : true;
            
        }).WithMessage("Group Name must be unique");
    }
}

public class CreateUpdateAlertRequestHandler : IRequestHandler<CreateUpdateAlertRequest, Result<Unit>>
{
    private readonly IUnitWork _unitWork;
    private readonly IValidator<ServiceAlertRequest> _validator;
    private readonly IUserAccessor _userAccessor;

    public CreateUpdateAlertRequestHandler(IUnitWork unitWork,
    IValidator<ServiceAlertRequest> validator, IUserAccessor userAccessor)
    {
        _userAccessor = userAccessor;
        _validator = validator;
        _unitWork = unitWork;
    }

    public async Task<Result<Unit>> Handle(CreateUpdateAlertRequest request, CancellationToken cancellationToken)
    {
        try
        {

            ServiceAlert? alert = null;
            var userEmployeeCode = await _userAccessor.GetUserEmployeeCode();
            if (!string.IsNullOrEmpty(request.AlertRequest.Id))
            {

                alert = await _unitWork.ServiceAlert.GetByIdAsync(Guid.Parse(request.AlertRequest.Id));

                if (alert == null) throw new Exception("Alert not found");
                foreach (var detail in request.AlertRequest.Details)
                {
                    if(string.IsNullOrEmpty(detail.Id)) {
                        alert.AddDetail(detail.ServiceCode, detail.KmAlert, detail.KmInterval, userEmployeeCode);
                    }
                    else
                    {
                        alert.UpdateDetail(detail.Id, detail.ServiceCode, detail.KmAlert, detail.KmInterval,userEmployeeCode);
                    }
                }
                _unitWork.ServiceAlert.Update(alert);
            }
            else
            {
                //check the same
                var validation = await _validator.ValidateAsync(request.AlertRequest);

                if (!validation.IsValid)
                {
                    return Result.Fail(validation.Errors[0].ToString());
                }

                alert = new ServiceAlert(
                    request.AlertRequest.GroupName,
                    request.AlertRequest.Description,
                    request.AlertRequest.Assigned,
                    request.AlertRequest.Categories);

                if (request.AlertRequest.Details != null && request.AlertRequest.Details.Count() > 0)
                {

                    foreach (var alertDetail in request.AlertRequest.Details)
                    {
                        alert.AddDetail(alertDetail.ServiceCode, alertDetail.KmAlert, alertDetail.KmInterval, "H22095411");
                    }
                }

                _unitWork.ServiceAlert.Add(alert);
            }

            await _unitWork.CommitSaveAsync(userEmployeeCode);

            return Result.Ok(Unit.Value);

        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }

    }
}
