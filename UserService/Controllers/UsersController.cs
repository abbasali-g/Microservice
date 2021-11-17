using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserService.Data;
using UserService.Entities;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        
        public UsersController( )
        {
             
        }

        [HttpGet]
        public async Task<string> GetUser()
        {
            Data.DBHelper db = new DBHelper();
            return await db.execSql("select * from MicUser_User for json path");
        }

        private void PublishToMessageQueue(string integrationEvent, string eventData)
        {
            Data.DBHelper db = new DBHelper();
            db.writeToLog(integrationEvent, eventData).Wait();
            // TOOO: Reuse and close connections and channel, etc, 
            var factory = new ConnectionFactory();
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            var body = Encoding.UTF8.GetBytes(eventData);
            channel.BasicPublish(exchange: "user",
                                             routingKey: integrationEvent,
                                             basicProperties: null,
                                             body: body);
        }

        [HttpPut]
        public async Task<string> PutUser(User user)
        {
            string query = "UPDATE  MicUser_User SET [Name] = '"+user.Name+ "' ,[Mail] = '" + user.Mail + "'  ,[OtherData] = '" + user.OtherData+ "' WHERE ID=" + user.ID+ "";
            Data.DBHelper db = new DBHelper();
            await db.execSql(query);
            var integrationEventData = JsonConvert.SerializeObject(new
            {
                id = user.ID,
                newname = user.Name
            });
            PublishToMessageQueue("user.update", integrationEventData);


            return "{'user':'" + user.ID + "' }";
        }

        [HttpPost]
        public async Task<string> PostUser(User user)
        {
            string query = "INSERT INTO MicUser_User ([ID],[Name],[Mail],[OtherData])  VALUES("+user.ID+ ",'" + user.Name + "','" + user.Mail + "','" + user.OtherData + "')";
            Data.DBHelper db = new DBHelper();
            await db.execSql(query);

            var integrationEventData = JsonConvert.SerializeObject(new
            {
                id = user.ID,
                name = user.Name
            });
            PublishToMessageQueue("user.add", integrationEventData);


            return "{'user':'"+user.ID+"' }";
        }
    }
}
