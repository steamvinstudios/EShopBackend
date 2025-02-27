namespace EShopBackend.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;         // Название товара
        public string Description { get; set; } = string.Empty;    // Описание товара
        public decimal Price { get; set; }                      // Цена товара
        public int StockQuantity { get; set; }                 // Количество на складе
        public int CategoryId { get; set; }                     // ID категории
        public Category Category { get; set; } = null!;            // Категория (навигационное свойство)
        public string? ImageUrl { get; set; }                   // URL изображения товара (необязательное)
        public bool IsFeatured { get; set; } = false;          // Флаг "Рекомендуемый товар" (необязательное)
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;   // Дата создания
        public DateTime? UpdatedAt { get; set; }                  // Дата последнего обновления (необязательное)
                                                                  // Можно добавить другие свойства:  вес, размеры, цвет, производитель, и т.д.
    }
}
