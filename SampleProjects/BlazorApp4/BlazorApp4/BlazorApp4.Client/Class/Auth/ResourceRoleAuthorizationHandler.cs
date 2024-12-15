using Microsoft.AspNetCore.Authorization;

namespace Class.Auth
{
    public class ResourceRoleAuthorizationHandler : AuthorizationHandler<ResourceRoleRequirement, MenuResource>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            ResourceRoleRequirement requirement,
            MenuResource resource)
        {
            // Access the policy name
            var policyName = requirement.PolicyName; //ReadWrite; Read;

            // Check if the user is in any of the roles that define ownership of the resource
            if (resource.OwnerRoles.Any(
                role => context.User.IsInRole(role.RoleName) 
                && role.PermissionType.Any(p => p == policyName)
                ))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

}
