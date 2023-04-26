namespace Activ.Pointis.Android.PersonnelUI;
using Activ.Pointis.Android.PersonnelUI.Models;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http;


public partial class ComptePage : ContentPage
{
    private long ident;
    private string telephone;
    public ComptePage()
	{
		InitializeComponent();
	}

    public async Task Modifier(long id, Connexion connexion)
    {
        var httpClientHandler = new HttpClientHandler();

        //string lien = "https://face.activactions.net/api/Connexion/modifier/" + id;

        httpClientHandler.ServerCertificateCustomValidationCallback =
        (message, cert, chain, errors) => { return true; };
        HttpClient client = new HttpClient(httpClientHandler);
        var content = new StringContent(JsonConvert.SerializeObject(connexion), Encoding.UTF8, "application/json");
        //var content = new StringContent(connexion);
        var response = await client.PostAsync("https://face.activactions.net/api/Connexion/modifier/" + id, content);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Enregistrement reussi!");
        }
        else
        {
            //await Application.Current.MainPage.DisplayAlert("Echec", "Le numéro de téléphone est incorrect !", "OK");
            Console.WriteLine("Echec de l'ajout: " + response.StatusCode);

        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        ident = loginPage.identconnex;
        // Utilisez la variable "message" ici
    }

    private async void valierClick(object sender, EventArgs e)
    {
        string passe = password.Text;
        string conf = passwordconfirm.Text;
        //App.Current.MainPage = new NavigationPage(new MainPage());

        if(passe == conf)
        {
            Connexion connexion = new Connexion()
            {
                Login = passe,
                Password = passe,
                EmployeID = 1,
                Role = passe,
            };

            await Modifier(ident, connexion);

            //string filePath = "Pointis\\Activ.Pointis.Android.PersonnelUI\\DétailConnexion\\connexion.txt";

            // Récupère les informations de connexion de l'utilisateur (login et IMEI)
            string login = ident.ToString();
            string tel = telephone;

            // Crée le fichier de connexion et stocke les informations de connexion de l'utilisateur
            /*using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(login);
                writer.WriteLine(tel);
            }*/

            await Shell.Current.GoToAsync("//MainPage");
        }

        else
        {
            await Application.Current.MainPage.DisplayAlert("Echec", "Les deux mots de passe ne correspondent pas", "OK");
        }
    }
}