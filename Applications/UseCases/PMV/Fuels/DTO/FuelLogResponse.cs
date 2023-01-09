using Applications.UseCases.Common;
using Core.Common;
using Core.PMV.Fuels;

namespace Applications.UseCases.PMV.Fuels.DTO;


public class FuelLogKeyResponse
{
    public string Id { get; set; }
    public int DocumentNo { get; set; }
}

public class FuelLogDetailKeyResponse
{
    public string Id { get; set; }
    public string LogSheetId { get; set; }
}


public class FuelLogResponse
{

    public string? Id { get; set; } = null;
    public int ReferenceNo { get; set; }
    public int DocumentNo { get; set; }
    public DateTime FueledDate { get; set; } = new DateTime();
    public DateTime? ShiftStartTime { get; set; } 
    public DateTime? ShiftEndTime { get; set; }

    public int StartShiftTankerKm { get; set; }

    public int? EndShiftTankerKm { get; set; }

    public string Location { get; set; } = "";

    public string? Remarks { get; set; } = null;
    public string Fueler { get; set; } = null!;
    public bool IsPosted { get; set; }

    public IEnumerable<SelectItem> LocationSelections { get; set; } = new List<SelectItem>();
    public string Station { get; set; } = "";
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
    public DateTime FuelTime { get; set; } = DateTime.Now;
    public string? OperatorDriver { get; set; } = "";

    public string Remarks { get; set; }

    public string LogType { get; set; }
    
    public int Reading { get; set; } = 0;
    public float Quantity { get; set; } = 0f;
    public string DriverQatarIdUrl { get; set; }
    


}