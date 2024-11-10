using System.ComponentModel;

namespace UserManagementApp.Domain.Enums
{
    public enum Roles
    {
        [Description("Normal")]
        Normal,
        [Description("Administrador")]
        Admin
    }
}
