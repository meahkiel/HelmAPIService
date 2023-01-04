using System.ComponentModel.DataAnnotations.Schema;

namespace Core.PMV.Alerts;


public class ServiceAlertDetail : BaseEntity<Guid>
{
    public ServiceAlertDetail()
    {
        
    }
    public string ServiceCode { get; set; }

    public string? Description { get; set; }
    public int KmAlert { get; set; }
    public int KmInterval { get; set; }

    public ServiceAlert ServiceAlert { get; set; }

    [NotMapped]
    public virtual string Tracker { get; set; }

    

}