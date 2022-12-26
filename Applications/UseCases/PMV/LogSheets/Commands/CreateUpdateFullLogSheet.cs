namespace Applications.UseCases.PMV.LogSheets.Commands;

public record CreateUpdateFullLogSheetRequest(LogSheetRequest SheetRequest) : IRequest<Result<LogSheetRequest>>;


public class CreateUpdateFullLogSheetValidator : AbstractValidator<LogSheetRequest>
{

}

public class CreateUpdateFullLogSheetRequestHandler : IRequestHandler<CreateUpdateFullLogSheetRequest, Result<LogSheetRequest>>
{
    private readonly IUnitWork _unitWork;
    private readonly IValidator<LogSheetRequest> _validator;
    public CreateUpdateFullLogSheetRequestHandler(IUnitWork unitWork, IValidator<LogSheetRequest> validator)
    {
        _validator = validator;
        _unitWork = unitWork;

    }
    public Task<Result<LogSheetRequest>> Handle(CreateUpdateFullLogSheetRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
