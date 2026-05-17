using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Service
{
	public class EmailService(IConfiguration configuration) : IEmailService
	{
		public void EmailSender(string to, string subject, string body)
		{
			// need to update to get the email settings from configuration
			var displayName = configuration["EmailSettings:DisplayName"];
			var senderEmail = configuration["EmailSettings:Email"];
			var password = configuration["EmailSettings:Password"];
			var host = configuration["EmailSettings:Host"];
			var port = int.Parse(configuration["EmailSettings:Port"]);

			var message = new MimeMessage();

			message.From.Add( MailboxAddress.Parse(senderEmail));
			message.To.Add(MailboxAddress.Parse(to));
			message.Subject = subject;
			message.Body = new TextPart("plain") { Text = body };

			using (var client = new SmtpClient())
			{
				client.Connect(host, port, SecureSocketOptions.StartTls);
				client.Authenticate(senderEmail, password);
				client.Send(message);
				client.Disconnect(true);
			}
		}
	}
}
