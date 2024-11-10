namespace UserManagementApp.Application.Users.Exceptions;

public class UserRoleException : Exception
{
    public override string Message { get; }

    public UserRoleException(string role) : base()
    {
        Message = $"El rol: {role} no es valido";
    }
}
