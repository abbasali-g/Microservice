using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Data
{
    public class PostDBHelper
    {
        
        public async Task<string> addUser(string userID, string userName)
        {
            Data.DBHelper db = new Data.DBHelper();
            string query = "INSERT INTO [MicPost_User] ([ID],[Name]) VALUES (" + userID + ",'" + userName + "')";

            await db.execSql(query);
            return "{'post_AddUser':'" + userID + "' }";
        }

        
        public async Task<string> updateUser(string userID, string userName)
        {
            Data.DBHelper db = new Data.DBHelper();
            await db.writeToLog("updateUser__function", userID + "::" + userName);
            string _query = "update MicPost_User set [Name] ='" + userName + "' where ID=" + userID + ";";
            try
            {
                
                await db.execSql(_query);
                
                
            }
            catch (Exception ex)
            {
               await db.writeToLog("inside updateUser:", ex.Message);
                return "{}";
            }
            return "{'post_UpdateUser':'" + userID + "' }";

        }
    }
}
