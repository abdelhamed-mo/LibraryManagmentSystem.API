namespace Service
{
	public class ServiceManger(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager, IConfiguration configuration) : IServiceManager
	{
		private readonly Lazy<IReaderService> readerService =
			new Lazy<IReaderService>(() => new ReaderService(unitOfWork, mapper));
		private readonly Lazy<IAdminService> adminService =
			new Lazy<IAdminService>(() => new AdminService(unitOfWork, mapper));
		private readonly Lazy<IAuthenticationService> authenticationService =
			new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager,configuration));

		public IReaderService ReaderService => readerService.Value;
		public IAdminService AdminService => adminService.Value;
		public IAuthenticationService AuthenticationService => authenticationService.Value;
	}
}
