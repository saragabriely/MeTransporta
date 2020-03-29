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
	public partial class Chat : ContentPage
	{
		public Chat ()
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
    }
}