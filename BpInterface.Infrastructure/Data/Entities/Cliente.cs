using BpInterface.Core.Models;

namespace BpInterface.Infrastructure.Data.Entities
{
    public class Cliente : Persona
    {
        public Cliente()
        {
            CuentasBancarias = new HashSet<Cuenta>();
        }

        public string Contrasenia { get; set; } = null!;
        public bool Estado { get; set; }
        public virtual ICollection<Cuenta> CuentasBancarias { get; set; }
    }
}
