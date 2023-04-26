using Newtonsoft.Json;
using System.ComponentModel;
using Activ.Pointis.Android.PersonnelUI.Models;
using System.Threading.Tasks;

namespace Activ.Pointis.Android.PersonnelUI;

public partial class PointagePage : ContentPage
{
    private long ident;

    private List<V_Pointage> _data;
    public List<V_Pointage> Data
    {
        get => _data;
        set
        {
            _data = value;
            OnPropertyChanged(nameof(Data));
        }
    }
    public PointagePage()
	{
		InitializeComponent();
        OnAppearing();
        BindingContext = this;
        GetData(ident);
    }

    

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        ident = loginPage.identifiant;
        // Utilisez la variable "message" ici
    }

    private async void GetData(long id)
    {
        //var lien = "https://face.activactions.net/api/Pointage/Jour/" + Id;

        var httpClientHandler = new HttpClientHandler();

        httpClientHandler.ServerCertificateCustomValidationCallback =
        (message, cert, chain, errors) => { return true; };
        HttpClient client = new HttpClient(httpClientHandler);
        HttpResponseMessage response = await client.GetAsync("https://face.activactions.net/api/Pointage/Jour/" + Id);

        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();
            Data = JsonConvert.DeserializeObject<List<V_Pointage>>(json);
        }
    }
}