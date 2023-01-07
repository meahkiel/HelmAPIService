using Applications.UseCases.PMV.Assets.DTO;
using Applications.UseCases.PMV.LogSheets.DTO;
using AutoMapper;
using Core.PMV.Assets;
using Core.PMV.LogSheets;

namespace Applications.Configurations.PMV;

public class PMVMapProfile : Profile
{
	public PMVMapProfile()
	{	
		this.CreateMap<ServiceLog,ServiceLogResponse>()
			.ForMember(s => s.AlertAtKm, o => o.MapFrom(c => c.AlertAtKm))
			.ForMember(s => s.IntervalAtKm, o => o.MapFrom(c => c.IntervalAtKm))
			.ForMember(s => s.Status, o => o.MapFrom(c => c.ServiceStatus))
			.ReverseMap();

		this.CreateMap<LogSheetDetail,LogSheetDetailResponse>()
			.ForMember(s => s.LogSheetId, o => o.MapFrom(c => c.LogSheet.Id));
		this.CreateMap<LogSheet,LogSheetResponse>();
	}
}
