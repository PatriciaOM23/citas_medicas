namespace Citas.Models;

// Datos de una cita solicitada. Se guarda en el fichero citas.json
public class Cita
{
    public string Nombre { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string NumPaciente { get; set; } = string.Empty;
    public string Turno { get; set; } = string.Empty;
    public string Especialidad { get; set; } = string.Empty;
    public string Doctor { get; set; } = string.Empty;
    public int DiasEspera { get; set; }
    public DateTime FechaSolicitud { get; set; }
}
