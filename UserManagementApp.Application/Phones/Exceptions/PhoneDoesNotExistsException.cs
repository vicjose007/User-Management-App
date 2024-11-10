namespace UserManagementApp.Application.Phones.Exceptions;

public class PhoneDoesNotExistsException : Exception
{
    public override string Message { get; }

    public PhoneDoesNotExistsException() : base()
    {
        Message = "El telefono no existe";
    }
}
