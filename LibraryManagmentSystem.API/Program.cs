namespace LibraryManagementSystem.API
{
	public class Program(StoreContext context)
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers()
			   .AddApplicationPart(typeof(ControllerAssembly).Assembly);
			builder.Services.AddDbContext<StoreContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSqlConnection")));
			//builder.Services.AddDbContext<IdentityStoreContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("IdentitySqlConnection")));
			builder.Services.AddIdentity<User, IdentityRole>(opt =>
			{
				//// comment for dev time
				opt.Password.RequireDigit = false;
				opt.Password.RequireUppercase = false;
				opt.Password.RequireLowercase = false;
				opt.Password.RequireNonAlphanumeric = false;
				opt.Password.RequiredLength = 8;
				opt.User.RequireUniqueEmail = true;
			})
			.AddEntityFrameworkStores<StoreContext>()
			.AddDefaultTokenProviders();

			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,

					ValidAudience = builder.Configuration.GetSection("JwtOptions")["Audience"],
					ValidIssuer = builder.Configuration.GetSection("JwtOptions")["Issuer"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JwtOptions")["SecretKey"]!))
				};
			});

			builder.Services.AddAuthorization(options => options.AddPolicy("Paid",
				builder => builder.AddRequirements(new SubscriptionRequirements())));
			builder.Services.AddScoped<IAuthorizationHandler, SubscriptionHandler>();

			builder.Services.AddScoped<IDbInitializer, DbInitializer>();
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			builder.Services.AddScoped<IServiceManager, ServiceManger>();

			builder.Services.AddAutoMapper(typeof(Service.AssemblyReference).Assembly);
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			await DataSeeding(app);

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseStaticFiles();

			app.UseHttpsRedirection();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();

			app.UseMiddleware<ExceptionHandlerMiddleware>();

			app.Run();
		}
		static async Task DataSeeding(WebApplication app)
		{
			// Create Scope 
			using var scope = app.Services.CreateScope();
			// Inject
			var initDb = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
			// Call Initializer 
			// Initialization Data Base
			await initDb.InitializeAsync();
			//await initDb.InitializeIdentityAsync();
		}
	}
}
