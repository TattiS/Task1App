using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1App.Classes
{
	class Comment:IComparable
	{
		public int Id { get; set; }
		public DateTime CreatedAt { get; set; }
		public string Body { get; set; }
		public int UserId { get; set; }
		public int PostId { get; set; }
		public int Likes { get; set; }

		public int CompareTo(object obj)
		{
			return Id.CompareTo(obj);
		}

		public override string ToString()
		{
			return $"CreatedAt:{CreatedAt.ToShortDateString()} - UserId:{UserId} - Length:{Body.Length} - Likes:{Likes} - PostId:{PostId}";
		}
	}
}
