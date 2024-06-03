using ECommerce.Respiratory.Entities;
using ECommerce.Respiratory.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public UserController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = _unitOfWork.UserRepository.Get().ToList();
            return Ok(users);
        }

        // GET: api/User/page
        [HttpGet("page")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersPaginated(int pageIndex = 1, int pageSize = 10, string fullName = null)
        {
            var users = _unitOfWork.UserRepository.Get(
                u => string.IsNullOrEmpty(fullName) || u.FullName.Contains(fullName),
                q => q.OrderBy(u => u.UserId),
                "",
                pageIndex,
                pageSize
            ).ToList();

            return Ok(users);
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = _unitOfWork.UserRepository.GetByID(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _unitOfWork.UserRepository.Insert(user);
            _unitOfWork.Save();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // POST: api/User/bulk
        [HttpPost("bulk")]
        public async Task<ActionResult> PostUsersBulk(List<User> users)
        {
            foreach (var user in users)
            {
                _unitOfWork.UserRepository.Insert(user);
            }
            _unitOfWork.Save();

            return Ok();
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _unitOfWork.UserRepository.Update(user);

            try
            {
                _unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // PUT: api/User
        [HttpPut]
        public async Task<IActionResult> UpdateUserInfo(User user)
        {
            var existingUser = _unitOfWork.UserRepository.GetByID(user.UserId);
            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.FullName = user.FullName;
            existingUser.Email = user.Email;
            existingUser.Address = user.Address;
            existingUser.Phone = user.Phone;

            _unitOfWork.UserRepository.Update(existingUser);
            _unitOfWork.Save();

            return NoContent();
        }

        // POST: api/User/change-password
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(int userId, string newPassword)
        {
            var user = _unitOfWork.UserRepository.GetByID(userId);
            if (user == null)
            {
                return NotFound();
            }

            user.Password = newPassword;
            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.Save();

            return NoContent();
        }

        //// POST: api/User/upload-avatar
        //[HttpPost("upload-avatar")]
        //public async Task<IActionResult> UploadAvatar(int userId, IFormFile file)
        //{
        //    var user = _unitOfWork.UserRepository.GetByID(userId);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    if (file == null || file.Length == 0)
        //    {
        //        return BadRequest("No file uploaded.");
        //    }

        //    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/avatars", file.FileName);

        //    using (var stream = new FileStream(path, FileMode.Create))
        //    {
        //        await file.CopyToAsync(stream);
        //    }

        //    user.AvatarPath = path;
        //    _unitOfWork.UserRepository.Update(user);
        //    _unitOfWork.Save();

        //    return Ok(new { file.FileName, file.Length });
        //}

        // GET: api/User/order-history
        [HttpGet("order-history")]
        public async Task<ActionResult<IEnumerable<BoughtItem>>> GetOrderHistory(int userId)
        {
            var user = _unitOfWork.UserRepository.GetByID(userId);
            if (user == null)
            {
                return NotFound();
            }

            var orderHistory = _unitOfWork.BoughtItemRepository.Get(b => b.Checkout.UserId == userId).ToList();

            return Ok(orderHistory);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = _unitOfWork.UserRepository.GetByID(id);
            if (user == null)
            {
                return NotFound();
            }

            _unitOfWork.UserRepository.Delete(id);
            _unitOfWork.Save();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _unitOfWork.UserRepository.GetByID(id) != null;
        }
    }
}
