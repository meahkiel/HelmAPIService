
namespace Core.PMV.Alerts;

public class ServiceAlert : BaseEntity<Guid>
{
    public string GroupName { get; set; }
    public string Description { get; set; }
    
    private List<AlertDetail> _details = new List<AlertDetail>();
    public IEnumerable<AlertDetail> Details => new List<AlertDetail>(_details);


}