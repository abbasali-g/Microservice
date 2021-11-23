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

        
        [HttpGet("GetPost")]
        public async Task<string> GetPost()
        {
            Data.DBHelper db = new Data.DBHelper();
            return await db.execSql("select MicPost_Post.*,micpost_user.Name from MicPost_Post inner join micpost_user on micpost_user.ID = MicPost_Post.PostUserID  for json path");

        }

        [HttpPost("PostPost")]
        public async Task<string> PostPost(Post post)
        {
            Data.DBHelper db = new Data.DBHelper();
            string query = "INSERT INTO MicPost_Post(PostId,PostTitle,PostContent,PostUserID)";
                    query+=" VALUES("+post.PostId+ ",'" + post.PostTitle + "','" + post.PostContent + "' ," + post.User.ID + ")";
            await db.execSql(query);
            return "{'post':'" + post.PostId + "' }";
        }

        [HttpPut("PutPost")]
        public async Task<string> PutPost(User user)
        {
            Data.PostDBHelper db = new Data.PostDBHelper();
            await db.updateUser(user.ID.ToString(),user.Name);
            return "{'postuser':'" + user.ID.ToString() + "' }";
        }


    }
}