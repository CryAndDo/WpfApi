using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        private HttpClient httpClient;
        private MainWindow mainWindow;
        private string token;
        public Main(Response response)
        {
            InitializeComponent();
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + response.access_token);
            token = response.access_token;
            Task.Run(() => Load());
        }
        public async Task Load()
        {
            List<Tariffs>? list = await httpClient.GetFromJsonAsync<List<Tariffs>>("http://localhost:5054/api/Tariffs");
            int i = 0;
            Dispatcher.Invoke(() =>
            {
                ListTariffs.ItemsSource = null;
                ListTariffs.Items.Clear();
                ListTariffs.ItemsSource = list;
            });
        }
        private void MainWindow_Closed(object sender, EventArgs e)
        {
            this.mainWindow.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TimesheetWindow timesheetWindow = new TimesheetWindow(token);
            timesheetWindow.ShowDialog();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TariffsWindow tariffsWindow = new TariffsWindow(token!);
            if (tariffsWindow.ShowDialog() == true)
            {
                Tariffs tariffs = new Tariffs
                { 
                    Speciality_name = tariffsWindow.Speciality_nameProperty,
                    Price = tariffsWindow.PriceProperty
                };
                JsonContent content = JsonContent.Create(tariffs);
                using var response = await httpClient.PostAsync("http://localhost:5054/api/Tariffs", content);
                string responseText = await response.Content.ReadAsStringAsync();
                await Load();
            }
        }
    }
}
