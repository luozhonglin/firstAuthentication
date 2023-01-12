using Microsoft.AspNetCore.Authorization;

namespace FirstAuthentication
{
    public class LevelAuthorizationRequirement: IAuthorizationRequirement
    {
        public int Level { get; private set; }

        public LevelAuthorizationRequirement(int lv)
        {
            Level = lv;
        }
    }
}
