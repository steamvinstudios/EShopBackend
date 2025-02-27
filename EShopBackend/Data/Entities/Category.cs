namespace EShopBackend.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // Название категории
        public string? Description { get; set; }      // Описание категории (необязательное)
        public int? ParentCategoryId { get; set; }   // ID родительской категории (для иерархических категорий)
        public Category? ParentCategory { get; set; } // Навигационное свойство к родительской категории
        public ICollection<Product> Products { get; set; } = new List<Product>(); // Товары в этой категории (навигационное свойство)
        public ICollection<Category> SubCategories { get; set; } = new List<Category>(); // Подкатегории (навигационное свойство)
    }
}
