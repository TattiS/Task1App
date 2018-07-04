using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1App.Classes
{
	class Address
	{
		public int Id { get; set; }
		public string Country { get; set; }
		public string City { get; set; }
		public string Street { get; set; }
		public string Zip { get; set; }
		public int UserId { get; set; }

		public override string ToString()
		{
			return $"{Street}\n{City}\n{Country}\n{Zip}";
		}
	}
}
