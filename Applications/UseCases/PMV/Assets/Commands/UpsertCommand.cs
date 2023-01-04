using Applications.UseCases.PMV.Assets.DTO;
using Applications.UseCases.PMV.Assets.Events;

namespace Applications.UseCases.PMV.Assets.Commands;

public record UpsertCommand(AssetRequest Request) : IRequest<Result<Unit>>;

public class UpsertCommandHandler : IRequestHandler<UpsertCommand, Result<Unit>>
{
    private readonly IUnitWork _unitWork;
    private readonly IPublisher _publisher;

    public UpsertCommandHandler(IUnitWork unitWork,IPublisher publisher)
    {
        _unitWork = unitWork;
        _publisher = publisher;
    }
    public async Task<Result<Unit>> Handle(UpsertCommand request, CancellationToken cancellationToken)
    {
        try {
            if (request.Request.Id > 0)
            {
                var asset = await _unitWork.Assets.ViewAssetById(request.Request.Id);
                if(asset != null)
                    await _publisher.Publish(new AssetUpsertEvent(asset));
            }
            else {

            }

            return Result.Ok(Unit.Value);
        }
        catch(Exception ex) {
            
            return Result.Fail(ex.Message);
        }

        
    }
}