using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1App.Classes
{
	class Todo
	{
		public int Id { get; set; }
		public DateTime CreatedAt { get; set; }
		public string Name { get; set; }
		public bool IsComplete { get; set; }
		public int UserId { get; set; }

		public override string ToString()
		{
			return $"TodoId:{Id} - Created:{CreatedAt.ToShortDateString()} - Name:{Name} - Completed:{IsComplete} - UserId:{UserId}\n";
		}
	}
}
