namespace LibrarySystem.Models
{
    // Общий базовый класс для всех сущностей БД — поле Id есть у каждой таблицы.
    public abstract class BaseEntity
    {
        public int Id { get; set; }
    }
}
