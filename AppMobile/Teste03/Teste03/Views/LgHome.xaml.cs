using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste03.Controllers;
using Teste03.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Teste03.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LgHome : ContentPage
	{
        #region Variáveis - Populando
        public string nome        =  Session.Instance.Cnome;

        public int    idMotorista =  Session.Instance.IdMotorista;    // Motorista: 1; //
        public int    idCliente   =  Session.Instance.IdCliente;      // Motorista: 8; // 7; //
        public int    idTipoUser  =  Session.Instance.IdTipoUsuario;  // Motorista: 3; // 2; //

        ClienteController clienteController = new ClienteController();

        #endregion

        public LgHome ()
		{
			InitializeComponent ();

            #region Verifica o tipo de usuário

            if(idTipoUser == 3)                  // Motorista
            {
                slCliente.IsVisible = false;

                slMotorista.IsVisible    = true;

                BuscaNome(idCliente);
                
                lbBemVindoMotorista.Text = "Bem-vindo(a) " + nome;

                //lbBemVindoMotorista.Text = "Bem-vindo(a) " + nome.ToString();

                #region Verifica notificações

                NotificaOrcamento_Motorista();            // Orcamento

                NotificaColeta_Motorista();    // Coleta em andamento
                #endregion

            }
            else                                                            // Cliente
            {
                slMotorista.IsVisible = false;

                slCliente.IsVisible    = true;

                BuscaNome(idCliente);
                
                lbBemVindoCliente.Text = "Bem-vindo(a) " + nome;

                #region Verifica notificações
                
                NotificaOrcamento_Cliente();            // Orcamento

                NotificaColetaEmAndamento_Cliente();    // Coleta em andamento
                #endregion
            }
            #endregion
        }

        private async void BuscaNome(int idcliente)
        {
            // busca nome
           // var user = await clienteController.GetCliente(idCliente);

           // nome = user.Cnome;
        }

        // --------------------------------------------------------------

        #region Cliente 

        #region Notificações

        #region NotificaOrcamento_Cliente()

        private async void NotificaOrcamento_Cliente()
        {
            // Controller
            OrcamentoController orcamentoController = new OrcamentoController();

            List<Orcamento> _lista = new List<Orcamento>();

            _lista = await orcamentoController.GetListOrcamento_Cliente(idCliente);

            if(_lista.Count > 0)
            {
                lbNotifica_.Text = "Novos orçamentos disponíveis. Verifique agora!";

                lbNotifica_.IsVisible = true;
            }
            else
            {
                lbNotifica__.Text = "Sem novos orçamentos.";
                lbNotifica__.IsVisible = true;
            }
        }

        #endregion

        #region NotificaColetaEmAndamento_Cliente()

        private async void NotificaColetaEmAndamento_Cliente()
        {
            #region Variáveis e controllers
            
            string coletaEmAndamento = "Uma coleta está em andamento. Acompanhe!";
            string coletaFinalizada  = "Uma coleta foi finalizada. Confira!";
            string notifcoleta       = "Aqui aparecerá informações/status sobre o andamento de coletas cadastradas.";

            ColetaController controller = new ColetaController();

            List<Coleta>     coletas    = new List<Coleta>();

            #endregion

            #region Verifica se o cliente já cadastrou alguma coleta
            
            coletas = await controller.GetListColetas(idCliente);

            if(coletas.Count > 0)               // 
            {
                // Verifica se tem coletas em andamento

                var coleta = coletas.Where(l => l.IdStatus == 8).ToList();       // Em andamento

                var finalizada = coletas.Where(l => l.IdStatus == 10).ToList();  // Finalizada

                if(coleta.Count > 0)                            // Caso tenha coleta em andamento
                {
                    lbNotificaColeta__.IsVisible = false;

                    stColeta.IsVisible = true;

                    lbNotificaColeta_.Text      = coletaEmAndamento;
                    lbNotificaColeta_.IsVisible = true;

                    stColeta.IsVisible = true;
                }
                else if (finalizada.Count > 0)                  // verificar se tem alguma coleta finalizada
                {
                    lbNotificaColeta__.IsVisible = false;
                    
                    lbNotificaColeta_.Text      = coletaFinalizada;
                    lbNotificaColeta_.IsVisible = true;

                    stColeta.IsVisible = true;
                }
                else
                {
                    stColeta.IsVisible = true;

                    lbNotificaColeta__.Text      = notifcoleta;
                    lbNotificaColeta__.IsVisible = true;
                } 
            }
            else if (coletas.Count == 0) // verificar se tem alguma coleta finalizada
            {
                lbNotificaColeta_.IsVisible = false;
            
                // Mostra e preenche label
                lbNotificaColeta__.Text      = notifcoleta;
                lbNotificaColeta__.IsVisible = true;
            }
            
            #endregion
            
        }

        #endregion

        #endregion
        
        #region Orçamentos
        private async void VerificaOrcamentos(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Views.LgOrcamentos());
        }
        #endregion

        #region Acompanha Coleta
        private async void AcompanhaColeta(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Views.LgAcompanhaColeta());
        }
        #endregion

        #endregion

        // --------------------------------------------------------------

        #region Motorista

        #region Notificações

        #region NotificaOrcamento_Motorista()

        private async void NotificaOrcamento_Motorista()
        {
            #region Variáveis e controllers

            string aceito   = "PARABÉNS! Orçamento aceito! Inicie a coleta agora mesmo!";
            string aceitos  = "PARABÉNS! Orçamentos aceitos! Inicie as coletas agora mesmo!";
            string nada     = "Sem novidades referente(s) ao(s) orçamento(s) realizado(s).";
            string mensagem = "Aqui aparecerá novidades relacionadas a orçamentos. Fique atento!";

            OrcamentoController orcamentoController = new OrcamentoController();
            
            #endregion

            // Verifica se o motorista possui orçamentos que estão aguardando aprovação
            var orcamento = await orcamentoController.GetListOrcamento_Geral(idMotorista);

            orcamento = orcamento.Where(l => l.IdStatus == 13).ToList();    // Status 13 - Aguardando aprovação

            // Verifica orçamentos aceitos
            var orcamentos = await orcamentoController.GetListOrcamentoAceito(idMotorista);

            // Esconde
            lbNotificaColeta__Moto.IsVisible = false;

            if (orcamentos.Count == 1)       // Um Orcamento aceito
            {
                // Esconde 
                lbNotificaColeta_Moto_.IsVisible = false;

                // Mostra
                stColeta_Moto.IsVisible         = true;
                lbNotificaColeta_Moto.IsVisible = true;

                lbNotificaColeta_Moto.Text = aceito;

            }
            if (orcamentos.Count > 0)       // Mais de um orcamento aceito
            {
                // Esconde 
                lbNotificaColeta_Moto_.IsVisible = false;

                // Mostra
                stColeta_Moto.IsVisible         = true;
                lbNotificaColeta_Moto.IsVisible = true;

                lbNotificaColeta_Moto.Text = aceitos;

            }
            else if(orcamento.Count > 0)  // caso tenha orçamentos que estao aguardando aprovação
            {
                stColeta_Moto.IsVisible          = true;
                lbNotificaColeta_Moto_.IsVisible = true;

                lbNotificaColeta_Moto_.Text      = nada;
            }
            else if(orcamento.Count == 0)    // caso não tenha orçamentos aguardando aprovação
            {
                // esconde
                stColeta_Moto.IsVisible         = false;
                lbNotificaColeta_Moto.IsVisible = false;

                // mostra
                lbNotificaColeta__Moto.IsVisible = true;
                lbNotificaColeta__Moto.Text      = mensagem;
            }

        }

        #endregion

        #region NotificaColeta_Motorista()

        private async void NotificaColeta_Motorista()
        {
            #region Variáveis e Controllers

            string novas  = "Novas coletas disponíveis. Confira!";
            string inicio = "Aqui aparecerá uma notificação caso tenha novas coletas disponíveis. Fique atento!";

            Coleta coleta = new Coleta();

            List<Coleta> lista = new List<Coleta>();

            ColetaController coletaController = new ColetaController();

            #endregion

            // Verifica de tem novas coletas

            lista = await coletaController.GetListColeta_Geral(idMotorista, 0); // Busca as coletas não visualizadas

            if(lista.Count > 0)             // Novas coletas
            {
                lbNotifica__Moto.IsVisible = false;

                lblNotifica_Moto.IsVisible = true;

                lblNotifica_Moto.Text = novas;
            }
            else
            {
                lblNotifica_Moto.IsVisible = false;

                lbNotifica__Moto.IsVisible = true;

                lbNotifica__Moto.Text = inicio;
            }
            
        }

        #endregion


        #endregion

        #region Orçamentos
        private async void VerificaOrcamento(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Views.LgOrcamentos());
        }
        #endregion

        #region Verifica Coleta
        private async void VerificaColeta(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Views.LgColetas());
        }
        #endregion

        #endregion

        // --------------------------------------------------------------

        #region Navegação entre as telas 

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

        #region Btn - Chat
        private async void BtnChat_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Views.LgChat());
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
            await Navigation.PushModalAsync(new Views.LgMinhaContaa());
        }
        #endregion

        #endregion

        #region Others
        private void BtnCliente_Clicked(object sender, EventArgs e)
        {
            slCliente.IsVisible = true;
            slMotorista.IsVisible = false;
            
            lbBemVindoCliente.Text = "Bem-vindo(a) " + nome.ToString();
        }

        private void BtnMotorista_Clicked(object sender, EventArgs e)
        {
            slCliente.IsVisible   = false;
            slMotorista.IsVisible = true;

            lbBemVindoMotorista.Text = "Bem-vindo(a) " + nome.ToString();
        }
        #endregion

    }
}