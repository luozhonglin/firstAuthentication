namespace FirstAuthentication.Data
{
    public class User
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }

        /// <summary>
        /// 用户等级，1-5
        /// </summary>
        public int Level { get; set; } = 1;
    }
}
