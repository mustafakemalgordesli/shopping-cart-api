namespace WebAPI.Entities;

public class Cart : BaseEntity
{
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public virtual ICollection<CartItem> Items { get; set; }
}
