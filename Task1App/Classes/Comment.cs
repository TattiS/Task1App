using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1App.Classes
{
	class Comment:IEquatable<Comment>
	{
		public int Id { get; set; }
		public DateTime CreatedAt { get; set; }
		public string Body { get; set; }
		public int UserId { get; set; }
		public int PostId { get; set; }
		public int Likes { get; set; }

		public bool Equals(Comment other)
		{
			if (Object.ReferenceEquals(other, null)) return false;

			if (Object.ReferenceEquals(this, other)) return true;

			return Id.Equals(other.Id) && CreatedAt.Equals(other.CreatedAt);
		}

		public override int GetHashCode()
		{
			int hashId = Id.GetHashCode();
			int hashBody = Body == null ? 0 : Body.GetHashCode();
			int hashUser = UserId.GetHashCode();
			return hashBody ^ hashId * hashUser;
		}

		public override string ToString()
		{
			return $"CreatedAt:{CreatedAt.ToShortDateString()} - UserId:{UserId} - Length:{Body.Length} - Likes:{Likes} - PostId:{PostId}";
		}
	}
}
