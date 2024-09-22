namespace Demo.Domain.Events;

public class CustomerUpdatedEvent : BaseEvent
{
    public CustomerUpdatedEvent(Customer item)
    {
        Item = item;
    }

    public Customer Item { get; }
}
