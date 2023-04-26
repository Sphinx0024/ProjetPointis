using Newtonsoft.Json;
using System.Net.NetworkInformation;
using System.Text;
using ZXing.Net.Maui.Controls;
using ProjetScan.Model;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Xamarin.Essentials;
//using Microsoft.Maui.Essentials;

namespace ProjetScan;

public partial class ScannerPage : ContentPage
{
	public ScannerPage()
	{
		InitializeComponent();
	}

    public async Task Post(PointageModel pointageModel)
    {
        var httpClientHandler = new HttpClientHandler();

        httpClientHandler.ServerCertificateCustomValidationCallback =
        (message, cert, chain, errors) => { return true; };
        HttpClient client = new HttpClient(httpClientHandler);
        
        //  using var client = new HttpClient();
        var content = new StringContent(JsonConvert.SerializeObject(pointageModel), Encoding.UTF8, "application/json");
        
        //var content = new StringContent(JsonSerializer.Serialize(employesModel), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("https://face.activactions.net/api/Pointage/Post", content);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Enregistrement reussi!");
            await Navigation.PushAsync(new MainPage());
        }
        else
        {
            Console.WriteLine("Echec de l'ajout: " + response.StatusCode);
        }
    }

    /*public async Task<string> GetImeiAsync()
    {
        try
        {
            var imei = await Microsoft.Maui.Devices.DeviceInfo.GetImeiAsync();
            return imei;
        }
        catch (Xamarin.Essentials.FeatureNotSupportedException ex)
        {
            // Gérer l'exception ici
            return null;
        }
    }*/


    public async void ValiderClick(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());

        /*DateTime maintenant = DateTime.Now;

        PointageModel pointage = new PointageModel
        {
            Jour = maintenant,
            HeureEntree = maintenant,
            EmployesID = 1
        };

        await Post(pointage);*/

    }

    /*public string GetImei()
   {
       if (DeviceInfo.Platform == DevicePlatform.Android)
       {
           var context = Android.App.Application.Context;
           var telephonyManager = (TelephonyManager)context.GetSystemService(Context.TelephonyService);
           if (telephonyManager != null)
           {
               var imei = telephonyManager.GetImei(0);
               return imei;
           }
           else
           {
               // Gérer l'exception ici
               return null;
           }
       }
       else
       {
           // La plateforme cible ne prend pas en charge l'accès au numéro IMEI
           return null;
       }
   }*/



    private void CameraBarcodeReaderView_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
	{
		Dispatcher.Dispatch(() =>
		{
			barcodeResult.Text = $"{e.Results[0].Value} {e.Results[0].Format}";
		});
	}
}