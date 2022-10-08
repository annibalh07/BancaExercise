namespace BpInterface.Core.Models
{
    public class Persona
    {
        public int Id { get; set; }
        public string Identificacion { get; set; } = null!;
        public string Nombres { get; set; } = null!;
        public string Genero { get; set; } = null!;
        public int Edad { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
    }
}
