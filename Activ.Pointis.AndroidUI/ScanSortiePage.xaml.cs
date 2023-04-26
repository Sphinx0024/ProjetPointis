using Newtonsoft.Json;
using System.Text;
using ProjetScan.Model;
using System.Net.Http.Json;

namespace ProjetScan;

public partial class ScanSortiePage : ContentPage
{
	public ScanSortiePage()
	{
		InitializeComponent();
	}

    public async Task Put(PointageModel pointageModel)
    {
        var httpClientHandler = new HttpClientHandler();

        httpClientHandler.ServerCertificateCustomValidationCallback =
        (message, cert, chain, errors) => { return true; };
        HttpClient client = new HttpClient(httpClientHandler); 
        var content = new StringContent(JsonConvert.SerializeObject(pointageModel), Encoding.UTF8, "application/json");
        //var content = new StringContent(JsonSerializer.Serialize(employesModel), Encoding.UTF8, "application/json");
        var response = await client.PutAsync("https://face.activactions.net/api/Pointage/Put/5", content);

        if (response.IsSuccessStatusCode)
        { 
            Console.WriteLine("Modification reussi!");
            await Navigation.PushAsync(new MainPage());
        }
        else
        {
            Console.WriteLine("Echec: " + response.StatusCode);
        }
    }

    public async void ValiderClick(object sender, EventArgs e)
    {
        DateTime maintenant = DateTime.Now;
        /*string heurestr = maintenant.ToString("hh:mm:ss");
        TimeSpan heure;
        heure = TimeSpan.ParseExact(heurestr, "hh\\:mm\\:ss", null);*/

        var pointageModels = new List<PointageModel>();
        PointageModel model = new PointageModel();
        

        /*DateTime aujourdHui = DateTime.Today;
        string date = aujourdHui.ToString("D");*/

        var httpClientHandler = new HttpClientHandler();

        httpClientHandler.ServerCertificateCustomValidationCallback =
        (message, cert, chain, errors) => { return true; };
        HttpClient client = new HttpClient(httpClientHandler);

        HttpResponseMessage response = await client.GetAsync("https://face.activactions.net/api/Pointage/Get/5");

        if (response.IsSuccessStatusCode)
        {
            pointageModels = await response.Content.ReadFromJsonAsync<List<PointageModel>>();

            foreach( var pointageM in pointageModels)
            {
                model.EmployesID = pointageM.EmployesID;
                model.HeureEntree = pointageM.HeureEntree;
                model.HeureSortie = maintenant;
                model.Jour = pointageM.Jour;
            }
            await Put(model);
        }

        /*PointageModel pointage = new PointageModel
        {
            Jour = maintenant,
            HeureEntree = heure,
            EmployesID = 1
        };*/

        

    }

    private void CameraBarcodeReaderView_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
	{
		Dispatcher.Dispatch(() =>
		{
			barcodeResult.Text = $"{e.Results[0].Value} {e.Results[0].Format}";

			//dateJour.Text = DateTime.Now.ToString();
		});
	}

    

}