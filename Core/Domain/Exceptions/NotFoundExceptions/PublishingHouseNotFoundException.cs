using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.NotFoundExceptions
{
	public class PublishingHouseNotFoundException(Guid id)
		: NotFoundException($"Publishing House with id ({id}) isn't found")
	{
	}
}
