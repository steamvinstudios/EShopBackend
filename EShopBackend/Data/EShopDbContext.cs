using EShopBackend.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EShopBackend.Data
{
    public class EShopDbContext : DbContext
    {
        // Конструктор, принимающий DbContextOptions<EShopDbContext>
        // Этот конструктор используется для внедрения зависимостей (DI)
        public EShopDbContext(DbContextOptions<EShopDbContext> options) : base(options) { }

        // DbSet'ы для каждой сущности.  Они представляют таблицы в базе данных.
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        // Переопределение метода OnModelCreating для настройки модели (Fluent API)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // --- КОНФИГУРАЦИЯ СВЯЗЕЙ (Fluent API) ---

            // Связь один-ко-многим: Category <-> Product
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)     // У Product есть одна Category
                .WithMany(c => c.Products)   // У Category много Products
                .HasForeignKey(p => p.CategoryId) // Внешний ключ - Product.CategoryId
                .OnDelete(DeleteBehavior.Restrict); // Запретить удаление категории, если есть связанные товары

            // Связь один-ко-многим: Order <-> OrderItem
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade); // Удалять OrderItems при удалении Order

            // Связь один-ко-многим: Product <-> OrderItem
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany()  // У Product может быть много OrderItems (или ни одного), поэтому WithMany без параметра
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict); // Не удалять товар, если он есть в заказах

            // Связь один-ко-многим: User <-> Order
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);  // Не удалять пользователя, если у него есть заказы

            // Связь один-ко-многим (самореферирующаяся): Category <-> Category (родительская/дочерние)
            modelBuilder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict); // Не удалять родительскую категорию, если есть дочерние


            // --- НАСТРОЙКА УНИКАЛЬНЫХ ИНДЕКСОВ ---

            // Уникальный индекс для Email пользователя
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Уникальный индекс для Username пользователя
            modelBuilder.Entity<User>()
               .HasIndex(u => u.Username)
               .IsUnique();

            // Уникальный индекс для имени категории (в пределах одной родительской категории)
            modelBuilder.Entity<Category>()
                .HasIndex(c => new { c.Name, c.ParentCategoryId }) // Составной индекс
                .IsUnique();


            // --- НАСТРОЙКА ЗНАЧЕНИЙ ПО УМОЛЧАНИЮ ---
            modelBuilder.Entity<Product>()
                .Property(p => p.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP"); // Автоматическое заполнение даты создания

            modelBuilder.Entity<Order>()
                .Property(o => o.OrderDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");


            // --- НАСТРОЙКА ТИПОВ ДАННЫХ (опционально, если типы C# не соответствуют типам БД) ---

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18, 2)");  // Указываем тип decimal с точностью 18 и 2 знаками после запятой

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.UnitPrice)
                .HasColumnType("decimal(18, 2)");


            // --- ИНИЦИАЛИЗАЦИЯ ДАННЫХ (Seed Data) - опционально ---

            // modelBuilder.Entity<Category>().HasData(
            //     new Category { Id = 1, Name = "Electronics" },
            //     new Category { Id = 2, Name = "Books" }
            // );
            //
            // modelBuilder.Entity<Product>().HasData(
            //     new Product { Id = 1, Name = "Smartphone", CategoryId = 1, Price = 599.99m, StockQuantity = 100 },
            //     new Product { Id = 2, Name = "The Lord of the Rings", CategoryId = 2, Price = 29.99m, StockQuantity = 50 }
            // );

            base.OnModelCreating(modelBuilder); // Вызов базового метода
        }
    }
}
