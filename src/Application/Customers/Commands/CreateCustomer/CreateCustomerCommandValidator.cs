using Demo.Application.Common.Interfaces;

namespace Demo.Application.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateCustomerCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.NomeEmpresa)
            .NotEmpty()
            .MaximumLength(200)
            .MustAsync(BeUniqueTitle)
                .WithMessage("'{PropertyName}' deve ser único.")
                .WithErrorCode("Unique");
    }

    public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
    {
        return await _context.Customers
            .AllAsync(l => l.NomeEmpresa != title, cancellationToken);
    }
}
