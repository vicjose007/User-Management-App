namespace UserManagementApp.Application.Users.Exceptions;

public class UserDoesNotExistsException : Exception
{
    public override string Message { get; }

    public UserDoesNotExistsException() : base()
    {
        Message = "El Usuario no existe";
    }
}
