using Applications.UseCases.PMV.Assets.DTO;
using Applications.UseCases.PMV.Fuels.DTO;
using Applications.UseCases.PMV.LogSheets.DTO;
using AutoMapper;
using Core.PMV.Assets;

namespace Applications.UseCases.PMV.Assets.Queries;

public record ViewAssetQuery(int Id) : IRequest<Result<AssetViewResponse>>;

public class ViewAssetQueryHandler : 
    IRequestHandler<ViewAssetQuery, 
        Result<AssetViewResponse>>
{
    
    private readonly IUnitWork _unitWork;
        private readonly IMapper _mapper;

    public ViewAssetQueryHandler(IUnitWork unitWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitWork = unitWork;
    }

    public async Task<Result<AssetViewResponse>> Handle(ViewAssetQuery request, 
        CancellationToken cancellationToken)
    {
        try
        {
            var viewAsset = await _unitWork.Assets.ViewAssetById(request.Id);

            if (viewAsset == null)
                throw new Exception("Cannot find asset");

            //get asset servicelogs
            var serviceLogs = await _unitWork.GetContext()
                                            .ServiceLogs.Where(a => a.AssetId == viewAsset.Id).ToListAsync();
            
            viewAsset.ServiceLogs = _mapper.Map<IEnumerable<ServiceLogResponse>>(serviceLogs ?? new List<ServiceLog>());

            //get fuel history
            viewAsset.LogSheets = await _unitWork.LogSheets.GetPostedTransactionsByAsset(viewAsset.AssetCode!) 
                                            ?? new List<FuelLogTransactionsResponse>();
            
            return Result.Ok(viewAsset);
        }
        catch(Exception ex) {

            return Result.Fail(ex.Message);
        }
    }
}

