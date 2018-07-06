using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Task1App.Classes;

namespace Task1App
{
	class Program
	{
		//private static Func<IEnumerable<SomeEntity>> GetDataDelegate = GetEntities;
		//private static MenuManager menu = new MenuManager(GetDataDelegate);
		//private static readonly string[] endpoints = { "users", "posts", "comments", "todos", "address" };
		//private static readonly string baseAddress = "https://5b128555d50a5c0014ef1204.mockapi.io/";
		private static Menu menu = new Menu();

		static void Main(string[] args)
		{
			try
			{
				menu.ShowMenu();
			}
			catch (Exception ex)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				
				PushMessage("\t\tERROR!\n\n"+ex.Message);
			}
		
		}


		private static void PushMessage(string message)
		{
			Console.Clear();
			Console.WriteLine(message);
			Console.ReadKey();
		}


		//private static IEnumerable <SomeEntity> GetEntities()
		//{
		//	using (WebClient webClient = new WebClient())
		//	{
		//		webClient.BaseAddress = baseAddress;
		//		string usersStr = webClient.DownloadString(endpoints[0]);
		//		string postsStr = webClient.DownloadString(endpoints[1]);
		//		string commentsStr = webClient.DownloadString(endpoints[2]);
		//		string todosStr = webClient.DownloadString(endpoints[3]);
		//		string addressStr = webClient.DownloadString(endpoints[4]);

		//		List<User> users = JsonConvert.DeserializeObject<List<User>>(usersStr);
		//		List<Post> posts = JsonConvert.DeserializeObject<List<Post>>(postsStr);
		//		List<Comment> comments = JsonConvert.DeserializeObject<List<Comment>>(commentsStr);
		//		List<Todo> todos = JsonConvert.DeserializeObject<List<Todo>>(todosStr);
		//		List<Address> addresses = JsonConvert.DeserializeObject<List<Address>>(addressStr);
		//		try
		//		{
		//			var entitiesList = from user in users
		//							   join post in posts on user.Id equals post.UserId into pList
		//							   let postsList = from p in pList
		//											   join comment in comments on p.Id equals comment.PostId into cList
		//											   select new FullPost { Id = p.Id, CreatedAt = p.CreatedAt, Title = p.Title, Body = p.Body, UserId = p.UserId, Likes = p.Likes, Comments = cList }
		//							   join todo in todos on user.Id equals todo.UserId into tList
		//							   join comm in comments on user.Id equals comm.UserId into ucList
		//							   let address = addresses.Find(a => a.UserId == user.Id)
		//							   select new SomeEntity { Id = user.Id, Name = user.Name, Avatar = user.Avatar, CreatedAt = user.CreatedAt, Email = user.Email, MyAddress = address, Posts = postsList, Todos = tList, Comments = ucList};

		//			return entitiesList; 
		//		}
		//		catch (Exception ex)
		//		{

		//			throw ex;
		//		}
		//	}
		//}
		//private static List<SomeEntity> GetEntitiesList()
		//{
		//	using (WebClient webClient = new WebClient())
		//	{
		//		webClient.BaseAddress = baseAddress;
		//		string usersStr = webClient.DownloadString(endpoints[0]);
		//		string postsStr = webClient.DownloadString(endpoints[1]);
		//		string commentsStr = webClient.DownloadString(endpoints[2]);
		//		string todosStr = webClient.DownloadString(endpoints[3]);
		//		string addressStr = webClient.DownloadString(endpoints[4]);

		//		List<User> users = JsonConvert.DeserializeObject<List<User>>(usersStr);
		//		List<Post> posts = JsonConvert.DeserializeObject<List<Post>>(postsStr);
		//		List<Comment> comments = JsonConvert.DeserializeObject<List<Comment>>(commentsStr);
		//		List<Todo> todos = JsonConvert.DeserializeObject<List<Todo>>(todosStr);
		//		List<Address> addresses = JsonConvert.DeserializeObject<List<Address>>(addressStr);
		//		try
		//		{
		//			var entitiesList = from user in users
		//							   join post in posts on user.Id equals post.UserId into pList
		//							   let postsList = from p in pList
		//											   join comment in comments on p.Id equals comment.PostId into cList
		//											   select new FullPost { Id = p.Id, CreatedAt = p.CreatedAt, Title = p.Title, Body = p.Body, UserId = p.UserId, Likes = p.Likes, Comments = cList }
		//							   join todo in todos on user.Id equals todo.UserId into tList
		//							   join comm in comments on user.Id equals comm.UserId into ucList
		//							   let address = addresses.Find(a => a.UserId == user.Id)
		//							   select new SomeEntity { Id = user.Id, Name = user.Name, Avatar = user.Avatar, CreatedAt = user.CreatedAt, Email = user.Email, MyAddress = address, Posts = postsList, Todos = tList, Comments = ucList};

		//			return entitiesList.ToList<SomeEntity>();
		//		}
		//		catch (Exception ex)
		//		{

		//			throw ex;
		//		}
		//	}
		//}

	}
}
