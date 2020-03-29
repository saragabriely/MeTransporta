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
	public partial class ConhecerApp : ContentPage
	{
		public ConhecerApp()
		{
			InitializeComponent ();

            // Popula o ScrollView
            Popula();
		}

        #region Popula() - Preenche o ScrollView com a descrição do aplicativo

        private void Popula()
        {
            string titulo   = "MeTransporta!";

            string conteudo = "Conheça o 'MeTransporta'! É um aplicativo que pretende auxiliar aqueles que procuram";

            string conteudo02 = "";

          //  lblConhecerApp.text = "";
        }

        #endregion

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

    }
}