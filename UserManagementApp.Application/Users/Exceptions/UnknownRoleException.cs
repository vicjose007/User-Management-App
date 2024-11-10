namespace UserManagementApp.Application.Users.Exceptions;

public class UnknownRoleException : Exception
{
    public override string Message { get; }

    public UnknownRoleException() : base()
    {
        Message = $"El rol no es valido";
    }
}

