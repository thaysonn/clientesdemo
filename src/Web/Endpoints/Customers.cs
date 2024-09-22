using Demo.Application.Customers.Commands.CreateCustomer;
using Demo.Application.Customers.Commands.DeleteCustomer;
using Demo.Application.Customers.Commands.UpdateCustomer;
using Demo.Application.Customers.Queries.GetCustomers;

namespace Demo.Web.Endpoints;

public class Customers : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(Get)
            .MapPost(Create)
            .MapPut(Update, "{id}")
            .MapDelete(Delete, "{id}");
    }

    public Task<CustomerVm> Get(ISender sender)
    {
        return sender.Send(new GetCustomersQuery());
    }

    public Task<int> Create(ISender sender, CreateCustomerCommand command)
    {
        return sender.Send(command);
    }

    public async Task<IResult> Update(ISender sender, int id, UpdateCustomerCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> Delete(ISender sender, int id)
    {
        await sender.Send(new DeleteCustomerCommand(id));
        return Results.NoContent();
    }
}
