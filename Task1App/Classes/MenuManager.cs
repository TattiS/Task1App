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
		private string menuItems = "Choose a query that you'd like to do and \n press the necessary key button:\n\n Press \"A\" - QUERY1:(список из пост-количество)\n Press \"B\" - QUERY2:(список из комментов)\n Press \"C\" - QUERY3:(список todos (id, name))\n Press \"D\" - QUERY4:(список пользователей по алфавиту)\n Press \"E\" - QUERY5:(структуру UserStruct)\n Press \"F\" - QUERY6(структуру PostStruct)\n Press \"Esc\" - EXIT";
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
				int userId;
				int postId;
				switch (pressedKey)
				{
					case ConsoleKey.A:
						Console.Clear();
						userId = EnterUserId();
						var result1 = Query1(userId);
						Console.ReadKey();
						break;
					case ConsoleKey.B:
						Console.Clear();
						userId = EnterUserId();
						var result2 = Query2(userId);
						Console.ReadKey();
						break;
					case ConsoleKey.C:
						Console.Clear();
						userId = EnterUserId();
						var result3 = Query3(userId);
						Console.ReadKey();
						break;
					case ConsoleKey.D:
						Console.Clear();
						var result4 = Query4();
						Console.ReadKey();
						break;
					case ConsoleKey.E:
						Console.Clear();
						userId = EnterUserId();
						var u = Query5(userId);
						Show(u);
						Console.ReadKey();
						break;
					case ConsoleKey.Escape:
						return;
					case ConsoleKey.F:
						Console.Clear();
						postId = EnterPostId();
						var p = Query6(postId);
						Show(p);
						Console.ReadKey();
						break;
					case ConsoleKey.J:
						Console.Clear();
						Show(getDataDelegate.Invoke());
						Console.ReadKey();
						break;
					case ConsoleKey.K:
						Console.Clear();
						userId = EnterUserId();
						Show(getDataDelegate.Invoke().Where(o=>o.Id == userId).First());
						Console.ReadKey();
						break;
					case ConsoleKey.L:
						Console.Clear();
						postId = EnterPostId();
						Show(getDataDelegate.Invoke().SelectMany(k=>k.Posts).Where(o => o.Id == postId).First());
						Console.ReadKey();
						break;
				}
			} while (true);
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

				if (Int32.TryParse(resultString, out id) && id > 0)
				{
					var isExist = getDataDelegate.Invoke().Where(i => i.Id == id);
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
		private IEnumerable<Query1Result> Query1(int userId)
		{
			IEnumerable<Query1Result> result = null;
			
			try
			{
				var data = getDataDelegate.Invoke();
				var subresult = data.SelectMany(u => u.Posts.Where(p => p.UserId == userId));
				if (subresult != null && subresult.Count() > 0)
				{
					result = subresult.Select(p => new Query1Result { PostItem = p, CommentCount = p.Comments.Count() });

					foreach (var item in result)
					{
						Console.WriteLine($"Post: {item.PostItem} \n- Number of comments: {item.CommentCount}");
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
			return result;
		}
		
		//Query2:Получить список комментов под постами конкретного пользователя (по айди), где body коммента < 50 символов (список из комментов)
		private IEnumerable<Comment> Query2(int userId)
		{
			IEnumerable<Comment> result = null;
			try
			{
				var data = getDataDelegate.Invoke();
				result = data.Where(u => u.Id == userId).SelectMany(p => p.Posts).SelectMany(o => o.Comments.Where(i => i.Body.Length < 50));

				if (result != null && result.Count() > 0)
				{
					foreach (var item in result)
					{
						Console.WriteLine(item + $"\n");
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

			return result;
		}
		
		//Query3:Получить список (id, name) из списка todos которые выполнены для конкретного пользователя (по айди)
		private IEnumerable<Query3Result> Query3(int userId)
		{
			IEnumerable<Query3Result> result = null;
			try
			{
				var data = getDataDelegate.Invoke();
				result = data.Where(u => u.Id == userId).SelectMany(t => t.Todos.Where(e => e.IsComplete == true)).Select(r => new Query3Result{ Id= r.Id, Name= r.Name });

				if (result != null && result.Count() > 0)
				{
					foreach (var item in result)
					{
						Console.WriteLine($"Id: {item.Id}  Name: {item.Name}\n");
					}
				}
				else
				{ Console.WriteLine("Result: 0"); }
			}
			catch (Exception ex)
			{
				Console.Clear();
				Console.WriteLine(ex.Message);
				Console.ReadKey();
			}
			return result;
		}
		
		//Query4:Получить список пользователей по алфавиту (по возрастанию) с отсортированными todo items по длине name (по убыванию)
		private IEnumerable<SomeEntity> Query4()
		{
			IEnumerable<SomeEntity> result = null;
			try
			{
				var data = getDataDelegate.Invoke();
				var subresult = data.OrderBy(e => e.Name).ToList();
				result = subresult.GroupJoin(subresult.SelectMany(p => p.Todos)
														  .OrderByDescending(t => t.Name.Length), 
												 r => r.Id, t => t.UserId, 
												 (u,j) => new SomeEntity{Id = u.Id,
																		 Name =u.Name,
																		 Avatar =u.Avatar,
																		 CreatedAt =u.CreatedAt,
																		 Email =u.Email,
																		 MyAddress =u.MyAddress,
																		 Posts =u.Posts,
																		 Todos =j}
												 );

				if (result != null && result.Count() > 0)
				{
					foreach (var item in result)
					{
						Console.WriteLine(item);
					}
				}
				else
				{ Console.WriteLine("Result: 0"); }
			}
			catch (Exception ex)
			{
				Console.Clear();
				Console.WriteLine(ex.Message);
				Console.ReadKey();
			}
			return result;
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
				SomeEntity usr = data.Where(u => u.Id == userId).FirstOrDefault();
				List<FullPost> posts = usr.Posts.ToList();

				if (posts != null && posts.Count()>0)
				{
					FullPost lastPost = posts.OrderByDescending(p => p.CreatedAt).First();
					if (lastPost != null)
					{
						user.LastPost = lastPost;

						if (lastPost.Comments != null)
						{
							user.LastPostCommentsCount = lastPost.Comments.Count();
						}
					}

					List<FullPost> subresult = posts.OrderByDescending(p => p.Comments.Count())
														.Where(c => c.Comments.Where(e => e.Body.Length > 80).Count() > 0)
														.ToList();

					if (subresult != null && subresult.Count() > 0)
					{
						user.MostPopularByComms = subresult.GroupJoin(subresult.SelectMany(c => c.Comments),
																	  p => p.Id,
																	  c => c.PostId,
																	  (p, cl) => new
																	  {
																		  PostItem = p,
																		  CommsCounts = cl.Select(a => a.Body)
																						  .Where(b => b.Length > 80)
																						  .Count()
																	  }
																	  ).OrderBy(v => v.CommsCounts)
																	  .Last().PostItem;
					}



					user.MostPopularByLikes = posts.OrderByDescending(p => p.Likes).First();
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
				postStruct.LongestComm = currentPost.Comments
													.OrderBy(c => c.Body.Length)
													.LastOrDefault();
				postStruct.LikestComm = currentPost.Comments
												   .OrderBy(c => c.Likes)
												   .LastOrDefault();
				List<Comment> subresult = currentPost.Comments
										   .Where(p => p.Likes == 0 || p.Body.Length < 80).ToList();

				Console.WriteLine($"Current: {currentPost} \nAll comments: ");

				foreach (var item in currentPost.Comments)
				{
					Console.WriteLine(item);
				}
				Console.WriteLine("\nSelected comments");
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
			
			return postStruct;
		}

	}
}
