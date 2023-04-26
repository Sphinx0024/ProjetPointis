using Microsoft.Maui.Controls.Shapes;
using Microsoft.Toolkit.Mvvm.Input;
using Newtonsoft.Json;
using ProjetScan.Model;
using ProjetScan.Services.Employes;
using System.Text;
using System.Net.Http.Json;
using ZXing.QrCode.Internal;
using ZXing.QrCode;
using ZXing;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Xamarin.Essentials;
using Newtonsoft.Json.Linq;
using Json.Net;

namespace ProjetScan;

public partial class AddEmployePage : ContentPage
{

    public AddEmployePage()
	{
		InitializeComponent();
	}

    public async Task<long> Post(EmployesModel employesModel)
    {
        var httpClientHandler = new HttpClientHandler();

        long ident = 0;

        httpClientHandler.ServerCertificateCustomValidationCallback =
        (message, cert, chain, errors) => { return true; };
        HttpClient client = new HttpClient(httpClientHandler);
        var content = new StringContent(JsonConvert.SerializeObject(employesModel), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("https://face.activactions.net/api/Employes/Post", content);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Enregistrement reussi!");
            var responseContent = await response.Content.ReadAsStringAsync();
            ident = long.Parse( responseContent);
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Echec", "Enregistrement échoué !", "OK");
            Console.WriteLine("Echec de l'ajout: " + response.StatusCode);
            
        }

        return ident;
    }

    public async Task PostC(ConnexionModel connexion)
    {
        var httpClientHandler = new HttpClientHandler();

        httpClientHandler.ServerCertificateCustomValidationCallback =
        (message, cert, chain, errors) => { return true; };
        HttpClient client = new HttpClient(httpClientHandler);
        //  using var client = new HttpClient();
        var content = new StringContent(JsonConvert.SerializeObject(connexion), Encoding.UTF8, "application/json");
        //var content = new StringContent(JsonSerializer.Serialize(employesModel), Encoding.UTF8, "application/json");
        //var response = await client.PostAsync("https://localhost:44350/api/Employes/Post", content);
        var response = await client.PostAsync("https://face.activactions.net/api/Connexion/Post", content);

        if (response.IsSuccessStatusCode)
        {
            await Application.Current.MainPage.DisplayAlert("Success", "Enregistrement réussi !", "OK");
            //Console.WriteLine("Enregistrement reussi!");
            await Navigation.PushAsync(new UtilisateurPage());
        }
        else
        {
            //await Application.Current.MainPage.DisplayAlert("Echec", "Enregistrement échoué !", "OK");
            Console.WriteLine("Echec de l'ajout: " + response.StatusCode);
            //await Navigation.PushAsync(new AddEmployePage());
            
        }
    }

    public async void AjouterClick(object sender, EventArgs e)
    {

        EmployesModel employes = new EmployesModel
        {
            Nom = txtnom.Text,
            Prenom = txtprenom.Text,
            Email = txtemail.Text,
            Telephone = txttelephone.Text,
            Sexe = (string)txtsexe.SelectedItem,
            SocieteID = 1,
            Titre = txttitre.Text,
            Matricule = txtmatricule.Text
        };

        

        long id = await Post(employes);

        ConnexionModel connexion = new ConnexionModel
        {
            EmployeID = id,
            Login = txttelephone.Text,
            Password = "mkze0123sd",
            Role = txttitre.Text
        };

        await PostC(connexion);
    }


}