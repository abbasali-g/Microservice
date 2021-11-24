using System.Data.SqlClient;

using System.Threading.Tasks;

namespace PostService.Data
{
    public class DBHelper
    {
        public async Task<string> execSql(string query)
        {
            string constr = @"Data Source=192.168.2.2;Initial Catalog=microservice;uid=sa;password=123";
            string rz = "{}";
            using (SqlConnection con = new SqlConnection("Data Source=192.168.2.2;Initial Catalog=microservice;uid=sa;password=123"))
            {

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        if (sdr.Read())
                        {
                            rz = sdr[0].ToString();

                        }
                    }
                    con.Close();
                }
            }
            return rz;
        }

        public  async Task writeToLog(string title, string content)
        {
            string query = "INSERT INTO Mic_log   (logTitle,logContent)  VALUES('"+ title + "','"+ content + "')";
            await execSql(query);
        }

    }
}
