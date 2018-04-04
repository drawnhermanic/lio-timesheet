using System.ComponentModel.DataAnnotations;

namespace Timesheet.Models
{
    public class UserModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "UserName is required")]
        [MinLength(5, ErrorMessage = "UserName must be greater than 5 characters")]        
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}