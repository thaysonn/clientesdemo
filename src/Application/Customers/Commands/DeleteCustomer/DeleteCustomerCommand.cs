namespace Demo.Application.Customers.Commands.DeleteCustomer;

public record DeleteCustomerCommand(int Id) : IRequest;
