using System;
using System.Data;
using Dapper;

namespace Timesheet.Data
{
    public interface ITimesheetRepository
    {
        int Save(int userId, int hours, DateTime dateOfTimesheet);
    }

    public class TimesheetRepository : ITimesheetRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public TimesheetRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public int Save(int userId, int hours, DateTime dateOfTimesheet)
        {
            using (IDbConnection db = _connectionFactory.CreateConnection())
            {
                const string insertQuery = @"INSERT INTO [dbo].[Timesheets] ([UserId], [HoursWorked], [Date]) VALUES (@UserId, @Hours, @Date)";
                return db.Execute(insertQuery, new { userId, hours, dateOfTimesheet.Date });
            }
        }
    }
}
