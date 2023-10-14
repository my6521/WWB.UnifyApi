namespace WWB.UnifyApi.Security
{
    public interface IPermissionValidator
    {
        PermissionValidResult Valid(string permission);
    }
}