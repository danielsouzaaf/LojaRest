﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using Newtonsoft.Json;


namespace LojaAppRest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string ip = "http://10.21.0.137";
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);
            var response = await httpClient.GetAsync("/20131011110380/api/fabricante");
            var str = response.Content.ReadAsStringAsync().Result;
            List<Modelos.Fabricante> obj = JsonConvert.DeserializeObject<List<Modelos.Fabricante>>(str);
            ListBoxFabricantes.ItemsSource = obj;
        }

        private async void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);
            Modelos.Fabricante f = new Modelos.Fabricante
            {
                Id = int.Parse(txtId.Text),
                Descricao = txtDesc.Text
            };
            List<Modelos.Fabricante> fl = new List<Modelos.Fabricante>();
            fl.Add(f);
            string s = "=" + JsonConvert.SerializeObject(fl);
            var content = new StringContent(s, Encoding.UTF8,
            "application/x-www-form-urlencoded");
            await httpClient.PostAsync("/20131011110380/api/fabricante", content);
        }

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);
            Modelos.Fabricante f = new Modelos.Fabricante
            {
                Id = int.Parse(txtId.Text),
                Descricao = txtDesc.Text
            };
            string s = "=" + JsonConvert.SerializeObject(f);
            var content = new StringContent(s, Encoding.UTF8,
            "application/x-www-form-urlencoded");
            await httpClient.PutAsync("/20131011110380/api/fabricante/" + f.Id,
            content);
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);
            await httpClient.DeleteAsync("/20131011110380/api/fabricante/" +
            txtId.Text);
        }

        private void btnFabricante_Click(object sender, RoutedEventArgs e)
        {
            var abrir = new VeiculoWindow();
            abrir.Show();
        }
    }
}
