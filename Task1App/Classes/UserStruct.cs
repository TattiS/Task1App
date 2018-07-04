namespace Task1App.Classes
{
	struct UserStruct
	{
		public FullPost LastPost { get; internal set; }
		public int LastPostCommentsCount { get; internal set; }
		public FullPost MostPopularByComms { get; internal set; }
		public FullPost MostPopularByLikes { get; internal set; }
		public int UnCompletedTasksCount { get; internal set; }
	}
}