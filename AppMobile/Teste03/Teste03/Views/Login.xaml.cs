using Newtonsoft.Json;
using Org.Apache.Http.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Teste03.ClassesComuns;
using Teste03.Controllers;
using Teste03.Models;
using Teste03.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Teste03.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
        LoginController loginController = new LoginController();

		public Login ()
		{
			InitializeComponent ();

            lblAlerta.Text        = "";
            lblEntrando.Text      = "";
            lblEntrando.IsVisible = false;
            lblAlerta.IsVisible   = true;
        }

        #region Botão - Entrar
        private async void BtnEntrar_Clicked(object sender, EventArgs e)
        {
           await Verifica();
        }
        #endregion

        #region Verifica ()
        public async Task Verifica()
        {
            #region Variáveis / Controllers

            Cliente cliente             = new Cliente();
            LoginModel        login;
            Motorista         motorista = new Motorista();
            DataService       data      = new DataService();

            LoginController     control             = new LoginController();
            ClienteController   controlCliente      = new ClienteController();
            MotoristaController motoristaController = new MotoristaController();

            string email = etEmail.Text;
            string senha = etSenha.Text;
            string cpf;
            int    idCliente = 0;

            #endregion

            #region Mensagens de retorno
            string ok            = "Campos OK";
            string notEmail      = "Digite o e-mail!";
            string notSenha      = "Digite a senha!";
            string vazios        = "Campos vazios!";
            string invalidos     = "Usuário e/ou senha incorretos";
            string emailInvalido = "E-mail inválido!";
            string acessando     = "Aguarde ...";
            #endregion
                        
            

            if (email != null && senha != null)
            {
                if (!ValidaCampos.IsEmail(email))                  // VALIDANDO E-MAIL
                {
                    lblEntrando.IsVisible = false;
                    lblAlerta.IsVisible   = true;
                    lblAlerta.Text        = "";
                    lblAlerta.Text        = emailInvalido;
                }
                else
                {
                    #region Verifica login

                    // Lista completa
                    var lista = await loginController.GetList();

                    var lista_ = lista.Where(l => l.IdStatus == 4).ToList();       // Id: 4 - Ativo
                     
                    var filtro = lista_.FirstOrDefault(l => l.Email == email);     // Pesquisa e-mail

                    if (filtro == null)  // email inválido
                    {
                        await DisplayAlert("E-mail inválido", "Verifique o e-mail digitado.", "OK");
                    }
                    else if(filtro.Senha != senha)
                    {
                        await DisplayAlert("Senha inválido", "Verifique a senha digitada.", "OK");
                    }
                    else if(filtro.Senha == senha)
                    {
                        // Aguarde ...
                        lblEntrando.IsVisible = true;
                        lblEntrando.Text      = acessando;

                        // Envi ao objeto Login 
                        login = await loginController.GetLogin_(filtro);

                        cpf = login.Ccpf;

                        cliente = await controlCliente.GetCpf(cpf);

                        if(cliente.IdTipoUsuario == 3)
                        {
                            motorista = await motoristaController.GetMotoristaCliente(cliente.IdCliente);

                            Session.Instance.motorista = motorista;
                        }

                        #region Captura dos dados do usuário

                        Session.Instance.IdCliente     = cliente.IdCliente;
                        Session.Instance.IdTipoUsuario = cliente.IdTipoUsuario;
                        Session.Instance.Email         = cliente.Cemail;
                        Session.Instance.Senha         = cliente.Csenha;
                        Session.Instance.Cnome         = cliente.Cnome;
                        Session.Instance.Crg           = cliente.Crg;
                        Session.Instance.Ccpf          = cliente.Ccpf;
                        Session.Instance.Csexo         = cliente.Csexo;
                        Session.Instance.CdataNascto   = cliente.CdataNascto;
                        Session.Instance.Ccelular      = cliente.Ccelular;
                        Session.Instance.Ccelular2     = cliente.Ccelular2;
                        Session.Instance.Cendereco     = cliente.Cendereco;
                        Session.Instance.Cnumero       = cliente.Cnumero;
                        Session.Instance.Ccomplemento  = cliente.Ccomplemento;
                        Session.Instance.Cbairro       = cliente.Cbairro;
                        Session.Instance.Ccidade       = cliente.Ccidade;
                        Session.Instance.Ccep          = cliente.Ccep;
                        Session.Instance.Cuf           = cliente.Cuf;
                        Session.Instance.IdStatus      = cliente.IdStatus;

                        Session.Instance.IdMotorista   = motorista.IdMotorista;
                        Session.Instance.MnumeroCnh    = motorista.MnumeroCnh;
                        Session.Instance.McategoriaCnh = motorista.McategoriaCnh;
                        Session.Instance.MvalidadeCnh  = motorista.MvalidadeCnh;

                        Session.Instance.cliente       = cliente;

                        #endregion
                        
                        await Navigation.PushModalAsync(new Views.LgHome());
                        
                    }
                    
                    #endregion
                }
                
                }
            if (email != null && senha == null)
            {
                await DisplayAlert("Senha", notSenha, "OK");
            }
            if (email == null && senha != null)
            {
                await DisplayAlert("Email", notEmail, "OK");
            }
            if (email == null && senha == null)
            {
                await DisplayAlert("Vazios", vazios, "OK");
            }
        }
        #endregion
        
        #region Navegação entre as páginas

        #region Btn - Home
        private async void BtnHome_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Views.PaginaInicial());
        }
        #endregion

        #region Btn - Conhecer o APP
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



        #region Esqueceu Senha
        private async void CliqueAquiEsqueceuSenha(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Views.EsqueceuSenha());
        }
        #endregion

        #region Esqueceu email
        private async void CliqueAquiEsqueceuEmail(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Views.EsqueceuEmail());
        }
        #endregion

        #endregion
    }
}