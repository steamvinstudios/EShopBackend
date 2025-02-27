namespace EShopBackend.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }                        // ID пользователя, сделавшего заказ
        public User User { get; set; } = null!;                  // Пользователь (навигационное свойство)
        public DateTime OrderDate { get; set; } = DateTime.UtcNow; // Дата заказа
        public decimal TotalAmount { get; set; }                  // Общая сумма заказа
        public string ShippingAddress { get; set; } = string.Empty;     // Адрес доставки
        public string? OrderNotes { get; set; }                   // Примечания к заказу (необязательное)
        public string Status { get; set; } = "Pending";          // Статус заказа (например, "Pending", "Shipped", "Delivered", "Cancelled")
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>(); // Элементы заказа (навигационное свойство)
                                                                                        // Другие свойства, например, способ оплаты, номер отслеживания, и т.д.
    }
}
