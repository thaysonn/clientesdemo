using Demo.Application.Common.Interfaces;

namespace Demo.Application.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCustomerCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.NomeEmpresa)
            .NotEmpty()
            .MaximumLength(200)
            .MustAsync(BeUniqueTitle)
                .WithMessage("'{PropertyName}' deve ser único.")
                .WithErrorCode("Unique");
    }

    public async Task<bool> BeUniqueTitle(UpdateCustomerCommand model, string title, CancellationToken cancellationToken)
    {
        return await _context.Customers
            .Where(l => l.Id != model.Id)
            .AllAsync(l => l.NomeEmpresa != title, cancellationToken);
    }
}
