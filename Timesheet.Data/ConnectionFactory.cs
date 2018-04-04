using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Timesheet.Data
{
    public interface IConnectionFactory
    {
        IDbConnection CreateConnection();
    }

    public class ConnectionFactory : IConnectionFactory
    {
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetConnection"].ConnectionString);
        }
    }
}