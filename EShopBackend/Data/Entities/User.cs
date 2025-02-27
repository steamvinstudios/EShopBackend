namespace EShopBackend.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;       // Логин пользователя (уникальный)
        public string Email { get; set; } = string.Empty;          // Email пользователя (уникальный)
        public string PasswordHash { get; set; } = string.Empty;    // Хеш пароля
        public string? FirstName { get; set; }                    // Имя (необязательное)
        public string? LastName { get; set; }                     // Фамилия (необязательное)
        public string? PhoneNumber { get; set; }                   // Номер телефона (необязательное)
        public string? Address { get; set; }                     // Адрес (необязательное)
        public bool IsAdmin { get; set; } = false;               // Флаг администратора (необязательное)
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Дата создания аккаунта
        public ICollection<Order> Orders { get; set; } = new List<Order>(); // Заказы пользователя (навигационное свойство)
                                                                            // Другие свойства, например, роль пользователя, дата рождения, и т.д.
    }
}
