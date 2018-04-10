using System;
using System.Collections.Generic;
using System.Data;
using Dapper;

namespace Timesheet.Data
{
    public interface ITimesheetRepository
    {
        int Save(int userId, int hours, DateTime dateOfTimesheet);
        IList<Timesheet> Get(int userId);
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

        public IList<Timesheet> Get(int userId)
        {
            using (IDbConnection db = _connectionFactory.CreateConnection())
            {
                const string getTimesheetQuery = @"SELECT * FROM [dbo].[Timesheets] WHERE UserId = @UserId";
                return db.Query<Timesheet>(getTimesheetQuery, new { userId }).AsList();
            }
        }
    }

    public class Timesheet
    {
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public int HoursWorked { get; set; }
    }
}
