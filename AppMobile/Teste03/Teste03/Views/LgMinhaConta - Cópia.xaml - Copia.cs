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
	public partial class LgMinhaConta : ContentPage
	{
        public string nome          = Models.Session.Instance.Email;
        public int    IdTipoUsuario = Models.Session.Instance.IdTipoUsuario;

        public LgMinhaConta ()
		{
			InitializeComponent ();

            #region Mostra as opções de acordo com o perfil
            if (IdTipoUsuario == 2)
            {
                slCliente.IsVisible   = true;
                slMotorista.IsVisible = false;
            }
            else
            {
                slCliente.IsVisible   = false;
                slMotorista.IsVisible = true;
            }
            #endregion

        }

        #region Navegação entre as páginas

        #region Btn - Home
        private async void BtnHome_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Views.LgHome());
        }
        #endregion

        #region Btn - Coletas
        private async void BtnColetas_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Views.LgColetas());
        }
        #endregion

        #region Btn - Pesquisar
        private async void BtnPesquisar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Views.LgPesquisar());
        }
        #endregion

        #region Btn - Orcamentos
        private async void BtnOrcamentos_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Views.LgOrcamentos());
        }
        #endregion

        #region Btn - Minha Conta
        private async void BtnMinhaConta_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Views.LgMinhaConta());
        }
        #endregion

        #region Btn - Veiculo
        private async void BtnVeiculos_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Views.LgVeiculos());
        }
        #endregion

        #endregion
        
        #region Btn - Sair - LOGOUT
        private async void BtnSair_Clicked(object sender, EventArgs e)
        {
            var resultado = await DisplayAlert("Sair", "Deseja mesmo sair?", "Ok", "CANCELAR");

            if (resultado == true)
            {
                await Navigation.PushModalAsync(new Views.Login());

                Models.Session.Instance.IdCliente = 0;
                Models.Session.Instance.IdTipoUsuario = 0;
                Models.Session.Instance.Email = "";
            }
        }
        #endregion

        private void BtnCliente_Clicked(object sender, EventArgs e)
        {
            slCliente.IsVisible = true;
            slMotorista.IsVisible = false;
        }

        private void BtnMotorista_Clicked(object sender, EventArgs e)
        {
            slCliente.IsVisible = false;
            slMotorista.IsVisible = true;
        }

        

        
    }
}