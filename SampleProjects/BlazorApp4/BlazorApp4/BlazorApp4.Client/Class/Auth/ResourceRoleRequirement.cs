using Microsoft.AspNetCore.Authorization;

namespace Class.Auth
{
    public class ResourceRoleRequirement : IAuthorizationRequirement
    {
        public string PolicyName { get; }

        public ResourceRoleRequirement(string policyName)
        {
            PolicyName = policyName;
        }
    }

}
