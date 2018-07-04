using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1App.Classes
{
	class Post
	{
		public int Id { get; set; }
		public DateTime CreatedAt { get; set; }
		public string Title { get; set; }
		public string Body { get; set; }
		public int UserId { get; set; }
		public int Likes { get; set; }

		public override string ToString()
		{
			return $"PostId:{Id} - CreatedAt:{CreatedAt.ToShortDateString()} - Title:{Title} - UserId:{UserId} - Likes:{Likes}\n";
		}
	}

	class FullPost:Post
	{
		public IEnumerable<Comment> Comments { get; set; }
		
		public override string ToString()
		{
			return $"PostId:{Id} - CreatedAt:{CreatedAt.ToShortDateString()} - Title:{Title} - UserId:{UserId} - Likes:{Likes} - Comments:{Comments.Count()}\n";
		}
	}
}
