using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1App.Classes
{
	class SomeEntity
	{
		public int Id { get; set; }
		public DateTime CreatedAt { get; set; }
		public string Name { get; set; }
		public string Avatar { get; set; }
		public string Email { get; set; }
		public IEnumerable<FullPost> Posts { get; set; }
		public IEnumerable<Todo> Todos { get; set; }
		public Address MyAddress { get; set; }

		public override string ToString()
		{
			string result = $"Id:{Id} Created:{CreatedAt.ToShortDateString()}\nName:{Name}\nEmail:{Email}\nAddress:{MyAddress}\n";
			if (Todos != null && Todos.Count() > 0)
			{
				result += "\tToDos list:\n";
				foreach (var item in Todos)
				{
					result += item.ToString();
				}
				result += "\n";
			}
			if (Posts != null && Posts.Count() > 0)
			{
				result += "\tPosts list:\n";
				foreach (var item in Posts)
				{
					result += item.ToString();
				}
				result += "\n";
			}
			return result;
		}
	}
}
