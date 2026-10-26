using Microsoft.EntityFrameworkCore;
using messanger.Models;
namespace messanger.Controllers
{
    public class DB : DbContext    // класс для работы с бд, наследует все от DbContext, поэтому файл такой пустой
    {
        public DB(DbContextOptions<DB> options) : base(options)  // запускает конструктор от род класса, чтоб тот схавал строку подключения к бд
        {
        }
        public DbSet<UserDB> Users { get; set; } // таблица Users 

        // эти строки это обертки таблиц для программы, чтоб программа сама формировала sql запросы при использовании функций

    }
}