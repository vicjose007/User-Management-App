namespace UserManagementApp.Application.Phones.Exceptions;

public class PhoneNotFoundException : Exception
{
    public override string Message { get; }

    public PhoneNotFoundException(string id) : base()
    {
        Message = $"Telefono no encontrado con este id: {id}";
    }
}
