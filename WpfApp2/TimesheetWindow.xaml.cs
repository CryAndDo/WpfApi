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
   
    public partial class TimesheetWindow : Window
    {
        private HttpClient httpClient;
        private Timesheet? timesheet;
        public TimesheetWindow(string token)
        {
            InitializeComponent();
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            Task.Run(() => Load());
        }

        private async Task Load()
        {
            List<Timesheet>? list = await httpClient.GetFromJsonAsync<List<Timesheet>>("http://localhost:5054/api/Timesheet");
            Dispatcher.Invoke(() =>
            {
                ListTimesheet.ItemsSource = null;
                ListTimesheet.Items.Clear();
                ListTimesheet.ItemsSource = list;
            });
        }
        private async Task Save()
        {
            Timesheet timesheet = new Timesheet
            {
                Workshop_name = Workshop_nameTimesheet.Text,
                FIO = FIOTimesheet.Text,
                Speciality = SpecialityTimesheet.Text,
                Number_of_days_worked = Convert.ToInt32(Number_of_days_workedTimesheet.Text),
                Zarplata = Convert.ToDouble(ZarplataTimesheet.Text),
                Speciality_code = Convert.ToInt32(Speciality_codeTimesheet.Text),
            };
            JsonContent content = JsonContent.Create(timesheet);
            using var response = await httpClient.PostAsync("http://localhost:5054/api/Timesheet/", content);
            string responseText = await response.Content.ReadAsStringAsync();
            await Load();
        }
        private async Task Edit()
        {
            timesheet!.Workshop_name = Workshop_nameTimesheet.Text;
            timesheet!.FIO = FIOTimesheet.Text;
            timesheet!.Speciality = SpecialityTimesheet.Text;
            timesheet.Number_of_days_worked = Convert.ToInt32(Number_of_days_workedTimesheet.Text);
            timesheet!.Zarplata = Convert.ToDouble(ZarplataTimesheet.Text);
            timesheet!.Speciality_code = Convert.ToInt32(Speciality_codeTimesheet.Text);
            JsonContent content = JsonContent.Create(timesheet);
            using var response = await httpClient.PutAsync("http://localhost:5054/api/Timesheet", content);
            string responseText = await response.Content.ReadAsStringAsync();
            await Load();
        }
        private async Task Delete()
        {
            using var response = await httpClient.DeleteAsync("http://localhost:5054/api/Timesheet/" + timesheet?.Id);
            string responseText = await response.Content.ReadAsStringAsync();
            await Load();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await Edit();
        }

        private void ListTimesheet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            timesheet = ListTimesheet.SelectedItem as Timesheet;
            if (timesheet != null)
            {
                Workshop_nameTimesheet.Text = timesheet!.Workshop_name;
                FIOTimesheet.Text = timesheet!.FIO;
                SpecialityTimesheet.Text = timesheet!.Speciality;
                Number_of_days_workedTimesheet.Text = timesheet!.Number_of_days_worked.ToString();
                ZarplataTimesheet.Text = timesheet!.Zarplata.ToString();
                Speciality_codeTimesheet.Text = timesheet!.Speciality_code.ToString();
            }
        }

        private async void ButtonDeleteTimesheet_Click(object sender, RoutedEventArgs e)
        {
            await Delete();
        }

        private async void Button_Click1(object sender, RoutedEventArgs e)
        {
            await Save();
        }
    }
}
