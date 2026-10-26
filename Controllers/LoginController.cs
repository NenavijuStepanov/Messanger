using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using messanger.Controllers;
using messanger.Models;

namespace messanger.Controllers
{
    [Route("api/Login")]  // POST /api/login
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly DB _db;       // объект для работы с бд
        private readonly IPasswordHasher<UserDB> _hasher; // объект для хэширования   // приватные поля, чтоб в них записывать объекты из других файлов


        public LoginController(DB db, IPasswordHasher<UserDB> hasher) // в функции переменные мы получаем их из других файлов и присваиваем нашим приватным полям
        {
            _db = db;
            _hasher = hasher;
        }

        [HttpPost]  
        public async Task<IActionResult> Login([FromBody] UserDTO userDTO)   // получение из класса LoginDTO.cs значений
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == userDTO.Username);    // запрос к бд на поиск первого попавшегося с таким юзером
            if (user == null)
                return Unauthorized();


            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, userDTO.Password);    // проверка на совпадение паролей
            if (result == PasswordVerificationResult.Failed)
                return Unauthorized();

            return Ok();
        }
    }
}
