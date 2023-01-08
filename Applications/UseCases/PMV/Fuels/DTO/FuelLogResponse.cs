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

    public DateTime FueledDate { get; set; }

    public string? ShiftStartTime { get; set; }

    public string? ShiftEndTime { get; set; }

    public int StartShiftTankerKm { get; set; }

    public int? EndShiftTankerKm { get; set; }

    public string Location { get; set; } = "";

    public string Station { get; set; } = "";

    public string? Remarks { get; set; } = null;

    public string Fueler { get; set; } = null!;

    public IEnumerable<FuelTransactionResponse> Details { get; set; } = new List<FuelTransactionResponse>();
}


public class FuelTransactionResponse
{
    public string LogSheetId { get; set; }
    public string Id { get; set; }
    public string AssetCode { get; set; } = "";
    public DateTime FuelTime { get; set; } = DateTime.Now;
    public string? OperatorDriver { get; set; } = "";
    public int Reading { get; set; } = 0;
    
    public float Quantity { get; set; } = 0f;
    public string DriverQatarIdUrl { get; set; }
    public string TransactionType { get; set; }


}