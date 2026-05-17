namespace ServiceAbstraction
{
	public interface IServiceManager
	{
		public IReaderService ReaderService { get; }
		public IAdminService AdminService { get; }
		public IAuthenticationService AuthenticationService { get; }
		public IEmailService EmailService { get; }
	}
}
