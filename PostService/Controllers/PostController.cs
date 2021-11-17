using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Data;
using PostService.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PostService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {

        
        [HttpGet]
        public async Task<string> GetPost()
        {
            Data.DBHelper db = new Data.DBHelper();
            return await db.execSql("select * from MicPost_Post for json path");

        }

        [HttpPost]
        public async Task<string> PostPost(Post post)
        {
            Data.DBHelper db = new Data.DBHelper();
            string query = "INSERT INTO MicPost_Post(PostId,PostTitle,PostContent,PostUserID)";
                    query+=" VALUES("+post.PostId+ ",'" + post.PostTitle + "','" + post.PostContent + "' ," + post.User.ID + ")";
            await db.execSql(query);
            return "{'post':'" + post.PostId + "' }";
        }

        public async Task<string> addUser(string userID, string userName)
        {
            Data.DBHelper db = new Data.DBHelper();
            string query = "INSERT INTO [MicPost_User] ([ID],[Name]) VALUES ("+userID+",'"+userName+"')";
            
            await db.execSql(query);
            return "{'post_AddUser':'" + userID + "' }";
        }
        public async Task<string> updateUser(string userID, string userName)
        {
            Data.DBHelper db = new Data.DBHelper();
            string query = "udpate [MicPost_User] set [Name] ='"+userName+"' where ID=" + userID + "";

            await db.execSql(query);
            return "{'post_UpdateUser':'" + userID + "' }";
        }
    }
}