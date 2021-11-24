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
            Data.DBHelper db = new DBHelper();
            try
            {
                ListenForIntegrationEvents();
            }catch(Exception ex)
            {
                db.writeToLog("main start Exception:", ex.Message).Wait();          
            }

            
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
            db.writeToLog("ListenForIntegrationEvents start position","").Wait();
            try
            {
                


                var factory = new ConnectionFactory() { HostName = "rmax.rabbitmq", UserName = "guest", Password = "guest", Port = 5672 };
                var connection = factory.CreateConnection();
                var channel = connection.CreateModel();
                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {

                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    db.writeToLog("ListenForIntegrationEvents:", message).Wait();
                    //Console.WriteLine(" [x] Received {0}", message);

                    
                    var data = JObject.Parse(message);
                    var type = ea.RoutingKey;
                    if (type == "user.add")
                    {
                        PostDBHelper pa = new PostDBHelper();
                        pa.addUser(data["id"].Value<int>().ToString(), data["name"].Value<string>()).Wait();
                        db.writeToLog("user.add", data["name"].Value<string>()).Wait();
                    }
                    else if (type == "user.update")
                    {
                        PostDBHelper pu = new PostDBHelper();
                        db.writeToLog("user.update:::"+ data["id"].Value<int>().ToString(), data["name"].Value<string>()).Wait();
                        pu.updateUser(data["id"].Value<int>().ToString(), data["name"].Value<string>()).Wait();
                        db.writeToLog("after user.update", "").Wait();
                    }
                };
                channel.BasicConsume(queue: "user.postservice",
                                         autoAck: true,
                                         consumer: consumer);
            }
            catch (Exception ex)
            { db.writeToLog("receive Error",ex.Message).Wait(); }
        }
    }
}
