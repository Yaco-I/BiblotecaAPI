namespace Biblioteca.API
{
    public class ApplicationConstants
    {
        public const string AdminClaim = "esAdmin";
        public const string ClienteClaim = "EsCliente";
        public const string EmpleadoClaim = "EsEmpleado";
        public const string AdminOrEmpleadoClaim = "EsAdminOrEmpleado";
        public const string AdminOrClienteClaim = "EsAdminOrCliente";
        public const string EmpleadoOrClienteClaim = "EsEmpleadoOrCliente";
        public const string AllClaims = "EsAdminOrEmpleadoOrCliente";

    }
}
