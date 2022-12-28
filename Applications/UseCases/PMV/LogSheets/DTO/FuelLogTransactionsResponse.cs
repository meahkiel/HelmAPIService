namespace Applications.UseCases.PMV.LogSheets.DTO;

public record FuelLogTransactionsResponse
{
    public Guid LogSheetId { get; set; }
    public int ReferenceNo { get; set; }
    public string RefNoFormatted => ReferenceNo.ToString("0000");
    public string AssetCode { get; set; } = "";
    public string Code { get; set; } = "";
    public string Location { get; set; } = "";
    public string FueledDate { get; set; } = "";
    public string FuelTime { get; set; } = "";
    
    public int StartShiftMeterReading { get; set; }

    public int EndShiftMeterReading { get; set; }

    public int Quantity { get; set; }
    public int Reading { get; set; } = 0;
    public int PreviousReading { get; set; } = 0;
    public decimal Diff => Reading - PreviousReading;
    public decimal LH => Diff == 0 ? 0m : (Quantity / Diff);
    public decimal HL => Quantity == 0 ? 0m : (Diff / Quantity);
    public string LHFormat => LH.ToString("0.00");
    public string HLFormat => HL.ToString("0.00");
    public bool IsPosted { get; set; }
    public string PostedAt { get; set; }
    public string Fueler { get; set; }
}