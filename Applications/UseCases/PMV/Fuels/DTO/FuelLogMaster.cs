using Applications.UseCases.Common;
using Core.PMV.Assets;
using Core.PMV.Fuels;

namespace Applications.UseCases.PMV.Fuels.DTO;

public class FuelLogReportResult {
    public IEnumerable<Station>? StationSelections {get;set;} = null;
    public IEnumerable<Asset>? AssetSelections { get; set; } = null;
    public IEnumerable<FuelLogMaster> Masters { get; set; } = new List<FuelLogMaster>();
    public IEnumerable<FuelLogEffeciencyList> EffeciencyLists { get; set; } = new List<FuelLogEffeciencyList>();
}

public class FuelLogMaster
{

    public Guid Id { get; set; }
    public string Station { get; set; } = "";
    public string StationType { get; set; }
    public int ReferenceNo { get; set; }
    public int DocumentNo { get; set; }
    public DateTime FueledDate { get; set; } = DateTime.Today;
    public DateTime? ShiftStartTime { get; set; } 
    public DateTime? ShiftEndTime { get; set; }
    public int StartShiftTankerKm { get; set; } = 0;
    public int? EndShiftTankerKm { get; set; } = 0;
    public float OpeningMeter { get; set; }
    public float TotalDispense { get; set; }
    public float TotalRestock { get; set; }
    public float TotalAdjustment { get; set; }

    public string Location { get; set; } = "";

    public string? Remarks { get; set; } = null;
    public string Fueler { get; set; } = null!;
    public bool IsPosted { get; set; }
    public DateTime? PostedAt { get; set; }
    public IList<FuelDetailMaster> FuelTransactions { get; set; } = new List<FuelDetailMaster>();
    public float ClosingMeter => OpeningMeter + TotalDispense;

    public float OpeningBalance => TotalRestock;

    public float RemainingBalance => (TotalRestock - TotalDispense - TotalAdjustment);
    
}


public class FuelDetailMaster
{   

    public Guid TransactionId { get; set; }
    public Guid FuelLogId { get; set; }
    public string AssetCode { get; set; } = "";

    public DateTime FuelDateTime { get; set; }
    public DateTime FuelDate => FuelDateTime;
    public DateTime FuelTime => FuelDateTime;
    public string? OperatorDriver { get; set; } = "";

    public string Remarks { get; set; }

    public string LogType { get; set; }

    public int PreviousReading { get; set; }
    public int Reading { get; set; } = 0;
    public float Quantity { get; set; } = 0f;
    public string DriverQatarIdUrl { get; set; }
    public float Diff => Reading - PreviousReading;
    public float LH => FuelTransaction.ToLiterPerHour(PreviousReading,Reading,Quantity);
    public float HL => FuelTransaction.ToHourPerLiter(PreviousReading,Reading,Quantity);

}
public class FuelLogEffeciencyList
{

    public string AssetCode { get; set; } = "";
    public string Station { get; set; } = "";
    public string Location { get; set; } = "";
    public DateTime FueledDate { get; set; }
    public DateTime FuelDateTime { get; set; }
    public float Quantity { get; set; }
    public int Reading { get; set; } = 0;
    public int PreviousReading { get; set; } = 0;
    public float Diff => Reading - PreviousReading;
    public float LH => Diff == 0 ? 0 : Quantity / Diff;
    public float HL => Quantity == 0 ? 0 : Diff / Quantity;
    public string LHFormat => LH.ToString("0.00");
    public string HLFormat => HL.ToString("0.00");
    public string Fueler { get; set; }
    
}

public class TankerSummary
{
    public string TankerCode { get; set; }
    public float TotalRefill { get; set; }
    public float TotalDispense { get; set; }

    public int TotalDraft { get; set; }
    public int TotalSubmitted { get; set; }

}

public class FuelLogMasterList {

    public string FuelStation { get; set; }
    public string LogSheetId { get; set; }
    public int DocumentNo { get; set; }
    public int ReferenceNo { get; set; }
    public int TotalDispense { get; set; }
    public int Refill { get; set; }
    public float OpeningMeter { get; set; }
    public float ClosingMeter { get; set; }
    public float OpeningBalance { get; set; }
    public float RemainingBalance { get; set; }
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


