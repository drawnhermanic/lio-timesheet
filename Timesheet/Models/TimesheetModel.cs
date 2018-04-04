using System;
using System.ComponentModel.DataAnnotations;

namespace Timesheet.Models
{
    public class TimesheetModel
    {
        public int TimesheetId { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Hours worked is required")]
        [Range(0, 24, ErrorMessage = "Can only be between 0 .. 24")]
        public int Hours { get; set; }

        public int UserId { get; set; }
    }
}