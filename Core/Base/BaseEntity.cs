using Core.Interfaces;

namespace Core.Base;


public abstract class BaseEntity<T> {

    private List<IDomainEvent> _domainEvents;
    
    
    public T Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }

}