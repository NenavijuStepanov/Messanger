using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using messanger.Controllers;
using messanger.Models;

namespace messanger.Controllers
{
    [Route("api/NewUser")]
    [ApiController]
    public class CreateAccountController : ControllerBase
    {
        private readonly DB _db;       // объект для работы с бд
        private readonly IPasswordHasher<UserDB> _hasher; // объект для хэширования   // приватные поля, чтоб в них записывать объекты из других файлов


        public CreateAccountController(DB db, IPasswordHasher<UserDB> hasher) // в функции переменные мы получаем их из других файлов и присваиваем нашим приватным полям
        {
            _db = db;
            _hasher = hasher;
        }


        [HttpPost] 
        public async Task<IActionResult> Register([FromBody] UserDTO dto) // получение из класса UserDTO.cs значений
        {

            if (await _db.Users.AnyAsync(u => u.Username == dto.Username))  // проверка на существование кого-либо с таким юзером
                return BadRequest("Пользователь уже существует");

            var newUser = new UserDB   // объявление объекта класса UserDTO.cs
            {
                Username = dto.Username
            };


            newUser.PasswordHash = _hasher.HashPassword(newUser, dto.Password); //хэширование

            _db.Users.Add(newUser);  // в объект добавляет данные
            await _db.SaveChangesAsync(); // посылает запрос и коммитит

            return Ok();
        }
    }
}
