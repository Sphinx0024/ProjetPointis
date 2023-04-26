using Activ.Pointis.Android.PersonnelUI.Models;
using Activ.Pointis.Android.PersonnelUI.Views;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http;
using Xamarin.Essentials;


//using Microsoft.Maui.Controls.Navigation;

//using Android.Telephony;
//using Xamarin.Essentials;




namespace Activ.Pointis.Android.PersonnelUI;

public partial class loginPage : ContentPage
{
    
    public static long identifiant { get; set; }
    public static long identconnex { get; set; }
    public static string telephone { get; set; }

    public loginPage()
	{
		InitializeComponent();
	}


    /*public string GetIMEI()
    {
        var imei = string.Empty;

        if (DeviceInfo.Platform == DevicePlatform.Android)
        {
            var telephonyManager = (TelephonyManager)Android.App.Application.Context.GetSystemService(Android.Content.Context.TelephonyService);

            if (telephonyManager != null)
            {
                imei = telephonyManager.DeviceId;
            }
        }

        return imei;
    }*/

    public async Task<string> verifier(string telephone)
    {
        var httpClientHandler = new HttpClientHandler();

        string ident = null;
        string lien = "https://face.activactions.net/api/Connexion/Verifier/"+telephone;

        httpClientHandler.ServerCertificateCustomValidationCallback =
        (message, cert, chain, errors) => { return true; };
        HttpClient client = new HttpClient(httpClientHandler);

        //var content = new StringContent(telephone, Encoding.UTF8, "application/json");
        var content = new StringContent(telephone);
        var response = await client.PostAsync(lien, content);

        if (response.IsSuccessStatusCode)
        {
            //Console.WriteLine("Enregistrement reussi!");
            var responseContent = await response.Content.ReadAsStringAsync();
            ident = responseContent;
            return ident;
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Echec", "Le numéro de téléphone est incorrect !", "OK");
            Console.WriteLine("Echec de l'ajout: " + response.StatusCode);
            ident = "NO";
            return ident;
        }

        //return ident;
    }

    public async  Task Modifier(long id, Connexion connexion)
    {
        var httpClientHandler = new HttpClientHandler();

        string lien = "https://face.activactions.net/api/Connexion/modifier/" + id;

        httpClientHandler.ServerCertificateCustomValidationCallback =
        (message, cert, chain, errors) => { return true; };
        HttpClient client = new HttpClient(httpClientHandler);
        var content = new StringContent(JsonConvert.SerializeObject(connexion), Encoding.UTF8, "application/json");
        //var content = new StringContent(connexion);
        var response = await client.PutAsync(lien, content);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Enregistrement reussi!");
            
            App.Current.MainPage = new NavigationPage(new MainPage());
            
            //var responseContent = await response.Content.ReadAsStringAsync();
        }
        else
        {
            //await Application.Current.MainPage.DisplayAlert("Echec", "Le numéro de téléphone est incorrect !", "OK");
            Console.WriteLine("Echec de l'ajout: " + response.StatusCode);

        }
    }

    public async Task<string> AfficherID(string telephone)
    {
        var httpClientHandler = new HttpClientHandler();

        string ident = null;
        string url = "https://face.activactions.net/api/Connexion/AfficherParTel/" + telephone;

        httpClientHandler.ServerCertificateCustomValidationCallback =
        (message, cert, chain, errors) => { return true; };
        HttpClient client = new HttpClient(httpClientHandler);
        var content = new StringContent(JsonConvert.SerializeObject(telephone), Encoding.UTF8, "application/json");
        //var content = new StringContent(telephone);
        var response = await client.PostAsync(url, content);

        if (response.IsSuccessStatusCode)
        {
            //Console.WriteLine("Enregistrement reussi!");
            var responseContent = await response.Content.ReadAsStringAsync();
            //ident = long.Parse(responseContent);
            ident = responseContent;
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Echec", "Le numéro de téléphone est incorrect !", "OK");
            Console.WriteLine("Echec de l'ajout: " + response.StatusCode);

        }

        return ident;
    }


    /*public string numero()
    {
        var telephonyManager = (TelephonyManager)Android.App.Application.Context.GetSystemService(Context.TelephonyService);
        var phoneNumber = telephonyManager.Line1Number;
        return phoneNumber;
    }*/


    /*public async Task<string> GetImeiAsync()
    {
        try
        {
            var imei = await DeviceInfo.GetImeiAsync();
            return imei;
        }
        catch (Xamarin.Essentials.FeatureNotSupportedException )
        {
            // Gérer l'exception ici
            return null;
        }
    }*/

    private async void ConnexionClick(object sender, EventArgs e)
    {
        
        string tel = user.Text;
        string recup = await verifier(tel);


        if(recup != "\"\"")
        {

            var id = await AfficherID(tel);

            var separer = id.Split('#');

            string con = separer[0].Replace("\"", "");
            long idConnex = long.Parse(con);

            string s = separer[1].Replace("\"", "");
            long idEmp = long.Parse(s);

            
            identifiant = idEmp;
            identconnex = idConnex;
            telephone = tel;

            await Shell.Current.GoToAsync("//Compte");

            //await Shell.Current.GoToAsync("//MainPage");


            //await Shell.Current.GoToAsync("//MainPage");

            //await Navigation.NavigateTo(new MainPage(), ("employeID", idEmp));

            //App.Current.MainPage = new NavigationPage(new MainPage());
        }

        else
        {
            await Application.Current.MainPage.DisplayAlert("Echec", "Le numéro de téléphone est incorrect !", "OK");
        }

        //string deviceIdentifier = DependencyService.Get<IDevice>().GetIdentifier();
    }

}