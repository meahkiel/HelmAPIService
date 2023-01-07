namespace Applications.UseCases.PMV.Fuels.DTO;


public class FuelTransactionReport
{

    public string AssetCode { get; set; } = "";
    public string Code { get; set; } = "";
    public string Location { get; set; } = "";
    public DateTime FueledDate { get; set; }
    public DateTime FuelTime { get; set; }
    public float Quantity { get; set; }
    public int Reading { get; set; } = 0;
    public int PreviousReading { get; set; } = 0;
    public float Diff => Reading - PreviousReading;
    public float LH => Diff == 0 ? 0 : Quantity / Diff;
    public float HL => Quantity == 0 ? 0 : Diff / Quantity;
    public string LHFormat => LH.ToString("0.00");
    public string HLFormat => HL.ToString("0.00");

    public string Fueler { get; set; }

    public Guid LogSheetId { get; set; }
    public int ReferenceNo { get; set; }
    public string RefNoFormatted => ReferenceNo.ToString("0000");

    public int StartShiftMeterReading { get; set; }

    public int EndShiftMeterReading { get; set; }

    public string TransactionType { get; set; }

    public string RefillStation { get; set; }

    public bool IsPosted { get; set; }
    public string PostedAt { get; set; }

}

public class TankerSummary
{
    public string TankerCode { get; set; }
    public float TotalRefill { get; set; }
    public float TotalDispense { get; set; }

    public int TotalDraft { get; set; }
    public int TotalSubmitted { get; set; }

}

public class TransactionList
{
    public string LogSheetId { get; set; }
    public string ReferenceNo { get; set; }
    public string AssetCode { get; set; }
    public string FuelingLocation { get; set; }
    public string FueledDate { get; set; }
    public string FueledTime { get; set; }
    public int SMU { get; set; }

    public float Quantity { get; set; }
    public string Fueler { get; set; }
}

public class FuelEfficiency
{
    public string AssetCode { get; set; }
    public int CurrentReading { get; set; }
    public int PreviousReading { get; set; }
    public float Diff { get; set; }
    public float Quantity { get; set; }

    public string LPerKm { get; set; }
    public string KmPerL { get; set; }


}



public record FuelTransactionRequest
{
    private readonly IEnumerable<FuelTransactionReport> _reports;

    public FuelTransactionRequest(IEnumerable<FuelTransactionReport> reports)
    {
        _reports = reports;

        Lists = new List<TransactionList>();
        EffeciencyReports = new List<FuelEfficiency>();

        foreach (var report in reports)
        {
            Lists.Add(new TransactionList
            {
                LogSheetId = report.LogSheetId.ToString(),
                ReferenceNo = report.RefNoFormatted,
                AssetCode = report.AssetCode,
                FuelingLocation = report.Location,
                FueledDate = report.FuelTime.ToLongDateString(),
                FueledTime = report.FuelTime.ToLongTimeString(),
                SMU = report.Reading,
                Quantity = report.Quantity,
                Fueler = report.Fueler
            });

            EffeciencyReports.Add(new FuelEfficiency
            {
                AssetCode = report.AssetCode,
                CurrentReading = report.Reading,
                PreviousReading = report.PreviousReading,
                Diff = report.Diff,
                LPerKm = report.LHFormat,
                KmPerL = report.HLFormat,
                Quantity = report.Quantity
            });
        }


        Summaries = reports.GroupBy(r => r.Code)
                    .OrderBy(r => r.Key)
                    .Select(r =>
                        new TankerSummary
                        {
                            TankerCode = r.Key,
                            TotalDraft = reports.Where(r => r.IsPosted == false).GroupBy(l => l.ReferenceNo).Count(),
                            TotalSubmitted = reports.Where(r => r.IsPosted == true).GroupBy(l => l.ReferenceNo).Count(),
                            TotalDispense = reports.Where(d => d.IsPosted && d.Code == r.Key && d.TransactionType == "Dispense")
                                            .Sum(d => d.Quantity),
                            TotalRefill = reports.Where(d => d.IsPosted && d.AssetCode == r.Key && d.TransactionType == "Restock")
                                            .Sum(d => d.Quantity)
                        }
                    ).ToList();

    }

    public IList<TransactionList> Lists { get; init; }
    public IList<FuelEfficiency> EffeciencyReports { get; init; }
    public IList<TankerSummary> Summaries { get; init; }

}