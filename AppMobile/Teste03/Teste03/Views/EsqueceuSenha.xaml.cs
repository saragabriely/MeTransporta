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
	public partial class EsqueceuSenha : ContentPage
	{
		public EsqueceuSenha ()
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

        private void BtnRecuperarSenha_Clicked(object sender, EventArgs e)
        {
            String resultadoOk = "A senha foi enviada para o e-mail cadastrado.";
            String resultadoNotOk = "Cadastrado não encontrado!" + " Verifique o e-mail digitado.";

            /* Apenas para teste das cores ... */

            if (etEmail.Text != null)
            {
                lblResultadoNotOk.IsVisible = false;
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