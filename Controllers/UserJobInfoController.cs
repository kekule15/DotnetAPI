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
    public class UserJobInfoController : ControllerBase
    {

        readonly DataContextDapper _dataDapper;


        public UserJobInfoController(IConfiguration configuration)
        {
            _dataDapper = new DataContextDapper(configuration);
        }


        [HttpGet("Get/{userId}")]
        public UserJobInfo GetUserJobInfo(int userId)
        {
            string sql = @$"SELECT * FROM TutorialAppSchema.UserJobInfo WHERE UserId = {userId}";
            return _dataDapper.LoadSingleData<UserJobInfo>(sql);
        }

        [HttpPost("Add")]
        public IActionResult AddUserJobInfo(UserJobInfo job)
        {
            string sql = @$"
            INSERT INTO TutorialAppSchema.UserJobInfo (
                UserId,
                JobTitle,
                Department
            )
            VALUES(
                {job.UserId}, 
                '{job.JobTitle}',
                '{job.Department}'
            )";
            Console.WriteLine(sql);

            if (_dataDapper.ExecuteSql(sql) > 0)
            {
                return Ok();
            }
            throw new Exception("Error adding user Job info");

        }

        [HttpPut("Edit")]
        public IActionResult EditUserJobInfo(UserJobInfo job)
        {

            string sql = @$"
            UPDATE TutorialAppSchema.UserJobInfo SET  
                [JobTitle] = '{job.JobTitle}',
                [Department] = '{job.Department}'
            WHERE UserId = {job.UserId}";

            Console.WriteLine(sql);

            if (_dataDapper.ExecuteSql(sql) > 0)
            {
                return Ok();
            }
            throw new Exception("Error updating User JobInfo");

        }

        [HttpDelete("Delete/{userId}")]
        public IActionResult DeleteUserJobInfo(int userId)
        {

            string sql = @$"
            DELETE FROM TutorialAppSchema.UserJobInfo 
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