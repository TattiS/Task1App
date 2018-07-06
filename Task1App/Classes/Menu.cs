using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1App.Service;

namespace Task1App.Classes
{
	class Menu
	{
		private string greeting = "\t\tWelcome to query generator!";
		private string menuItems = "Choose a query that you'd like to do and \n press the necessary key button:\n\n Press \"A\" - QUERY1:(список из пост-количество)\n Press \"B\" - QUERY2:(список из комментов)\n Press \"C\" - QUERY3:(список todos (id, name))\n Press \"D\" - QUERY4:(список пользователей по алфавиту)\n Press \"E\" - QUERY5:(структуру UserStruct)\n Press \"F\" - QUERY6:(структуру PostStruct)\n Press \"J\" - QUERY7:(список всех сущностей)\n Press \"K\" - QUERY8:(пользователь по id)\n Press \"L\" - QUERY9:(пост по id)\n Press \"Esc\" - EXIT";
		

		public void ShowMenu()
		{
			do
			{
				Console.Clear();
				Console.WriteLine(greeting);
				Console.WriteLine(menuItems);
				ConsoleKey pressedKey = Console.ReadKey().Key;
				int userId;
				int postId;
				switch (pressedKey)
				{
					case ConsoleKey.A:
						Console.Clear();
						userId = EnterUserId();
						var result1 = QueryService.Query1(userId);
						Show(result1);
						Console.ReadKey();
						break;
					case ConsoleKey.B:
						Console.Clear();
						userId = EnterUserId();
						var result2 = QueryService.Query2(userId);
						Show(result2);
						Console.ReadKey();
						break;
					case ConsoleKey.C:
						Console.Clear();
						userId = EnterUserId();
						var result3 = QueryService.Query3(userId);
						Show(result3);
						Console.ReadKey();
						break;
					case ConsoleKey.D:
						Console.Clear();
						var result4 = QueryService.Query4();
							Show(result4);
						Console.ReadKey();
						break;
					case ConsoleKey.E:
						Console.Clear();
						userId = EnterUserId();
						var u = QueryService.Query5(userId);
						Show(u);
						Console.ReadKey();
						break;
					case ConsoleKey.Escape:
						return;
					case ConsoleKey.F:
						Console.Clear();
						postId = EnterPostId();
						var p = QueryService.Query6(postId);
						Show(p);
						Console.ReadKey();
						break;
					case ConsoleKey.J:
						Console.Clear();
						Show(QueryService.GetEntitiesList());
						Console.ReadKey();
						break;
					case ConsoleKey.K:
						Console.Clear();
						userId = EnterUserId();
						Show(QueryService.GetEntitiesList().Where(o=>o.Id == userId).First());
						Console.ReadKey();
						break;
					case ConsoleKey.L:
						Console.Clear();
						postId = EnterPostId();
						Show(QueryService.GetEntitiesList().SelectMany(k=>k.Posts).Where(o => o.Id == postId).First());
						Console.ReadKey();
						break;
				}
			} while (true);
		}

		private void Show(IEnumerable<Query3Result> result3)
		{
			if (result3 != null && result3.Count() > 0)
			{
				foreach (var item in result3)
				{
					Show(item);
				}
			}
			else
			{ Console.WriteLine("Result: 0"); }
		}
		private void Show(IEnumerable<Comment> result2)
		{
			if (result2 != null && result2.Count() > 0)
			{
				foreach (var item in result2)
				{
					Console.WriteLine(item);
				}
			}
			else
			{ Console.WriteLine("Result: 0"); }
		}
		private void Show(IEnumerable<Query1Result> result1)
		{
			if (result1 != null && result1.Count() > 0)
			{
				foreach (var item in result1)
				{
					Console.WriteLine($"Post: {item.PostItem}");
					Console.WriteLine($"The number of comments: {item.CommentCount}");
				}
			}
			else
			{ Console.WriteLine("Result: 0"); }
		}
		private void Show(FullPost fullPost)
		{
			if (fullPost != null)
			{
				Console.WriteLine(fullPost);
				if (fullPost.Comments != null && fullPost.Comments.Count() > 0)
				{
					Console.WriteLine("\tList of comments:");
					foreach (var item in fullPost.Comments)
					{
						Console.WriteLine(item);
					}
				}
			}
			else
			{
				Console.WriteLine("Result: 0");
			}
		}
		private void Show(SomeEntity someEntity)
		{
			if (someEntity != null)
			{
				Console.WriteLine(someEntity);
			}
			else
			{
				Console.WriteLine("Result: 0");
			}
		}
		private void Show(PostStruct post)
		{
			Console.Write("The longest comment: ");
			if (post.LongestComm != null)
			{ Console.WriteLine(post.LongestComm); }
			else
			{ Console.WriteLine("None"); }

			Console.Write("The likest comment: ");
			if (post.LikestComm != null)
			{ Console.WriteLine(post.LikestComm); }
			else
			{ Console.WriteLine("None"); }

			Console.WriteLine($"The number of low-rated comments: {post.LowRatedCommsCount}");
			
		}
		private void Show(UserStruct user)
		{
			Console.Write("The latest post: ");
			if (user.LastPost != null)
			{ Console.WriteLine($"\n{user.LastPost}"); }
			else
			{ Console.WriteLine("None"); }

			Console.WriteLine($"The number of the latest post's comments: {user.LastPostCommentsCount}");

			Console.Write("The most popular post by comments: ");
			if (user.MostPopularByComms != null)
			{ Console.WriteLine($"\n{user.MostPopularByComms}"); }
			else
			{ Console.WriteLine("None"); }

			Console.Write("The most popular post by likes: ");
			if (user.MostPopularByLikes != null)
			{ Console.WriteLine($"\n{user.MostPopularByLikes}"); }
			else
			{ Console.WriteLine("None"); }

			Console.WriteLine($"The number of uncompleted tasks: {user.UnCompletedTasksCount}");
			
		}
		private void Show(IEnumerable <SomeEntity> users)
		{
			if (users != null && users.Count() > 0)
			{
				foreach (var item in users)
				{
					Console.WriteLine(item);
				}
			}
			else
			{ Console.WriteLine("Result: 0"); }
		}
		private void Show(Query3Result instance)
		{
			if (instance != null)
			{				
				Console.WriteLine($"Id: {instance.Id} - Name: {instance.Name}");
			}
			else
			{ Console.WriteLine("Result: 0"); }
		}
		
		private int EnterUserId()
		{
			int id = -1;
			do
			{
				Console.Clear();
				Console.WriteLine("\tEnter user Id:");
				string resultString = Console.ReadLine();

				if (Int32.TryParse(resultString, out id) && id > 0 && QueryService.IsUserExist(id))
				{
					break;
				}
				else
				{
					Console.WriteLine("There is no such user in this data source. Try again.");
				}

			} while (true);
			return id;
		}
		private int EnterPostId()
		{
			int id = -1;
			do
			{
				Console.Clear();
				Console.WriteLine("\tEnter post Id:");
				string resultString = Console.ReadLine();

				if (Int32.TryParse(resultString, out id) && id > 0 && QueryService.IsPostExist(id))
				{
					break;
				}
				else
				{
					Console.WriteLine("There is no such post in this data source. Try again.");
				}

			} while (true);
			return id;
		}
		
				
	}
}
