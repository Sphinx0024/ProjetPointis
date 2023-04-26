using Newtonsoft.Json;
using ProjetScan.Model;
using System.ComponentModel;

using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProjetScan;

public partial class UtilisateurPage : ContentPage, INotifyPropertyChanged
{
    private List<EmployesModel> _data;
    public List<EmployesModel> Data
    {
        get => _data;
        set
        {
            _data = value;
            OnPropertyChanged(nameof(Data));
        }
    }

    public UtilisateurPage()
	{
		InitializeComponent();

        BindingContext = this;
        GetData();
    }

    private async void GetData()
    {

        var httpClientHandler = new HttpClientHandler();

        httpClientHandler.ServerCertificateCustomValidationCallback =
        (message, cert, chain, errors) => { return true; }; 
        HttpClient client = new HttpClient(httpClientHandler);
        //HttpResponseMessage response = await client.GetAsync("https://face.activactions.net/api/Employes");
        HttpResponseMessage response = await client.GetAsync("https://face.activactions.net/api/Employes");

        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();
            Data = JsonConvert.DeserializeObject<List<EmployesModel>>(json);
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void AddClick(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AddEmployePage());
        //App.Current.MainPage = new NavigationPage(new ScannerPage());
        //App.Current.MainPage = new NavigationPage(new AddEmployePage());
    }
}