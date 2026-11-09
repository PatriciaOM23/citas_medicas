using citas_medicas.Models;
using System.Text.Json;
namespace citas_medicas;

public partial class SolicitarCitaPage : ContentPage
{
	public SolicitarCitaPage()
	{
		InitializeComponent();
		nombreEntry.Text = Preferences.Get("nombre", String.Empty);
		apellidosEntry.Text = Preferences.Get("apellidos", String.Empty);
		pacienteEntry.Text = Preferences.Get("numeroPaciente", String.Empty);
		recordarDatos.IsChecked = Preferences.Get("recordar", false);
        this.listaEspecialidades.ItemsSource = CargarEspecialidades();
	}


    private List<Especialidad> CargarEspecialidades()
    {
        return new List<Especialidad>
     {
         new Especialidad { Imagen = "cardiologia.png",   Nombre = "Cardiología",          Doctor = "Dra. Laura Martín",   DiasEspera = 25 },
         new Especialidad { Imagen = "dermatologia.png",  Nombre = "Dermatología",         Doctor = "Dr. Javier Ruiz",     DiasEspera = 40 },
         new Especialidad { Imagen = "oftalmologia.png",  Nombre = "Oftalmología",         Doctor = "Dra. Elena Gómez",    DiasEspera = 32 },
         new Especialidad { Imagen = "traumatologia.png", Nombre = "Traumatología",        Doctor = "Dr. Carlos Navarro",  DiasEspera = 55 },
         new Especialidad { Imagen = "pediatria.png",     Nombre = "Pediatría",            Doctor = "Dra. Marta Sánchez",  DiasEspera = 7 },
         new Especialidad { Imagen = "neurologia.png",    Nombre = "Neurología",           Doctor = "Dr. Andrés López",    DiasEspera = 60 },
         new Especialidad { Imagen = "ginecologia.png",   Nombre = "Ginecología",          Doctor = "Dra. Sofía Torres",   DiasEspera = 21 },
         new Especialidad { Imagen = "otorrino.png",      Nombre = "Otorrinolaringología", Doctor = "Dr. Pablo Hernández", DiasEspera = 35 }
     };
    }

    private async void listaEspecialidades_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Especialidad especialidad = (Especialidad)e.CurrentSelection[0];
      
    }
    private void SolicitarCitaBtn_Clicked(Object sender, EventArgs e)
	{
        if (string.IsNullOrWhiteSpace(nombreEntry.Text) ||
        string.IsNullOrWhiteSpace(apellidosEntry.Text) ||
        string.IsNullOrWhiteSpace(pacienteEntry.Text)) { 
            DisplayAlertAsync("Rellene todos los campos", "Introduce nombre, apellidos y número de paciente", "Aceptar");
            return;
        } 
        if(turnoEntry.SelectedIndex == -1)
        {
            DisplayAlertAsync("Rellene todos los campos", "Escoja turno: Mañana o tarde", "Aceptar");
            return;
        }
        if(listaEspecialidades.SelectedItem == null)
        {
            DisplayAlertAsync("Rellene todos los campos", "Escoja especialidad", "Aceptar");
            return;
        }
		if (recordarDatos.IsChecked)
		{
			Preferences.Set("nombre", this.nombreEntry.Text.Trim());
			Preferences.Set("apellidos", this.apellidosEntry.Text.Trim());
			Preferences.Set("numeroPaciente", this.pacienteEntry.Text.Trim());
			Preferences.Set("recordar", this.recordarDatos.IsChecked);
        }
        else
        {
            Preferences.Remove("nombre");
            Preferences.Remove("apellidos");
            Preferences.Remove("numeroPaciente");
			Preferences.Remove("recordar");

        }
        Especialidad especialidadElegida = (Especialidad)listaEspecialidades.SelectedItem;
        string turno = turnoEntry.SelectedItem?.ToString();
        Cita cita = new Cita(nombreEntry.Text, apellidosEntry.Text, pacienteEntry.Text, turno, especialidadElegida.Nombre, especialidadElegida.Doctor, especialidadElegida.DiasEspera, DateTime.Now);

        DisplayAlertAsync("Cita enviada", nombreEntry.Text + " " + apellidosEntry.Text + ", a la mayor brevedad le informaremos de su cita en " + especialidadElegida.Nombre +
            ". Puede consultar sus citas desde la sección Consulta de citas.", "Aceptar");

		Navigation.PushAsync(new MainPage());

        string rutaCitas = Path.Combine(FileSystem.AppDataDirectory, "citas.json");
        List<Cita> citas;
        if (File.Exists(rutaCitas))
        {
            string json = File.ReadAllText(rutaCitas);
            citas = JsonSerializer.Deserialize<List<Cita>>(json);
            if(citas == null)
            {
                citas = new List<Cita>();
            }

        } else
        {
            citas = new List<Cita>();
        }

        citas.Add(cita);

        string texto = JsonSerializer.Serialize(citas);
        File.WriteAllText(rutaCitas, texto);
    }
}