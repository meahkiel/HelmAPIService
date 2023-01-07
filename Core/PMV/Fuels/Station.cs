namespace Core.PMV.Fuels;

public class Station
{
    public string Code { get; set; }
    public string Description { get; set; }

    public float OpeningMeter { get; set; }
    public float OpeningBalance { get; set; }

    public string StationType { get; set; }
}
