using Applications.UseCases.Common;
using Core.Common;
using Core.PMV.Fuels;

namespace Applications.UseCases.PMV.Fuels.DTO;


public class FuelLogKeyResponse
{
    public string Id { get; set; }
    public int DocumentNo { get; set; }

    public float OpeningMeter { get; set; }
    
}

public class FuelLogDetailKeyResponse
{
    public string Id { get; set; }
    public string LogSheetId { get; set; }
}


public class FuelLogResponse
{

    public string? Id { get; set; } = null;
    public string Station { get; set; } = "";
    public string StationType { get; set; }
    public int ReferenceNo { get; set; }
    public int DocumentNo { get; set; }
    public DateTime FueledDate { get; set; } = DateTime.Today;
    public DateTime? ShiftStartTime { get; set; }  = DateTime.Today;
    public DateTime? ShiftEndTime { get; set; } = DateTime.Today;

    public int StartShiftTankerKm { get; set; } = 0;

    public int EndShiftTankerKm { get; set; } = 0;

    public float OpeningMeter { get; set; }
    
    public float ClosingMeter { get; set; }

    public float OpeningBalance { get; set; }

    public float RemainingBalance { get; set; }

    public string Location { get; set; } = "";

    public string? Remarks { get; set; } = null;
    public string Fueler { get; set; } = null!;
    public bool IsPosted { get; set; }

    public IEnumerable<SelectItem> LocationSelections { get; set; } = new List<SelectItem>();
    public IEnumerable<Station> StationSelections { get; set; } = new List<Station>();
    public IEnumerable<SelectItem> AssetCodeSelections { get; set; } = new List<SelectItem>();
    public IEnumerable<EmployeeMaster> FuelerSelections { get; set; } = new List<EmployeeMaster>();
    public IEnumerable<FuelDetailResponse> FuelTransactions { get; set; } = new List<FuelDetailResponse>();
}


public class FuelDetailResponse
{
    public string LogSheetId { get; set; }
    public string Id { get; set; }
    public string AssetCode { get; set; } = "";
    public DateTime FuelDateTime { get; set; }
    public DateTime FuelDate => FuelDateTime;
    public DateTime FuelTime => FuelDateTime;
    public string? OperatorDriver { get; set; } = "";    
    public string Remarks { get; set; }
    public string LogType { get; set; }
    public int Reading { get; set; } = 0;
    public float Quantity { get; set; } = 0f;
    public float DisplayQty => LogType == "Refill" ? Quantity : Quantity * -1;
    public string DriverQatarIdUrl { get; set; }

}