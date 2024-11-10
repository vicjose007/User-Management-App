namespace UserManagementApp.Application.Users.Exceptions;

public class PhoneDoesNotExistsException : Exception
{
    public override string Message { get; }

    public PhoneDoesNotExistsException() : base()
    {
        Message = "El Usuario no existe";
    }
}
