using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Task1App.Classes;

namespace Task1App.Service
{
	static class QueryService
	{
		private static readonly string[] endpoints = { "users", "posts", "comments", "todos", "address" };
		private static readonly string baseAddress = "https://5b128555d50a5c0014ef1204.mockapi.io/";

		public static IEnumerable<SomeEntity> GetEntities()
		{
			using (WebClient webClient = new WebClient())
			{
				webClient.BaseAddress = baseAddress;
				string usersStr = webClient.DownloadString(endpoints[0]);
				string postsStr = webClient.DownloadString(endpoints[1]);
				string commentsStr = webClient.DownloadString(endpoints[2]);
				string todosStr = webClient.DownloadString(endpoints[3]);
				string addressStr = webClient.DownloadString(endpoints[4]);

				List<User> users = JsonConvert.DeserializeObject<List<User>>(usersStr);
				List<Post> posts = JsonConvert.DeserializeObject<List<Post>>(postsStr);
				List<Comment> comments = JsonConvert.DeserializeObject<List<Comment>>(commentsStr);
				List<Todo> todos = JsonConvert.DeserializeObject<List<Todo>>(todosStr);
				List<Address> addresses = JsonConvert.DeserializeObject<List<Address>>(addressStr);
				try
				{
					var entitiesList = from user in users
									   join post in posts on user.Id equals post.UserId into pList
									   let postsList = from p in pList
													   join comment in comments on p.Id equals comment.PostId into cList
													   select new FullPost { Id = p.Id, CreatedAt = p.CreatedAt, Title = p.Title, Body = p.Body, UserId = p.UserId, Likes = p.Likes, Comments = cList }
									   join todo in todos on user.Id equals todo.UserId into tList
									   join comm in comments on user.Id equals comm.UserId into ucList
									   let address = addresses.Find(a => a.UserId == user.Id)
									   select new SomeEntity { Id = user.Id, Name = user.Name, Avatar = user.Avatar, CreatedAt = user.CreatedAt, Email = user.Email, MyAddress = address, Posts = postsList, Todos = tList, Comments = ucList };

					return entitiesList;
				}
				catch (Exception ex)
				{

					throw ex;
				}
			}
		}
		public static List<SomeEntity> GetEntitiesList()
		{
			using (WebClient webClient = new WebClient())
			{
				webClient.BaseAddress = baseAddress;
				string usersStr = webClient.DownloadString(endpoints[0]);
				string postsStr = webClient.DownloadString(endpoints[1]);
				string commentsStr = webClient.DownloadString(endpoints[2]);
				string todosStr = webClient.DownloadString(endpoints[3]);
				string addressStr = webClient.DownloadString(endpoints[4]);

				List<User> users = JsonConvert.DeserializeObject<List<User>>(usersStr);
				List<Post> posts = JsonConvert.DeserializeObject<List<Post>>(postsStr);
				List<Comment> comments = JsonConvert.DeserializeObject<List<Comment>>(commentsStr);
				List<Todo> todos = JsonConvert.DeserializeObject<List<Todo>>(todosStr);
				List<Address> addresses = JsonConvert.DeserializeObject<List<Address>>(addressStr);
				try
				{
					var entitiesList = from user in users
									   join post in posts on user.Id equals post.UserId into pList
									   let postsList = from p in pList
													   join comment in comments on p.Id equals comment.PostId into cList
													   select new FullPost { Id = p.Id, CreatedAt = p.CreatedAt, Title = p.Title, Body = p.Body, UserId = p.UserId, Likes = p.Likes, Comments = cList }
									   join todo in todos on user.Id equals todo.UserId into tList
									   join comm in comments on user.Id equals comm.UserId into ucList
									   let address = addresses.Find(a => a.UserId == user.Id)
									   select new SomeEntity { Id = user.Id, Name = user.Name, Avatar = user.Avatar, CreatedAt = user.CreatedAt, Email = user.Email, MyAddress = address, Posts = postsList, Todos = tList, Comments = ucList };

					return entitiesList.ToList<SomeEntity>();
				}
				catch (Exception ex)
				{

					throw ex;
				}
			}
		}

		public static bool IsUserExist(int id)
		{
			var isExist = GetEntities().Where(i => i.Id == id);
			if (isExist != null && isExist.Count() > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		public static bool IsPostExist(int id)
		{
			var isExist = GetEntities().SelectMany(p=>p.Posts).Where(i => i.Id == id);
			if (isExist != null && isExist.Count() > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		//Tasks
		//Query1: Получить количество комментов под постами конкретного пользователя (по айди) (список из пост-количество)
		public static IEnumerable<Query1Result> Query1(int userId)
		{
			IEnumerable<Query1Result> result = null;

			try
			{
				result = GetEntities().SelectMany(u => u.Posts.Where(p => p.UserId == userId))
									.Select(p => new Query1Result { PostItem = p, CommentCount = p.Comments.Count() });	
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
		public static IEnumerable<Comment> Query2(int userId)
		{
			IEnumerable<Comment> result = null;
			try
			{
				result = GetEntities().Where(u => u.Id == userId)
									.SelectMany(p => p.Posts)
									.SelectMany(o => o.Comments.Where(i => i.Body.Length < 50));
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
		public static IEnumerable<Query3Result> Query3(int userId)
		{
			IEnumerable<Query3Result> result = null;
			try
			{
				result = GetEntities().Where(u => u.Id == userId)
									.SelectMany(t => t.Todos.Where(e => e.IsComplete == true))
									.Select(r => new Query3Result { Id = r.Id, Name = r.Name });
				
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
		public static IEnumerable<SomeEntity> Query4()
		{
			IEnumerable<SomeEntity> result = null;
			try
			{
				var subresult = GetEntities().OrderBy(e => e.Name).ToList();
				result = subresult.GroupJoin(subresult.SelectMany(p => p.Todos)
														  .OrderByDescending(t => t.Name.Length),
												 r => r.Id, t => t.UserId,
												 (u, j) => new SomeEntity
												 {
													 Id = u.Id,
													 Name = u.Name,
													 Avatar = u.Avatar,
													 CreatedAt = u.CreatedAt,
													 Email = u.Email,
													 MyAddress = u.MyAddress,
													 Posts = u.Posts,
													 Todos = j
												 }
												 );
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
		public static UserStruct Query5(int userId)
		{
			UserStruct user = new UserStruct();
			try
			{
				SomeEntity usr = GetEntities().Where(u => u.Id == userId).FirstOrDefault();
				List<FullPost> posts = usr?.Posts.ToList();

				user.LastPost = posts?.OrderByDescending(p => p.CreatedAt).FirstOrDefault();

				user.LastPostCommentsCount = posts?.OrderByDescending(p => p.CreatedAt).FirstOrDefault()?.Comments.Count() ?? 0;

				user.MostPopularByComms = posts?.Where(p => p.Comments.Count() > 0)
									.Select(a => new { CurrentPost = a, CommentsCount = a.Comments.Where(c => c.Body.Length > 80).Count() })
									.OrderByDescending(i => i.CommentsCount)
									.Where(i => i.CommentsCount > 0)
									.FirstOrDefault()?.CurrentPost;
					
				user.MostPopularByLikes = posts?.OrderByDescending(p => p.Likes).FirstOrDefault();

				user.UnCompletedTasksCount = usr.Todos.Where(t => t.IsComplete == false).Count();
				
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
		public static PostStruct Query6(int postId)
		{
			PostStruct postStruct = new PostStruct();
			try
			{
				var data = GetEntities();
				FullPost currentPost = data.SelectMany(u => u.Posts)
											 .Where(u => u.Id == postId)
											 .FirstOrDefault();
				postStruct.LongestComm = currentPost.Comments
													.OrderBy(c => c.Body.Length)
													.LastOrDefault();
				postStruct.LikestComm = currentPost.Comments
												   .OrderBy(c => c.Likes)
												   .LastOrDefault();
				postStruct.LowRatedCommsCount = currentPost.Comments
										   .Where(p => p.Likes == 0 || p.Body.Length < 80).ToList().Count();

					
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
