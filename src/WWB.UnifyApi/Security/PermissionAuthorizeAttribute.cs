using Microsoft.AspNetCore.Mvc;

namespace WWB.UnifyApi.Security
{
    public class PermissionAuthorizeAttribute : TypeFilterAttribute
    {
        public PermissionAuthorizeAttribute(string permission) : base(typeof(RequirementPermissionFilter))
        {
            Arguments = new object[] { permission };
        }
    }
}