using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Service
{
	internal class EmailService(IConfiguration configuration) : IEmailService
	{
		public void EmailSender(string to, string subject, string body)
		{
			var displayName = configuration["EmailSettings:DisplayName"];
			var senderEmail = configuration["EmailSettings:Email"];
			var password = configuration["EmailSettings:Password"];
			var host = configuration["EmailSettings:Host"];
			var port = int.Parse(configuration["EmailSettings:Port"]);
			
			var message = new MimeMessage();
			message.From.Add(new MailboxAddress(displayName, senderEmail));
			message.To.Add(new MailboxAddress("", to));
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
