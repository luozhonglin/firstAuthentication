namespace FirstAuthentication.Data
{
    public static class UserDatas
    {
        internal static readonly IEnumerable<User> UserList = new User[]
   {
        new(){UserName="admin", Password="123456", Level=5},
        new(){UserName="kitty", Password="112211", Level=3},
        new(){UserName="bob",Password="215215", Level=2},
        new(){UserName="billy", Password="886600", Level=1}
   };

        // 获取所有用户
        public static IEnumerable<User> GetUsers() => UserList;

        // 根据用户名和密码校对后返回的用户实体
        public static User? CheckUser(string username, string passwd)
        {
            return UserList.FirstOrDefault(u => u.UserName!.Equals(username, StringComparison.OrdinalIgnoreCase) && u.Password == passwd);
        }
    }
}
