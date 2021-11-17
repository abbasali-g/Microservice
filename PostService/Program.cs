using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using PostService.Data;
using PostService.Entities;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace PostService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ListenForIntegrationEvents();
                       
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void ListenForIntegrationEvents()
        {
            Data.DBHelper db = new DBHelper();


            var factory = new ConnectionFactory();
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                db.writeToLog("ListenForIntegrationEvents",message).Wait();
                //Console.WriteLine(" [x] Received {0}", message);

                var data = JObject.Parse(message);
                var type = ea.RoutingKey;
                if (type == "user.add")
                {
                    PostService.Controllers.PostController p = new Controllers.PostController();
                    p.addUser(data["id"].Value<int>().ToString(), data["name"].Value<string>()).Wait();
                    db.writeToLog("user.add", data["name"].Value<string>()).Wait();
                }
                else if (type == "user.update")
                {
                    PostService.Controllers.PostController p = new Controllers.PostController();
                    p.updateUser(data["id"].Value<int>().ToString(), data["name"].Value<string>()).Wait();
                    db.writeToLog("user.update", data["name"].Value<string>()).Wait();
                }
            };
            channel.BasicConsume(queue: "user.postservice",
                                     autoAck: true,
                                     consumer: consumer);
        }
    }
}
