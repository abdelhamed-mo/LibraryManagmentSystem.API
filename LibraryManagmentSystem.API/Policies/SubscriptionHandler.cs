using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Data;
using System.Security.Claims;

namespace LibraryManagementSystem.API.Policies
{
	public class SubscriptionHandler(StoreContext context) : AuthorizationHandler<SubscriptionRequirements>
	{
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext _context, SubscriptionRequirements requirement)
		{
			var userId = _context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			if (userId == null) return Task.CompletedTask;

			var user = context.Users.FirstOrDefault(u => u.Id == userId);

			if (user is null) return Task.CompletedTask;

			var isSubscribed = user.EndDate > DateTime.UtcNow;

			if (isSubscribed) _context.Succeed(requirement);

			return Task.CompletedTask;

		}
	}
}
