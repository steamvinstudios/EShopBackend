namespace EShopBackend.Data.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }                       // ID заказа
        public Order Order { get; set; } = null!;                // Заказ (навигационное свойство)
        public int ProductId { get; set; }                     // ID товара
        public Product Product { get; set; } = null!;              // Товар (навигационное свойство)
        public int Quantity { get; set; }                       // Количество заказанных единиц товара
        public decimal UnitPrice { get; set; }                   // Цена за единицу товара (на момент заказа)
                                                                 // Другие свойства, например, скидка на товар в заказе.
    }
}
