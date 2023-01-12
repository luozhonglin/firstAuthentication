using Microsoft.AspNetCore.Authorization;

namespace FirstAuthentication
{
    public class DefaultAuthorizationEvaluator: IAuthorizationEvaluator
    {
        public AuthorizationResult Evaluate(AuthorizationHandlerContext context)
        => context.HasSucceeded
            ? AuthorizationResult.Success()
            : AuthorizationResult.Failed(context.HasFailed
                ? AuthorizationFailure.Failed(context.FailureReasons)
                : AuthorizationFailure.Failed(context.PendingRequirements));
    }
}
