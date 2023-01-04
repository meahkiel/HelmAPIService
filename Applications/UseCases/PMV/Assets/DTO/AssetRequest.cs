namespace Applications.UseCases.PMV.Assets.DTO;

public class AssetRequest
{
    public int Id { get; set; } = 0;
    public int CategoryId { get; set; }
    public string SubCategory { get; set; }
    public string AssetDesc { get; set; }


}