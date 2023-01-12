using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace FirstAuthentication.Handler
{
    public class LevelAuthorizationPolicyProvider : IAuthorizationPolicyProvider
    {
        private readonly AuthorizationOptions _options;

        public LevelAuthorizationPolicyProvider(IOptions<AuthorizationOptions> opt)
        {
            _options = opt.Value;
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            return Task.FromResult(_options.DefaultPolicy);
        }

        public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
        {
            return Task.FromResult(_options.FallbackPolicy);
        }

        public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith(LevelAuthorizationHandler.POLICY_NAME, StringComparison.OrdinalIgnoreCase))
            {
                // 比如，策略名 Level4，得到等级4
                // 提取名称最后的数字
                int prefixLen = LevelAuthorizationHandler.POLICY_NAME.Length;
                if (int.TryParse(policyName.Substring(prefixLen), out int level))
                {
                    // 动态生成策略
                    AuthorizationPolicyBuilder plcyBd = new AuthorizationPolicyBuilder();
                    plcyBd.AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme);
                    plcyBd.AddRequirements(new LevelAuthorizationRequirement(level));
                    // Build 方法生成策略
                    return Task.FromResult(plcyBd.Build())!;
                }
            }
            // 未处理，交由选项类去返回默认的策略
            return Task.FromResult(_options.GetPolicy(policyName));
        }
    }
}
