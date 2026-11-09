using citas_medicas.Models;
using System.Text.Json;

namespace citas_medicas;

public partial class ConsultarCitaPage : ContentPage
{
	public ConsultarCitaPage()
	{
		InitializeComponent();
        CargarCitas();
	}

    private void CargarCitas()
    {
        string rutaArchivo = Path.Combine(FileSystem.AppDataDirectory, "citas.json");
        List<Cita> lista = new List<Cita>();

        if (File.Exists(rutaArchivo))
        {
            string json = File.ReadAllText(rutaArchivo);

 
                lista = JsonSerializer.Deserialize<List<Cita>>(json) ?? new List<Cita>();
           
        }


        listaCitas.ItemsSource = lista;
    }
}