using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace LojaAppRest
{
    /// <summary>
    /// Interaction logic for VeiculoWindow.xaml
    /// </summary>
    public partial class VeiculoWindow : Window
    {
        private string ip = "http://10.21.0.137";
        public VeiculoWindow()
        {
            InitializeComponent();
            setFabricantes();
        }

        private async void setFabricantes()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);
            var response = await httpClient.GetAsync("/20131011110380/api/fabricante");
            var str = response.Content.ReadAsStringAsync().Result;
            List<Modelos.Fabricante> obj = JsonConvert.DeserializeObject<List<Modelos.Fabricante>>(str);
            cmbFabricantes.DisplayMemberPath = "Descricao";
            cmbFabricantes.SelectedValuePath = "Id";
            cmbFabricantes.ItemsSource = obj;
           
        }
        private async void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);
            Modelos.Veiculo v = new Modelos.Veiculo
            {
                id = int.Parse(txtId.Text),
                modelo = txtModelo.Text,
                ano = int.Parse(txtAno.Text),
                datacompra = Convert.ToDateTime(dtCompra.SelectedDate),
                datavenda = Convert.ToDateTime(dtVenda.SelectedDate),
                idfabricante = Convert.ToInt16(cmbFabricantes.SelectedValue),
                valorcompra = decimal.Parse(txtvCompra.Text),
                valorvenda = decimal.Parse(txtvVenda.Text),
                precovenda = decimal.Parse(txtpVenda.Text),    
            };
            List<Modelos.Veiculo> vl = new List<Modelos.Veiculo>();
            vl.Add(v);
            string s = "=" + JsonConvert.SerializeObject(vl);
            var content = new StringContent(s, Encoding.UTF8,
            "application/x-www-form-urlencoded");
            await httpClient.PostAsync("/20131011110380/api/veiculo", content);
        }

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);
            Modelos.Veiculo v = new Modelos.Veiculo
            {
                id = int.Parse(txtId.Text),
                modelo = txtModelo.Text,
                ano = int.Parse(txtAno.Text),
                datacompra = Convert.ToDateTime(dtCompra.SelectedDate),
                datavenda = Convert.ToDateTime(dtVenda.SelectedDate),
                idfabricante = Convert.ToInt16(cmbFabricantes.SelectedValue),
                valorcompra = decimal.Parse(txtvCompra.Text),
                valorvenda = decimal.Parse(txtvVenda.Text),
                precovenda = decimal.Parse(txtpVenda.Text),
            };
            string s = "=" + JsonConvert.SerializeObject(v);
            var content = new StringContent(s, Encoding.UTF8,
            "application/x-www-form-urlencoded");
            await httpClient.PutAsync("/20131011110380/api/veiculo/" + v.id, content);
        }
       
        private async void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);
            var response = await httpClient.GetAsync("/20131011110380/api/veiculo");
            var str = response.Content.ReadAsStringAsync().Result;
            List<Modelos.Veiculo> obj = JsonConvert.DeserializeObject<List<Modelos.Veiculo>>(str, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            ListBoxVeiculos.ItemsSource = obj;
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);
            await httpClient.DeleteAsync("/20131011110380/api/veiculo/" +
            txtId.Text);
        }

    }
}
