using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetAPI.Data;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace DotnetAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserEFController : ControllerBase
    {

        readonly DataContextEF _entityFramwork;


        public UserEFController(IConfiguration configuration)
        {
            _entityFramwork = new DataContextEF(configuration);

            // Console.WriteLine($"Connection string {configuration.GetConnectionString("DefaultConnection")}");
            Console.WriteLine(configuration.GetConnectionString("DefaultConnection"));
        }



        [HttpGet("GetUsers")]
        public IEnumerable<User> GetUsers()
        {

            IEnumerable<User> users = _entityFramwork.Users.ToList();
            return users;
        }


        [HttpGet("GetUser/{userId}")]
        public User GetUser(int userId)
        {
            User? user = _entityFramwork.Users.Where(e => e.UserId == userId).FirstOrDefault();
            if (user != null)
            {
                return user;
            }
            else
            {
                throw new Exception("Error Fetching this user");
            }
        }

        [HttpPost("AddUser")]
        public IActionResult AddUser(UserToAddDTO user)
        {
            User userDb = new()
            {
                Active = user.Active,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                Email = user.Email
            };
            _entityFramwork.Add(userDb);

            if (_entityFramwork.SaveChanges() > 0)
            {
                return Ok();
            }
            throw new Exception("Error Creating user");

        }

        [HttpPut("EditUser")]
        public IActionResult EditUser(User user)
        {

            User? userDb = _entityFramwork.Users.Where(e => e.UserId == user.UserId).FirstOrDefault();
            if (userDb != null)
            {
                userDb.Active = user.Active;
                userDb.FirstName = user.FirstName;
                userDb.LastName = user.LastName;
                userDb.Gender = user.Gender;
                userDb.Email = user.Email;

                if (_entityFramwork.SaveChanges() > 0)
                {
                    return Ok();
                }
                throw new Exception("Error Editing  this user");

            }
            throw new Exception("Error Fetching this user");

        }

        [HttpDelete("DeleteUser/{userId}")]
        public IActionResult DeleteUser(int userId)
        {


            User? userDb = _entityFramwork.Users.Where(e => e.UserId == userId).FirstOrDefault();
            if (userDb != null)
            {
                _entityFramwork.Users.Remove(userDb);

                if (_entityFramwork.SaveChanges() > 0)
                {
                    return Ok();
                }
                throw new Exception("Error Editing  this user");

            }
            throw new Exception("Error Fetching this user");


        }
    }


}