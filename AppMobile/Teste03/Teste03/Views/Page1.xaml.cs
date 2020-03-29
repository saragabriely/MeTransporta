using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Teste03.Models;
using Teste03.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Teste03.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Page1 : ContentPage
	{
		public Page1 ()
		{
			InitializeComponent ();
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            //int idT;
            int    id   = Convert.ToInt32(etId.Text);
            string desc = descricaoet.Text;

            DataService dataService = new DataService();

            if (!string.IsNullOrEmpty(etId.Text) && !string.IsNullOrEmpty(desc))
            {
                try
                {
                    Teste teste;
                    teste = new Teste(id, desc);

                   // await dataService.PostAsync(teste);

                   // idVerifica.Text = etId.Text + " - " + desc;

                }
                catch (Exception ex)
                {
                    if (ex.Source != null)
                    {
                        await DisplayAlert("Erro", ex.Message, "OK");
                        // Console.WriteLine("Exception source: {0}", ex.Source);
                    }
                    throw;
                }
            }
            else
            {
                await DisplayAlert("Erro", "Dados inválidos...", "OK");
            }
        }



        /*
private async Task Button_ClickedAsync(object sender, EventArgs e)
{
   int id;
   string desc;
   Teste teste;

   //if (etId == null)
   if (string.IsNullOrEmpty(etId.Text))
   {
       await DisplayAlert("Campo Vazio", "Digite a ID!", "OK");
   }
   else
   {
       if (string.IsNullOrEmpty(descricaoet.Text))
       {
           await DisplayAlert("Campo Vazio", "Digite a descrição!", "OK");
       }
       else
       {
           await DisplayAlert("Finalizado", "Cadastro realizado com sucesso!", "OK");
           id = Convert.ToInt32(etId);
           desc = descricaoet.Text;
           teste = new Teste(id, desc);

           await InserirAsync(teste);
       }

   } 
 */

        // try
        // {

        /*
        int id;
        string descricao = descricaoet.Text;
        Teste teste;
        teste = new Teste(Convert.ToInt32(etId), descricao);

        if (etId == null)
        {
            await DisplayAlert("Campo vazio", "Digite o Id!", "OK");
        }
        else if (string.IsNullOrEmpty(descricao))
        {
            await DisplayAlert("Campo vazio", "Digite a descricao!", "OK");
        }
        else
        {
            await DisplayAlert("Finalizado", "Cadastro realizado com sucesso!", "OK");
            await InserirAsync(teste);
            etId.Text = "";
            descricaoet.Text = "";
        } */
        //} 
        /*catch(Exception ex)
        {
            throw;
            //await DisplayAlert("ERRO", ex.ToString(), "OK");
        } 
    }*/
        /*
            private async Task InserirAsync(Teste teste)
            {
                HttpClient usuario = new HttpClient();

                try
                {
                    string url = "http://webapimt2.azurewebsites.net/api/teste/{0}";
                    var uri = new Uri(string.Format(url, teste.Id));

                    var data = JsonConvert.SerializeObject(teste);
                    var content = new StringContent(data, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = null;

                    response = await usuario.PostAsync(uri, content);

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("Erro!");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Erro", ex.ToString(), "OK");
                }
            }*/
    }
}