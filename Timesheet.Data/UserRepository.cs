
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
        private readonly IConnectionFactory _connectionFactory;

        public UserRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public int Save(string userName, string password)
        {
            using (IDbConnection db = _connectionFactory.CreateConnection())
            {
                const string insertQuery = @"INSERT INTO [dbo].[Users]([UserName], [Password]) VALUES (@UserName, @Password); SELECT SCOPE_IDENTITY()";
                return db.Execute(insertQuery, new { userName, password });
            }
        }
    }
}
