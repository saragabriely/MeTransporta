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
	public partial class LgAcompanhaColeta : ContentPage
	{
        #region Variáveis e controllers

        public int idCol;
        public int idOrca;
        public int idStatusOrcamento;
        public int idStatusColeta;

        public int idMotorista = Session.Instance.IdMotorista;    // Motorista: 1; //
        public int idCliente   = Session.Instance.IdCliente;      // Motorista: 8; //  7; //
        public int idTipoUser  = Session.Instance.IdTipoUsuario;  // Motorista: 3; // 2; //

        public string nomeMotorista;
        public string telefoneMotorista;

        AcompanhaColeta acompanha;
        Cliente         cli, cliMotorista;
        Coleta          coleta;
        Orcamento       orcam;
        Status          status;
        Motorista       motorista;

        AcompanhaController acompanhaController = new AcompanhaController();
        ClienteController   clienteController   = new ClienteController();
        ColetaController    coletaControl       = new ColetaController();
        OrcamentoController orcaControl         = new OrcamentoController();
        StatusController    statusController    = new StatusController();
        MotoristaController motoristaController = new MotoristaController();
        VeiculoController   veiculoController   = new VeiculoController();

        #endregion
                
        public LgAcompanhaColeta ()
		{
			InitializeComponent ();

            Filtro_Cliente();

            // Atualiza a lista
            // ListaColetas(idCliente);
		}

        #region Cliente

        #region Filtro

        #region Filtro_Cliente()

        private void Filtro_Cliente()
        {
            #region Lista - Encontrar

            List<string> lstOrcamento_Cliente = new List<string>
            {
                "Coleta(s) em andamento",
                "Coleta(s) finalizada(s)"
            };

            #endregion

            // 1 - Carrega o dropdown
            etFiltroColeta.ItemsSource   = lstOrcamento_Cliente;
            etFiltroColeta.SelectedIndex = 0;

            // 2 - Atualiza a lista de acordo com a opção escolhida
            ListaColetas(idCliente, 0);
        }
        #endregion

        #region Filtro - Coleta
        private void PckFiltroColeta(object sender, EventArgs e)
        {
            var itemSelecionado = etFiltroColeta.Items[etFiltroColeta.SelectedIndex];

            if (itemSelecionado.Equals("Coleta(s) em andamento"))
            {
                ListaColetas(idCliente, 0);
            }
            else if (itemSelecionado.Equals("Coleta(s) finalizada(s)"))
            {
                ListaColetas(idCliente, 1);
            }

        }
        #endregion

        #endregion

        #region Listas

        #region Lista 01 - Coletas em andamento

        #region ListaColetas - 01

        public async void ListaColetas(int idCliente, int id)
        {
            #region Coletas em andamento
            if (id == 0)
            {
                var _list = await coletaControl.GetList(idCliente);

                if (_list == null || _list.Count == 0)
                {
                    LstColeta_Cliente.IsVisible       = false;

                    lbListaVazia_Andamento.IsVisible  = true;

                    lbListaVazia_Finalizada.IsVisible = false;
                }
                else
                {
                    LstColeta_Cliente.IsVisible       = true;
                    LstColeta_Cliente.ItemsSource     = _list;

                    lbListaVazia_Andamento.IsVisible  = false;

                    lbListaVazia_Finalizada.IsVisible = false;
                }
            }
            #endregion

            #region Coletas finalizadas
            else if (id == 1)
            {
                var _list = await coletaControl.GetList_(idCliente);

                if (_list == null || _list.Count == 0)
                {
                    LstColeta_Cliente.IsVisible       = false;

                    lbListaVazia_Andamento.IsVisible  = false;

                    lbListaVazia_Finalizada.IsVisible = true;
                }
                else
                {
                    LstColeta_Cliente.IsVisible       = true;
                    LstColeta_Cliente.ItemsSource     = _list;

                    lbListaVazia_Andamento.IsVisible  = false;

                    lbListaVazia_Finalizada.IsVisible = false;
                }
            }
            #endregion
        }

        #endregion

        #region Lista - ItemSelected 01

        private async void LstOrcamento_Cliente_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            // obtem o item do listview - COLETA
            var coleta = e.SelectedItem as Coleta;

            int idCliente = Session.Instance.IdCliente;                 // Id do cliente logado

            var acompanha = await acompanhaController.GetList();        // Seleciona todos os registros

            acompanha = acompanha.Where(l => l.IdColeta  == coleta.IdColeta)
                                 .Where(l => l.IdCliente == idCliente)
                                 .ToList();

            var lastAcompanha = acompanha.Last();

            var idColeta     = coleta.IdColeta;           //  acompanha.Select(l => l.IdColeta).Distinct().First();
            
            // obtem os dados do motorista

            var idMotorista_ = lastAcompanha.IdMotorista; // acompanha.Select(l => l.IdMotorista).Distinct().First();

            var motorista = await motoristaController.GetMotorista(idMotorista_);

            var clienteMoto = await clienteController.GetCliente(motorista.IdCliente);

            // obtem os dados dos orçamentos que a coleta recebeu
            // var orcamentos = await orcaControl.GetListOrcamentoAceito(coleta.IdColeta);

            // verifica orçamento aceito
            var orcamento = await orcaControl.GetOrcamento(lastAcompanha.IdOrcamento);              

            var veiculo   = await veiculoController.GetConta(orcamento.IdVeiculoUsado);
            
            #region Popula

            lbColeta_.Text         = coleta.ApelidoColeta;
            lblNomeMotorista_.Text = clienteMoto.Cnome;
            lblTelMotorista_.Text  = clienteMoto.Ccelular;
            lblQtdeMotorista_.Text = "R$ " + orcamento.Valor;
            lbVeiculo_.Text        = veiculo.Modelo;
            lbPlaca_.Text          = veiculo.Placa;

            #endregion

            stAcompanha.IsVisible           = true;
            LstColeta_Acompanha.ItemsSource = acompanha;

            // Mostra campos
            MostraCampos();
            stAcompanhaDados.IsVisible = true;

            // Esconde lista inicial e filtro
            stListaCliente.IsVisible = false;
            stFiltrarColetas.IsVisible = false;

            // Mostra a lista de acompanhamento e popula

            LstColeta_Acompanha.IsVisible = true;

            stBtnVoltar_Cliente.IsVisible = true;
        }

        #endregion

        #endregion

        #endregion

        #region PopulaCamposLista()

        private async void PopulaCamposListaAsync(Coleta coleta, Orcamento orcamento, Cliente motorista)
        {
            // Popula Campos
            #region Motorista
            
            lblNomeMotorista_.Text  = motorista.Cnome;
         // lblQtdeMotorista_.Text  = 
            lblTelMotorista_.Text   = motorista.Ccelular;

            #endregion

            // Popula lista
            var acompanhamento = await acompanhaController.GetAcompanhaLista_Coleta(coleta.IdColeta);

            LstColeta_Acompanha.ItemsSource = acompanhamento;
        }

        #endregion

        #region Mostra e esconde campos

        #region MostraCampos()

        private void MostraCampos()
        {
            lbColeta.IsVisible          = true;
            lbColeta_.IsVisible         = true;
            lbNomeMotorista.IsVisible   = true;
            lblNomeMotorista_.IsVisible = true;
            lbQtdeMotorista.IsVisible   = true;
            lblQtdeMotorista_.IsVisible = true;
            lbTelMotorista.IsVisible    = true;
            lblTelMotorista_.IsVisible  = true;
            lbVeiculo.IsVisible         = true;
            lbVeiculo_.IsVisible        = true;
         //   lbPlaca.IsVisible           = true;
            lbPlaca_.IsVisible          = true;
        }
        #endregion

        #region EscondeCampos()

        private void EscondeCampos()
        {
            lbColeta.IsVisible          = false;
            lbColeta_.IsVisible         = false;
            lbNomeMotorista.IsVisible   = false;
            lblNomeMotorista_.IsVisible = false;
            lbQtdeMotorista.IsVisible   = false;
            lblQtdeMotorista_.IsVisible = false;
            lbTelMotorista.IsVisible    = false;
            lblTelMotorista_.IsVisible  = false;
            lbVeiculo.IsVisible         = false;
            lbVeiculo_.IsVisible        = false;
          //  lbPlaca.IsVisible           = false;
            lbPlaca_.IsVisible          = false;
        }
        #endregion

        #endregion

        #region Botões

        #region Btn - Voltar
        private void BtnVoltar_Clicked(object sender, EventArgs e)
        {
            if (stAcompanha.IsVisible)
            {
                // Esconde
                stAcompanhaDados.IsVisible = false;

                EscondeCampos();

                stBtnVoltar_Cliente.IsVisible = false;

                // Mostra
                stListaCliente.IsVisible   = true;
                stFiltrarColetas.IsVisible = true;
            }

        }
        #endregion

        #endregion

        #endregion
        
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

    }
}