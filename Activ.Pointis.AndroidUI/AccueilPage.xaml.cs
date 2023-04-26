namespace ProjetScan;

public partial class AccueilPage : ContentPage
{
	public AccueilPage()
	{
		InitializeComponent();
	}

	public void OnParametreClick(object sender, EventArgs e)
	{

	}

	public void OnDeconnexionClick(object sender, EventArgs e)
	{
		App.Current.MainPage = new NavigationPage(new MainPage());
	}
    private void ModifierClick(object sender, EventArgs e)
    {
        //App.Current.MainPage = new NavigationPage(new MainPage());
    }
}