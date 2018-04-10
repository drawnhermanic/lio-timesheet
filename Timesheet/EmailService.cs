namespace Timesheet
{
    public interface IEmailService
    {
        void SendEmail(string emailAddress);
    }

    public class EmailService : IEmailService
    {
        public void SendEmail(string emailAddress)
        {
            //send email duh
        }
    }
}