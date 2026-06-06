
namespace Application.Validators;
public static class AuthValidator
{
    public static void ValidateAdminOrOwner(Guid ownerId, Guid currentUserId, string currentUserRole, string message)
    {
        if (currentUserId != ownerId && currentUserRole != "Admin" && currentUserRole != "superadmin")
        {
            throw new Exception(message);
        }
    }


    public static void ValidateOwner(Guid ownerId, Guid currentUserId, string message)
    {
        if (currentUserId != ownerId)
        {
            throw new Exception(message);
        }
    }
    
    
}