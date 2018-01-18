#r "System.Data.Linq"

using System.Data.Linq.Mapping;

[Table(Name = "Contactos")]
public class Contacto
{
    [Column]
    public int id { get; set; }
    [Column]
    public string nombre { get; set; }
    [Column]
    public string apellido { get; set; }
    [Column]
    public string correo { get; set; }
    [Column]
    public string telefono { get; set; }
    [Column]
    public int edad { get; set; }
    [Column]
    public int mesNacimiento { get; set; }
    [Column]
    public int diaNacimiento { get; set; }
    [Column]
    public int anioNacimiento { get; set; }
}