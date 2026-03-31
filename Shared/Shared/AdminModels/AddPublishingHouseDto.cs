using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.AdminModels
{
	public record AddPublishingHouseDto
	{
		public string Name { get; set; }
		public string Location { get; set; }
	}
}
