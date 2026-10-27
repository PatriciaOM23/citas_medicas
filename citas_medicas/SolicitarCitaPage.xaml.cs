namespace citas_medicas;

public partial class SolicitarCitaPage : ContentPage
{
	public SolicitarCitaPage()
	{
		InitializeComponent();
		nombreEntry.Text = Preferences.Get("nombre", String.Empty);
		apellidosEntry.Text = Preferences.Get("apellidos", String.Empty);
		pacienteEntry.Text = Preferences.Get("numeroPaciente", String.Empty);

	}

	
	private void SolicitarCitaBtn_Clicked(Object sender, EventArgs e)
	{
		if (recordarDatos.IsChecked)
		{
			Preferences.Set("nombre", this.nombreEntry.Text.Trim());
			Preferences.Set("apellidos", this.apellidosEntry.Text.Trim());
			Preferences.Set("numeroPaciente", this.pacienteEntry.Text.Trim());
        }
        else
        {
            Preferences.Remove("nombre");
            Preferences.Remove("apellidos");
            Preferences.Remove("numeroPaciente");

        }
		Navigation.PushAsync(new MainPage());
    }
}