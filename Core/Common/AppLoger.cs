using BaseEntityPack.Core;

namespace Core.Common;

public class AppLoger : AggregateRoot<Guid> {

    public AppLoger() : base(Guid.NewGuid())
    {
        
    }

    public string ErrorDescription { get; set; }
    public string Segment { get; set; }
    public string Input { get; set; }
}