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
    public class UserSalaryController : ControllerBase
    {

        readonly DataContextDapper _dataDapper;


        public UserSalaryController(IConfiguration configuration)
        {
            _dataDapper = new DataContextDapper(configuration);
        }


        [HttpGet("GetUserSalary/{userId}")]
        public UserSalary GetUserSalary(int userId)
        {
            string sql = @$"SELECT * FROM TutorialAppSchema.UserSalary WHERE UserId = {userId}";
            return _dataDapper.LoadSingleData<UserSalary>(sql);
        }

        [HttpPost("AddUserSalary")]
        public IActionResult AddUserSalary(UserSalary user)
        {
            string sql = @$"
            INSERT INTO TutorialAppSchema.UserSalary (
                UserId,
                Salary
            )
            VALUES(
                {user.UserId}, 
                '{user.Salary}'
            )";
            Console.WriteLine(sql);

            if (_dataDapper.ExecuteSql(sql) > 0)
            {
                return Ok();
            }
            throw new Exception("Error adding user salary");

        }

        [HttpPut("EditUserSalary")]
        public IActionResult EditUserSalary(UserSalary user)
        {

            string sql = @$"
            UPDATE TutorialAppSchema.UserSalary SET  
                [UserSalary] = '{user.Salary}'
            WHERE UserId = {user.UserId}";

            Console.WriteLine(sql);

            if (_dataDapper.ExecuteSql(sql) > 0)
            {
                return Ok();
            }
            throw new Exception("Error updating user salary");

        }

        [HttpDelete("DeleteUserSalary/{userId}")]
        public IActionResult DeleteUserSalary(int userId)
        {

            string sql = @$"
            DELETE FROM TutorialAppSchema.UserSalary 
            WHERE UserId = {userId}";

            Console.WriteLine(sql);

            if (_dataDapper.ExecuteSql(sql) > 0)
            {
                return Ok();
            }
            throw new Exception("Error Deleting user salary");


        }
    }


}