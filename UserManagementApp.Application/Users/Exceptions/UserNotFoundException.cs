namespace UserManagementApp.Application.Users.Exceptions;

public class UserNotFoundException : Exception
{
    public override string Message { get; }

    public UserNotFoundException(string id) : base()
    {
        Message = $"Usuario no encontrado con este id: {id}";
    }
}

public class UserEmailNotFoundException : Exception
{
    public override string Message { get; }

    public UserEmailNotFoundException(string email) : base()
    {
        Message = $"Usuario no encontrado con este correo: {email}";
    }
}
