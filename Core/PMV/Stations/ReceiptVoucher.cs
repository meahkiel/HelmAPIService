namespace Core.PMV.Stations;

public class ReceiptVoucher
{
    public LVStation LVStation { get; set; }

    public DateTime? DateOfPurchase { get; set; }
    public string Description { get; set; }
    public int ReceivedQty { get; set; }

    

}