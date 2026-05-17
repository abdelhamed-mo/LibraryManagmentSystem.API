namespace Service
{
	public class ServiceManger : IServiceManager
	{
		private readonly Lazy<IReaderService> readerService;
		private readonly Lazy<IAdminService> adminService;
		private readonly Lazy<IEmailService> emailService;
		private readonly Lazy<IAuthenticationService> authenticationService;

		public ServiceManger(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
		{
			readerService =
			new Lazy<IReaderService>(() => new ReaderService(unitOfWork, mapper));
			adminService =
			new Lazy<IAdminService>(() => new AdminService(unitOfWork, mapper));
			emailService =
			new Lazy<IEmailService>(() => new EmailService(configuration));
			authenticationService =
			new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager,configuration,emailService.Value));
		}

		public IReaderService ReaderService => readerService.Value;
		public IAdminService AdminService => adminService.Value;
		public IAuthenticationService AuthenticationService => authenticationService.Value;
		public IEmailService EmailService => emailService.Value;
	}
}
