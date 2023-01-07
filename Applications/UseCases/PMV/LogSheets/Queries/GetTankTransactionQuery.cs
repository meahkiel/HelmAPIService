using Applications.UseCases.PMV.LogSheets.DTO;
using AutoMapper;

namespace Applications.UseCases.PMV.LogSheets.Queries;

public record GetTankTransactionQuery(string Station,DateTime DateFrom,DateTime DateTo) : IRequest<Result<IEnumerable<LogSheetResponse>>>;

public class GetTankTransactionQueryHandler : IRequestHandler<GetTankTransactionQuery, Result<IEnumerable<LogSheetResponse>>>
{


    private readonly IUnitWork _unitWork;
    private readonly IMapper _mapper;

    public GetTankTransactionQueryHandler(IUnitWork unitWork, IMapper mapper)
    {
        _unitWork = unitWork;
        _mapper = mapper;
    }
    public async Task<Result<IEnumerable<LogSheetResponse>>> Handle(GetTankTransactionQuery request, CancellationToken cancellationToken)
    {
        try {
            var results = await _unitWork.GetContext()
                                    .LogSheets
                                    .Include(l => l.Details)
                                    .Where(l => l.StationCode == request.Station && 
                                    l.ShiftStartTime >= request.DateFrom && l.ShiftEndTime <= request.DateTo)
                                    .ToListAsync();

            return Result.Ok(_mapper.Map<IEnumerable<LogSheetResponse>>(results));

        }
        catch(Exception ex) {
            return Result.Fail(ex.Message);
        }
    }
}
