using Common.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreModule.Query._Data.Entities;

[Table("Orders")]
class OrderQueryModel : Entity
{
    public Guid UserId { get; set; }
    public bool IsPay { get; set; }
    public int Discount { get; set; }
    public string? DiscountCode { get; set; }
    public DateTime? PaymentDate { get; set; }


    public List<OrderItemQueryModel> OrderItems { get; set; } = [];

    [ForeignKey("UserId")]
    public UserQueryModel User { get; set; } = null!;
}

class OrderItemQueryModel : Entity
{
    public Guid CourseId { get; set; }
    public Guid OrderId { get; set; }
    public int Price { get; set; }


    [ForeignKey("OrderId")]
    public OrderQueryModel Order { get; set; } = null!;

    public CourseQueryModel Course { get; set; } = null!;
}