namespace ProjetScan;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

	private void ConnexionClick(object sender, EventArgs e)
	{
		App.Current.MainPage = new NavigationPage(new MainPage());
	}
}