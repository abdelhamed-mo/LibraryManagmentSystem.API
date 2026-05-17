using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
	public class UnSubscribedException(DateTime date) : Exception($"User subscription expired at ({date.ToString("yyyy-MM-dd")})")
	{

	}
}
