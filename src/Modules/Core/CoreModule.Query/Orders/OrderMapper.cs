using CoreModule.Query._Data.Entities;
using CoreModule.Query.DTOs;
using CoreModule.Query.Orders.DTOs;

namespace CoreModule.Query.Orders;

static class OrderMapper
{
    public static OrderDto? MapOrder(OrderQueryModel? order)
    {
        if (order == null)
            return null;
        return new OrderDto
        {
            Id = order.Id,
            CreationDate = order.CreationDate,
            UserId = order.UserId,
            IsPay = order.IsPay,
            Discount = order.Discount,
            DiscountCode = order.DiscountCode,
            PaymentDate = order.PaymentDate,
            OrderItems = order.OrderItems.Select(s => new OrderItemDto
            {
                Id = s.Id,
                CreationDate = s.CreationDate,
                OrderId = s.OrderId,
                CourseTitle = s.Course.Title,
                Price = s.Price,
                TeacherFullName = s.Course.Teacher.Users.FullName!,
                CourseId = s.CourseId
            }).ToList(),
            User = new CoreModuleUserDto
            {
                Id = order.User.Id,
                CreationDate = order.User.CreationDate,
                Avatar = order.User.Avatar,
                Name = order.User.Name,
                Family = order.User.Family,
                Email = order.User.Email,
                Mobile = order.User.Mobile,
            }
        };
    }
}