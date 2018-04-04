
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace Timesheet.Data
{
    public interface IUserRepository
    {
        int Save(string userName, string password);
    }

    public class UserRepository : IUserRepository
    {
        public int Save(string userName, string password)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetConnection"].ConnectionString))
            {
                const string insertQuery = @"INSERT INTO [dbo].[User]([UserName], [Password], [CreatedDateTime]) VALUES (@UserName, @Password); SELECT SCOPE_IDENTITY()";
                return db.Execute(insertQuery, new { userName, password });
            }
        }
    }
}
