using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp2 { 

    public partial class TariffsWindow : Window
    {
        private HttpClient client;
        private Tariffs tariffs;
        public TariffsWindow(String token)
        {
            InitializeComponent();
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            Task.Run(() => LoadTariffs());
        }

        private async void LoadTariffs()
        {
            List<Tariffs>? list = await client.GetFromJsonAsync<List<Tariffs>>("http://localhost:5054/api/Tariffs");
           
        }
    
        public string? Speciality_nameProperty
        {
            get { return Speciality_name.Text; }
        }
        public double PriceProperty
        {
            get { return Convert.ToDouble(Price.Text); }
        }
        

        //public async Task<int> getIdTariffs()
        //{
        //    Tariffs? tariffs = await client.GetFromJsonAsync<Tariffs>("http://localhost:5054/api/Tariffs/" + cbGroup.Text);
        //    return tariffs!.Id;
        //}

        private void SaveTariffs_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelTariffs_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

    }
}
