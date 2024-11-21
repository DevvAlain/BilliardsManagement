namespace Domain.Models.Views;

public class RoleViewModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreateAt { get; set; }
}