
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.PMV.Assets;

public class ServiceLog : BaseEntity<Guid>
{
    public string ServiceAlertId { get; set; } = null!;

    public DateTime? LastServiceDate { get; set; }
    
    public int LastReading { get; set; }

    public int KmAlert { get; set; }
    public int KmInteraval { get; set; }

    [NotMapped]
    public int AlertAtKm => LastReading + KmAlert;

    public int IntervalAtKm => LastReading + KmInteraval;

    public string GetServiceStatus() 
    {
        if(LastReading == AlertAtKm) {
            return "Warning";
        }
        else if(LastReading > AlertAtKm && LastReading <= IntervalAtKm) {
            return "Danger";
        }
        else if(LastReading > IntervalAtKm) {
            return "Critical";
        }
        else {
            return "Good";
        }
    }
}