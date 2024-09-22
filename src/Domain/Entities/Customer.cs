namespace Demo.Domain.Entities;

public class Customer : BaseAuditableEntity
{
    public string? NomeEmpresa { get; set; }

    public PorteEmpresa PorteEmpresa { get; set; }
}
