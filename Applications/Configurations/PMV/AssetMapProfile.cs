using Applications.UseCases.PMV.Assets.DTO;
using AutoMapper;
using Core.PMV.Assets;

namespace Applications.Configurations.PMV;

public class AssetMapProfile : Profile
{
	public AssetMapProfile()
	{
		
		this.CreateMap<ServiceLog,ServiceLogResponse>();

	}
}
