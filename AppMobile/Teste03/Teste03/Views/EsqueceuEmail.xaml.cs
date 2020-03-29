using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Teste03.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EsqueceuEmail : ContentPage
	{
		public EsqueceuEmail ()
		{
			InitializeComponent ();
		}

        private async void BtnHome_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Views.PaginaInicial());
        }

        private async void BtnConhecerApp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Views.ConhecerApp());
        }

        private async void BtnCadastreSe_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Views.CadastreSe());
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Views.Login());
        }

        private void BtnRecuperarEmail_Clicked(object sender, EventArgs e)
        {
            String resultadoOk      = "O e-mail cadastrado neste CPF é ...";
            String resultadoNotOk   = "Cadastrado não encontrado!" + " Verifique o CPF digitado.";

            /* Apenas para teste das cores ... */

            if (etCpf.Text != null)
            {
                lblResultadoOk.IsVisible = true;
                lblResultadoOk.Text = resultadoOk;
            }
            else
            {
                lblResultadoOk.IsVisible = false;
                lblResultadoNotOk.IsVisible = true;
                lblResultadoNotOk.Text = resultadoNotOk;
            }
        }
    }
}