namespace Applications.UseCases.PMV.LogSheets.DTO;

public record FuelLogTransactionsResponse
{
    public string LogSheetId { get; init; }
    public int ReferenceNo { get; init; }
    public string RefNoFormatted => ReferenceNo.ToString("0000");
    public string AssetCode { get; init; } = "";
    public string Code { get; init; } = "";
    public string Location { get; init; } = "";
    public string FueledDate { get; init; } = "";
    public string FuelTime { get; init; } = "";
    
    public int StartShiftMeterReading { get; init; }

    public int EndShiftMeterReading { get; init; }

    public int Quantity { get; init; }
    public int Reading { get; init; }
    public int PreviousReading { get; init; }
    public decimal Diff => Reading - PreviousReading;
    public decimal LH => PreviousReading == 0 ? 0m : (Quantity / Diff);
    public decimal HL => PreviousReading == 0 ? 0m : (Diff / Quantity);
    public string LHFormat => LH.ToString("0.00");
    public string HLFormat => HL.ToString("0.00");
    public bool IsPosted { get; init; }
    public DateTime PostedAt { get; init; }
    public string Fueler { get; init; }
}