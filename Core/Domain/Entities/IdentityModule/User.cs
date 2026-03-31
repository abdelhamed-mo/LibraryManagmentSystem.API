using Domain.Entities.BookModule;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.IdentityModule
{
	public class User : IdentityUser
	{
		public string DisplayName { get; set; }
		public Plan Plan { get; set; }
		public DateTime EndDate { get; set; } = DateTime.UtcNow;

	}
	public enum Plan
	{
		Free, // 14 Days
		OneMonth,
		ThreeMonths,
		SixMonths,
		OneYear
	}
}
