using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace FirstAuthentication.Handler
{
    public class LevelAuthorizationHandler : AuthorizationHandler<LevelAuthorizationRequirement>
    {
        // 策略名称，写成常量方便使用
        public const string POLICY_NAME = "Level";

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, LevelAuthorizationRequirement requirement)
        {
            // 查找声明
            Claim? clm = context.User.Claims.FirstOrDefault(c => c.Type == "level");
            if (clm != null)
            {
                // 读出用户等级
                int lv = int.Parse(clm.Value);
                // 看看用户等级是否满足条件
                if (lv >= requirement.Level)
                {
                    // 满足，标记此阶段允许授权
                    context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;
        }
    }
}
