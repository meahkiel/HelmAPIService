using Applications.UseCases.Common;
using Applications.UseCases.PMV.Common;

namespace Applications.UseCases.PMV.Fuels.Queries;


/// <summary>
/// Set an initial value whenever the form is created
/// prepare the lookup value 
///     station
///     
/// </summary>



public class InitialValue
{
	public InitialValue()
	{

	}
	public IEnumerable<SelectItem> Stations { get; set; } = new List<SelectItem>();
	public IEnumerable<SelectItem> Assets { get; set; } = new List<SelectItem>();
} 

public record GetInitialValueQuery : IRequest<Result<InitialValue>>;
public class GetInitialValueQueryHandler : IRequestHandler<GetInitialValueQuery, Result<InitialValue>>
{
    private readonly ICommonService _commonService;

    public GetInitialValueQueryHandler(ICommonService commonService)
	{
        _commonService = commonService;
    }
    
    public async Task<Result<InitialValue>> Handle(GetInitialValueQuery request, CancellationToken cancellationToken)
    {
        try
        {

            InitialValue initial = new InitialValue();

            var stations = await _commonService.GetAllStation();
            
            initial.Stations = stations.Select(p => new SelectItem
            {
                Value = p.Code,
                Text = p.Description
            });
            initial.Assets = await _commonService.GetAssetLookup();

            return Result.Ok(initial);
        }
        catch(Exception ex)
        {
            return Result.Fail(ex.Message);
        }


    }
}
