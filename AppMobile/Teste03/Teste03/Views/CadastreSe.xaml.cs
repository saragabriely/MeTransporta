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
	public partial class CadastreSe : ContentPage
	{
		public CadastreSe ()
		{
			InitializeComponent ();
		}

        #region Navegação entre as páginas

        #region Btn - Home 
        private async void BtnHome_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Views.PaginaInicial());
        }
        #endregion

        #region Btn - Conhecer APP
        private async void BtnConhecerApp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Views.ConhecerApp());
        }
        #endregion

        #region Btn - Cadastrar-se
        private async void BtnCadastreSe_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Views.CadastreSe());
        }
        #endregion

        #region Btn - Login
        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Views.Login());
        }
        #endregion

        #endregion

        #region Direciona

        #region Btn - Motorista
        private async void BtnMotorista_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Views.Motorista_Cadastro());
        }
        #endregion

        #region Btn - Cliente
        private async void BtnCliente_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Views.ClientePF_Cadastro());
        }
        #endregion

        #endregion

    }
}