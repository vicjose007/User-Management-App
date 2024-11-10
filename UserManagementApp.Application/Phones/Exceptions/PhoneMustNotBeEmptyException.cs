namespace UserManagementApp.Application.Phones.Exceptions;

public class PhoneMustNotBeEmptyException : Exception
{
    public override string Message { get; }

    public PhoneMustNotBeEmptyException() : base()
    {
        Message = "El numero de telefono no debe estar vacio";
    }
}

