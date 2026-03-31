
using Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;

namespace Persistence.Repositories
{
	public class DbInitializer : IDbInitializer
	{
		readonly StoreContext storeContext;
		readonly UserManager<User> userManager;
		readonly RoleManager<IdentityRole> roleManager;

		public DbInitializer(StoreContext storeContext, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
		{
			this.storeContext = storeContext;
			this.roleManager = roleManager;
			this.userManager = userManager;
		}

		public async Task InitializeAsync()
		{
			try
			{
				var PendingMigrations = await storeContext.Database.GetPendingMigrationsAsync();
				// Check Pending Migrations
				if (PendingMigrations.Any())
				{
					await storeContext.Database.MigrateAsync();
				}

				// Seed Default Roles
				if (!roleManager.Roles.Any())
				{
					await roleManager.CreateAsync(new IdentityRole("Reader"));
					await roleManager.CreateAsync(new IdentityRole("Admin"));
				}
				// Seed Default Users
				if (!userManager.Users.Any())
				{
					var reader = new User()
					{
						UserName = "Reader",
						DisplayName = "Reader",
						Email = "library.reader@gmail.com",
						PhoneNumber = "01122334455",
					};
					var admin = new User()
					{
						UserName = "Admin",
						DisplayName = "Administrator",
						Email = "library.admin@gmail.com",
						PhoneNumber = "01112223334",
					};

					var res1 = await userManager.CreateAsync(reader, "Reader123");
					var res2 = await userManager.CreateAsync(admin, "Admin123");
					if (res1.Succeeded && res2.Succeeded)
					{
						await userManager.AddToRoleAsync(reader, "Reader");
						await userManager.AddToRoleAsync(admin, "Admin");
					}
				}
					// Authors
					if (!storeContext.Authors.Any())
				{
					var authorsFile = await File.ReadAllTextAsync(@"..\Infrastructure\Persistance\Data\Seeding\Authors.json");
					var options = new JsonSerializerOptions
					{
						PropertyNameCaseInsensitive = true,
						Converters = { new JsonStringEnumMemberConverter<Gender>() }
					};
					var authors = JsonSerializer.Deserialize<List<Author>>(authorsFile,options);
					if (authors is not null && authors.Any())
					{
						await storeContext.Authors.AddRangeAsync(authors);
						await storeContext.SaveChangesAsync();
					}
				}
				// PublishingHouses
				if (!storeContext.PublishingHouses.Any())
				{
					var housesFile = await File.ReadAllTextAsync(@"..\Infrastructure\Persistance\Data\Seeding\PublishingHouses.json");
					var options = new JsonSerializerOptions
					{
						PropertyNameCaseInsensitive = true
					};
					var houses = JsonSerializer.Deserialize<List<PublishingHouse>>(housesFile);
					if (houses is not null && houses.Any())
					{
						await storeContext.PublishingHouses.AddRangeAsync(houses);
						await storeContext.SaveChangesAsync();
					}
				}
				// Books
				if (!storeContext.Books.Any())
				{
					var booksFile = await File.ReadAllTextAsync(@"..\Infrastructure\Persistance\Data\Seeding\Books.json");
					var options = new JsonSerializerOptions
					{
						PropertyNameCaseInsensitive = true
					};
					var books = JsonSerializer.Deserialize<List<Book>>(booksFile);
					if (books is not null && books.Any())
					{
						await storeContext.Books.AddRangeAsync(books);
						await storeContext.SaveChangesAsync();
					}
				}

				// Save Changes
			}
			catch (Exception)
			{

				throw;
			}
		}

		//public async Task InitializeIdentityAsync()
		//{
		//	try
		//	{
		//		// Seed Default Roles
		//		if (!roleManager.Roles.Any())
		//		{
		//			await roleManager.CreateAsync(new IdentityRole("Reader"));
		//			await roleManager.CreateAsync(new IdentityRole("Admin"));
		//		}
		//		// Seed Default Users
		//		if (!userManager.Users.Any())
		//		{
		//			var reader = new User()
		//			{
		//				UserName = "Reader",
		//				DisplayName = "Reader",
		//				Email = "library.reader@gmail.com",
		//				PhoneNumber = "01122334455",
		//			};
		//			var admin = new User()
		//			{
		//				UserName = "Admin",
		//				DisplayName = "Administrator",
		//				Email = "library.admin@gmail.com",
		//				PhoneNumber = "01112223334",
		//			};

		//			var res1 = await userManager.CreateAsync (reader,"Reader123");
		//			var res2 = await userManager.CreateAsync (admin, "Admin123");
		//			if (res1.Succeeded && res2.Succeeded)
		//			{
		//				await userManager.AddToRoleAsync(reader, "Reader");
		//				await userManager.AddToRoleAsync(admin, "Admin");
		//			}
		//		}
		//	}
		//	catch (Exception)
		//	{

		//		throw;
		//	}
		//}
	}

	public class JsonStringEnumMemberConverter<T> : JsonConverter<T> where T : struct, Enum
	{
		public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			string? enumText = reader.GetString();

			foreach (T enumValue in Enum.GetValues(typeof(T)))
			{
				var memberInfo = typeof(T).GetMember(enumValue.ToString());
				var enumMemberAttr = memberInfo[0].GetCustomAttribute<EnumMemberAttribute>();
				if (enumMemberAttr?.Value == enumText)
				{
					return enumValue;
				}
			}

			throw new JsonException($"Unable to convert \"{enumText}\" to Enum \"{typeof(T)}\"");
		}

		public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
		{
			var memberInfo = typeof(T).GetMember(value.ToString());
			var enumMemberAttr = memberInfo[0].GetCustomAttribute<EnumMemberAttribute>();
			var enumString = enumMemberAttr?.Value ?? value.ToString();
			writer.WriteStringValue(enumString);
		}
	}

}
