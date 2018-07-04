using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1App.Classes
{
	class MenuManager
	{
		private string greeting = "\t\tWelcome to query generator!";
		private string menuItems = "Choose a query that you'd like to do and \n press the necessary key button:\n\n Press \"A\" - QUERY1\n Press \"B\" - QUERY2\n Press \"C\" - QUERY3\n Press \"D\" - QUERY4\n Press \"E\" - QUERY5\n Press \"F\" - QUERY6\n Press \"Esc\" - EXIT";
		private Func<IEnumerable<SomeEntity>> getDataDelegate;

		public MenuManager(Func<IEnumerable<SomeEntity>> getDataDelegate)
		{
			if (getDataDelegate != null)
				this.getDataDelegate = getDataDelegate;
		}

		public void ShowMenu()
		{
			do
			{
				Console.Clear();
				Console.WriteLine(greeting);
				Console.WriteLine(menuItems);
				ConsoleKey pressedKey = Console.ReadKey().Key;
				//var data = getDataDelegate.Invoke
				int userId;
				switch (pressedKey)
				{
					case ConsoleKey.A:
						Console.Clear();
						userId = EnterUserId();
						Query1(userId);
						Console.ReadKey();
						break;
					case ConsoleKey.B:
						Console.Clear();
						userId = EnterUserId();
						Query2(userId);
						Console.ReadKey();
						break;
					case ConsoleKey.C:
						Console.Clear();
						userId = EnterUserId();
						Query3(userId);
						Console.ReadKey();
						break;
					case ConsoleKey.D:
						Console.Clear();
						Query4();
						Console.ReadKey();
						break;
					case ConsoleKey.E:
						Console.Clear();
						userId = EnterUserId();
						var u = Query5(userId);
						Console.ReadKey();
						break;
					case ConsoleKey.Escape:
						return;
					case ConsoleKey.F:
						Console.Clear();
						var postId = EnterPostId();
						var p = Query6(postId);
						Console.ReadKey();
						break;
				}
			} while (true);
		}


		private int EnterUserId()
		{
			int id = -1;
			do
			{
				Console.Clear();
				Console.WriteLine("\tEnter user Id:");
				string resultString = Console.ReadLine();

				if (Int32.TryParse(resultString, out id) && id > 0)
				{
					var isExist = getDataDelegate.Invoke().Select(u => u).Where(i => i.Id == id);
					if (isExist != null && isExist.Count() > 0)
					{
						break;
					}

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

				if (Int32.TryParse(resultString, out id) && id > 0)
				{
					var isExist = getDataDelegate.Invoke().SelectMany(u => u.Posts).Where(i => i.Id == id);
					if (isExist != null && isExist.Count() > 0)
					{
						break;
					}

				}
				else
				{
					Console.WriteLine("There is no such post in this data source. Try again.");
				}

			} while (true);
			return id;
		}
		//Tasks
		//Query1: Получить количество комментов под постами конкретного пользователя (по айди) (список из пост-количество)
		private void Query1(int userId)
		{
			try
			{
				var data = getDataDelegate.Invoke();
				var subresult = data.SelectMany(u => u.Posts.Where(p => p.UserId == userId));
				if (subresult != null && subresult.Count() > 0)
				{
					var result = subresult.Select(p => new { PostItem = p, CommentCount = (int)p.Comments.Count() });
					foreach (var item in result)
					{
						Console.WriteLine($"{item.PostItem.CreatedAt} - {item.CommentCount}");
					}
				}
				else
				{
					Console.WriteLine("Result: 0");
				}
			}
			catch (Exception ex)
			{
				Console.Clear();
				Console.WriteLine(ex.Message);
				Console.ReadKey();
			}
		}
		//Query2:Получить список комментов под постами конкретного пользователя (по айди), где body коммента < 50 символов (список из комментов)
		private void Query2(int userId)
		{

			try
			{
				var data = getDataDelegate.Invoke();
				var result = data.Select(u => u).Where(u => u.Id == userId).SelectMany(p => p.Posts).SelectMany(o => o.Comments.Where(i => i.Body.Length < 50));
				if (result == null || result.Count() <= 0)
				{ Console.WriteLine("Result: 0"); return; }
				foreach (var item in result)
				{
					Console.WriteLine(item + $"\n");
				}
			}
			catch (Exception ex)
			{
				Console.Clear();
				Console.WriteLine(ex.Message);
				Console.ReadKey();
			}


		}
		//Query3:Получить список (id, name) из списка todos которые выполнены для конкретного пользователя (по айди)
		private void Query3(int userId)
		{
			try
			{
				var data = getDataDelegate.Invoke();
				var result = data.Select(u => u).Where(u => u.Id == userId).SelectMany(t => t.Todos.Where(e => e.IsComplete == true)).Select(r => new { r.Id, r.Name });
				if (result == null || result.Count() <= 0)
				{ Console.WriteLine("Result: 0"); return; }
				foreach (var item in result)
				{
					Console.WriteLine($"Id: {item.Id}  Name: {item.Name}\n");
				}
			}
			catch (Exception ex)
			{
				Console.Clear();
				Console.WriteLine(ex.Message);
				Console.ReadKey();
			}
		}
		//Query4:Получить список пользователей по алфавиту (по возрастанию) с отсортированными todo items по длине name (по убыванию)
		private void Query4()
		{
			try
			{
				var data = getDataDelegate.Invoke();
				var result = data.Select(u => u).OrderBy(e => e.Name);
				//SelectMany(u => u.Todos).OrderByDescending(t => t.Name.Length)

				if (result == null || result.Count() <= 0)
				{ Console.WriteLine("Result: 0"); return; }
				foreach (var item in result)
				{
					Console.WriteLine(item);
				}
			}
			catch (Exception ex)
			{
				Console.Clear();
				Console.WriteLine(ex.Message);
				Console.ReadKey();
			}
		}
		//Query5:Получить следующую структуру (передать Id пользователя в параметры)
		//		User
		//		Последний пост пользователя(по дате)
		//		Количество комментов под последним постом
		//		Количество невыполненных тасков для пользователя
		//		Самый популярный пост пользователя(там где больше всего комментов с длиной текста больше 80 символов)
		//		Самый популярный пост пользователя(там где больше всего лайков)
		private UserStruct Query5(int userId)
		{
			UserStruct user = new UserStruct();
			try
			{
				var data = getDataDelegate.Invoke();
				var usr = data.Select(u => u)
							  .Where(u => u.Id == userId).FirstOrDefault();
				if (usr.Posts != null)
				{
					var lastPost = usr.Posts.OrderByDescending(p => p.CreatedAt).First();
					if (lastPost != null)
					{
						user.LastPost = lastPost;
						if (lastPost.Comments != null)
						{
							user.LastPostCommentsCount = lastPost.Comments.Count();
						}
					}

					user.MostPopularByComms = usr.Posts.Where(p => p.Comments.Count() > 0 && p.Comments.Select(a => a).Where(c => c.Body.Length > 80).Count() > 0).OrderBy(p => p.Comments.Count()).FirstOrDefault();

					user.MostPopularByLikes = usr.Posts.OrderByDescending(p => p.Likes).First();
				}
				if (usr.Todos != null)
				{
					user.UnCompletedTasksCount = usr.Todos.Where(t => t.IsComplete == false).Count();
				}

			}
			catch (Exception ex)
			{
				Console.Clear();
				Console.WriteLine(ex.Message);
				Console.ReadKey();
			}

			Console.WriteLine(user);
			return user;
		}

		//Query6:Получить следующую структуру (передать Id поста в параметры)
		//Пост
		//Самый длинный коммент поста
		//Самый залайканный коммент поста
		//Количество комментов под постом где или 0 лайков или длина текста< 80
		private PostStruct Query6(int postId)
		{
			PostStruct postStruct = new PostStruct();
			try
			{
				var data = getDataDelegate.Invoke();
				FullPost currentPost = data.SelectMany(u => u.Posts)
											 .Where(u => u.Id == postId)
											 .FirstOrDefault();
				postStruct.LongestComm = currentPost.Comments.Select(c => c)
													.OrderBy(c => c.Body.Length)
													.LastOrDefault();
				postStruct.LikestComm = currentPost.Comments.Select(c => c)
												   .OrderBy(c => c.Likes)
												   .LastOrDefault();
				var subresult = currentPost.Comments.Select(c => c)
										   .Where(p => p.Likes == 0 || p.Body.Length < 80).Distinct();
								
				foreach (var item in subresult)
				{
					Console.WriteLine(item);
				}
				postStruct.LowRatedCommsCount = subresult.Count();
			}
			catch (Exception ex)
			{
				Console.Clear();
				Console.WriteLine(ex.Message);
				Console.ReadKey();
			}

			Console.WriteLine(postStruct);
			return postStruct;
		}

	}
}
