using System.Data.SqlClient;

namespace MVCBlogApp.Web.Helpers
{
    public class LogHelper
    {
        public static void LogToSqlServer(string message)
        {
            string connectionString = "YOUR_CONNECTION_STRING_HERE";
            string query = "INSERT INTO Logs (LogMessage, LogDate) VALUES (@Message, @Date)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@Message", message);
                    command.Parameters.AddWithValue("@Date", DateTime.Now);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
