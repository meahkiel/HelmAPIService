namespace Applications.UseCases.PMV.Services.DTO;

public class StationResponse
{
    public string Code { get; set; }   
    public float CurrentQty { get; set; }
    public float NewQty { get; set; }
    public string StationType { get; set; }
}