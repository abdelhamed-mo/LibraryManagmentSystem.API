namespace ServiceAbstraction
{
	public interface IEmailService
	{
		void EmailSender(string to, string subject, string body);
	}
}
