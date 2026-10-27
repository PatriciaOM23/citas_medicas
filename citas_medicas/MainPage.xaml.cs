namespace citas_medicas
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private void solicitarCitaBtn_Clicked (Object sender, EventArgs e)
        {
            Navigation.PushAsync(new SolicitarCitaPage());
        }
        private void consultarCitaBtn_Clicked (Object sender, EventArgs e)
        {
            Navigation.PushAsync(new ConsultarCitaPage());
        }
    }
}
