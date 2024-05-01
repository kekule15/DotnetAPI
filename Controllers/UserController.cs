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

    }
}