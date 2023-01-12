using BaseEntityPack.Core;

namespace Core.PMV.Assets;

public class Asset : AggregateRoot<int>
{
    public Asset() : base(0) {

    }

    public Asset(int Id) : base(Id)
    {
        
    }

    public string AssetCode { get; set; }
    public string AssetDesc { get; set; }
    public string FullDesc { get; set; }
    public string CompanyCode { get; set; }
}