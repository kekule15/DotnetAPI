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
    public class UserController : ControllerBase
    {

        readonly DataContextDapper _dataDapper;


        public UserController(IConfiguration configuration)
        {
            _dataDapper = new DataContextDapper(configuration);

            // Console.WriteLine($"Connection string {configuration.GetConnectionString("DefaultConnection")}");
            Console.WriteLine(configuration.GetConnectionString("DefaultConnection"));
        }

        [HttpGet("TestConnection")]
        public DateTime GetConnectionDateTime()
        {
            return _dataDapper.LoadSingleData<DateTime>("SELECT GETDATE()");
        }

        [HttpGet("GetUsers")]
        public IEnumerable<User> GetUsers()
        {
            // string sql = @"
            //     SELECT 
            //         [UserId],
            //         [FirstName],
            //         [LastName],
            //         [Email],
            //         [Gender],
            //     [Active] FROM TutorialAppSchema.Users
            // ";
            string sql = "SELECT * FROM TutorialAppSchema.Users";

            IEnumerable<User> users = _dataDapper.LoadData<User>(sql);
            return users;
        }


        [HttpGet("GetUser/{userId}")]
        public User GetUser(int userId)
        {
            return _dataDapper.LoadSingleData<User>($"SELECT * FROM TutorialAppSchema.Users WHERE UserId = {userId};");
        }

        [HttpPost("AddUser")]
        public IActionResult AddUser(User user)
        {
            string sql = @$"
            INSERT INTO TutorialAppSchema.Users(
                FirstName,
                LastName,
                Email,
                Gender,
                Active
            )
            VALUES(
                '{user.FirstName}', 
                '{user.LastName}', 
                '{user.Email}', 
                '{user.Gender}',  
                '{user.Active}'
            )";
            Console.WriteLine(sql);

            if (_dataDapper.ExecuteSql(sql) > 0)
            {
                return Ok();
            }
            throw new Exception("Error creating this user");

        }

        [HttpPut("EditUser")]
        public IActionResult EditUser(User user)
        {

            string sql = @$"
            UPDATE TutorialAppSchema.Users SET  
                [FirstName] = '{user.FirstName}',
                [LastName] = '{user.LastName}',
                [Email] = '{user.Email}',
                [Gender]= '{user.Gender}',
                [Active] = '{user.Active}' 
            WHERE UserId = {user.UserId}";

            Console.WriteLine(sql);

            if (_dataDapper.ExecuteSql(sql) > 0)
            {
                return Ok();
            }
            throw new Exception("Error updating this user");

        }
    }
}