namespace Demo.Domain.Events;

public class CustomerDeletedEvent : BaseEvent
{
    public CustomerDeletedEvent(Customer item)
    {
        Item = item;
    }

    public Customer Item { get; }
}
