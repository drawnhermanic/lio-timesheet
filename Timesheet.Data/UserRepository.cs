using System.Data;
using Dapper;

namespace Timesheet.Data
{
    public interface IUserRepository
    {
        int Save(string userName, string password);
        Manager GetManager(int userId);
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
                const string insertQuery = @"INSERT INTO [dbo].[Users]([UserName], [Password], [ManagerId]) VALUES (@UserName, @Password, 1); SELECT SCOPE_IDENTITY()";
                return db.QueryFirstOrDefault<int>(insertQuery, new { userName, password });
            }
        }

        public Manager GetManager(int userId)
        {
            using (IDbConnection db = _connectionFactory.CreateConnection())
            {
                const string getManagerIdQuery = @"SELECT ManagerId, EmailAddress FROM [dbo].[Users] U INNER JOIN [dbo].[Managers] M ON U.ManagerId = M.UserId WHERE ManagerId IS NOT NULL AND U.Id = @UserId";
                return db.QuerySingleOrDefault<Manager>(getManagerIdQuery, new { userId });
            }
        }
    }

    public class Manager
    {
        public int Id { get; set; }
        public string EmailAddress { get; set; }
    }
}
