namespace UserManagementApp.Domain.Entities;

public class Entity
{
    public string Id { get; set; }

    public DateTimeOffset Created { get; set; }

    public string? CreatedBy { get; set; } = string.Empty;

    public bool IsDeleted { get; set; }

    public string? DeletedToken { get; set; }

    public DateTimeOffset? Updated { get; set; }

    public string? UpdatedBy { get; set; } = string.Empty;
}
