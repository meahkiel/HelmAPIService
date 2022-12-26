namespace Core.PMV.Stations;

public class LVStation
{
    public string Code { get; set; } = "";

    public DateTime FuelDate { get; set; } = DateTime.Now;
    public string FuelStation { get; set; } = "";
    public string FueledTo { get; set; } = "";
    public string Operator { get; set; } = "";
    public float CurrentFuelQuantity { get; set; } = 0;
    public float PreviousFuelQuantity { get; set; } = 0;
    
}