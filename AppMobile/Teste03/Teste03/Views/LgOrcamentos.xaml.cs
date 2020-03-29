using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Teste03.Controllers;
using Teste03.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Linq;

namespace Teste03.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LgOrcamentos : ContentPage
	{
        #region Variáveis e controllers

        public int idCol;
        public int idOrca;
        public int idStatusOrcamento;
        public int idStatusColeta;
        public int ultimoRegistro;
        public int idAcompanha;
        public int option;

        public int idMotorista = Session.Instance.IdMotorista;    // Motorista: 1; //
        public int idCliente   = Session.Instance.IdCliente;      // Motorista: 8; // Cliente: 7; //
        public int idTipoUser  = Session.Instance.IdTipoUsuario;  // Motorista: 3; // Cliente: 2; //

        public string enderecoRetirada;
        public string enderecoEntrega;
        public string google = "http://www.google.com.br/maps/dir/";
        public string value;

        Cliente   cli, cliMotorista;
        Coleta    coleta;
        Orcamento orcam;
        Status    status;
        Motorista motorista;

        AcompanhaController acompanhaController = new AcompanhaController();
        ClienteController   clienteController   = new ClienteController();
        ColetaController    coletaControl       = new ColetaController();
        OrcamentoController orcaControl         = new OrcamentoController();
        StatusController    statusController    = new StatusController();
        MotoristaController motoristaController = new MotoristaController();

        #endregion

        public LgOrcamentos ()
		{
			InitializeComponent ();
            
            #region Verifica o tipo de usuário

            if(idTipoUser == 3)         // CLIENTE
            {
                slMotorista.IsVisible = true;
                slCliente.IsVisible   = false;

                Filtro_Motorista();     // Carrega o filtro
            }
            else                        // MOTORISTA
            {
                slMotorista.IsVisible = false;
                slCliente.IsVisible   = true;

                stFiltrarColetas_Cliente.IsVisible = true;

                Filtro_Cliente(1);
            }
            
            #endregion
        }

        //----------------------------------------------------------------------------------------------------------

        #region Cliente

        #region Filtro

        #region Filtro_Cliente()

        private void Filtro_Cliente(int opcao)
        {
            if (opcao == 1)                         // Mostra opções - Coletas com orçamentos
            {
                #region Lista - Encontrar

                List<string> lstOrcamento_Cliente = new List<string>
                {
                    "Coletas que ainda recebem orçamentos",
                    "Coletas pendentes / ainda não iniciadas",
                    "Coleta(s) em andamento",
                    "Coleta(s) cancelada(s)",
                    "Coletas finalizadas",
                    "Todas as coletas que receberam orçamento(s)"
                };

                #endregion

                // 1 - Carrega o dropdown
                etClienteFiltroOrcamento.ItemsSource = lstOrcamento_Cliente;
                etClienteFiltroOrcamento.SelectedIndex = 0;
                
                option = 1;
            }
            else if(opcao == 2)                     // Mostrar opções - Orçamentos
            {
                #region Lista - Encontrar

                List<string> lstOrcamento_Cliente = new List<string>
                {
                    "TODOS OS ORÇAMENTOS",
                    "Orçamentos pendentes de aprovação",
                    "Orçamentos aceitos",
                    "Orçamentos finalizados",
                    "Orçamentos recusados",
                    "Orçamentos cancelados"
                };

                #endregion

                // 1 - Carrega o dropdown
                etClienteFiltroOrcamento_Coleta.ItemsSource   = lstOrcamento_Cliente;
                etClienteFiltroOrcamento_Coleta.SelectedIndex = 0;

               // option = 2;

                // 2 - Atualiza a lista de acordo com a opção escolhida
                // ListaColetas_Orcamento(idCliente);
            }
        }
        #endregion

        #region Filtro - Coleta - Orçamento - Cliente
        private void lstOrcamento_(object sender, EventArgs e)
        {
            var itemSelecionado = etClienteFiltroOrcamento.Items[etClienteFiltroOrcamento.SelectedIndex];

            if(itemSelecionado.Equals("Coletas que ainda recebem orçamentos"))
            {
                ListaCliente_OrcamentoAsync(0);
            }
            else if (itemSelecionado.Equals("Coletas pendentes / ainda não iniciadas"))
            {
                ListaCliente_OrcamentoAsync(1);
            }
            else if (itemSelecionado.Equals("Coleta(s) em andamento"))
            {
                ListaCliente_OrcamentoAsync(2);
            }
            else if (itemSelecionado.Equals("Coleta(s) cancelada(s)"))
            {
                ListaCliente_OrcamentoAsync(3);
            }
            else if (itemSelecionado.Equals("Coletas finalizadas"))
            {
                ListaCliente_OrcamentoAsync(4);
            }
            else if (itemSelecionado.Equals("Todas as coletas que receberam orçamento(s)"))
            {
                ListaCliente_OrcamentoAsync(5);
            }
        }
        #endregion

        #region Filtro - Orçamento - Cliente
        private void lstOrcamento_Colet(object sender, EventArgs e)
        {
            var itemSelecionado = etClienteFiltroOrcamento_Coleta.Items[etClienteFiltroOrcamento_Coleta.SelectedIndex];
           
            if (itemSelecionado.Equals("TODOS OS ORÇAMENTOS"))
            {
                ListaColetas_Orcamento_(idCol, 0);
            }
            else if (itemSelecionado.Equals("Orçamentos pendentes de aprovação"))
            {
                ListaColetas_Orcamento_(idCol, 1);
            }
            else if (itemSelecionado.Equals("Orçamentos aceitos"))
            {
                ListaColetas_Orcamento_(idCol, 2);
            }
            else if (itemSelecionado.Equals("Orçamentos finalizados"))
            {
                ListaColetas_Orcamento_(idCol, 3);
            }
            else if (itemSelecionado.Equals("Orçamentos recusados"))
            {
                ListaColetas_Orcamento_(idCol, 4);
            }
            else if (itemSelecionado.Equals("Orçamentos cancelados"))
            {
                ListaColetas_Orcamento_(idCol, 5);
            }
            
        }
        #endregion

        #endregion

        #region Listas - Cliente

        #region Lista 01 - Coletas com orçamento
        
        #region Lista de coletas - Item selecionado
        private async void LstOrcamentoCliente_01_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {            
            if (e.SelectedItem == null)
            {
                return;
            }
            // obtem o item do listview
            var orcamento = e.SelectedItem as Coleta;

            lblOrcaColeta.Text = "'" + orcamento.ApelidoColeta + "'";
            
            // Mostra
            stFiltrarColetas_Cliente_Orca.IsVisible = true;
            stOrca.IsVisible                        = true;

            // Popula o dropdown com novas opções
            Filtro_Cliente(2);

            //  Esconde
            stListaCliente.IsVisible           = false;
            lbColetaOrcamento.IsVisible        = false;
            stFiltrarColetas_Cliente.IsVisible = false;

            idCol = orcamento.IdColeta;

            ListaColetas_Orcamento_(idCol, 0);

            stBtnVoltar_Cliente_.IsVisible = false;
        }
        #endregion

        #endregion

        #region Lista 02 - Orçamentos

        #region ListaColetas_Orcamento_ (coletas que possuem orçamento)

        public async void ListaColetas_Orcamento_(int idColeta, int opcao)
        {
            #region Variáveis e controllers

            string aceito      = "Nenhum orçamento aceito (e que a coleta ainda não foi finalizada) foi encontrado.";
            string cancelados  = "Nenhum orçamento cancelados, para a coleta em questão, foi localizado.";
            string finalizados = "Nenhum orçamento finalizado foi encontrado (orçamentos aceitos e que a coleta já foi finalizada).";
            string todos       = "Nenhum orçamento foi localizado.";
            string pendente    = "Nenhum orçamento pendente de aprovação, para a coleta em questão, foi encontrado.";
            string rejeitados  = "Nenhum orçamento rejeitado, para a coleta em questão, foi encontrado";
            
            #endregion
            
            var lista = await orcaControl.GetListOrcamento();

            lista = lista.Where(l => l.IdColeta == idColeta).ToList();

            #region Todos os orçamentos
            if (opcao == 0)                                         // "TODOS OS ORÇAMENTOS"
            {
                // var lista = await orcaControl.GetListOrcamento();

                var _list = lista.Where(l => l.IdColeta == idCol).ToList();

                if (_list == null || _list.Count == 0)
                {
                    LstOrcamento_Cliente_02.IsVisible = false;

                    lbResultado_Vazio.Text     = todos;
                    lbResultado_Vazio.IsVisible = true;
                }
                else
                {
                    lbResultado_Vazio.IsVisible = false;

                    // Mostra a lista 02
                    stListaMoto_02.IsVisible = true;

                    LstOrcamento_Cliente_02.ItemsSource = _list;
                    LstOrcamento_Cliente_02.IsVisible   = true;
                }
            }
            #endregion

            #region Orçamentos pendentes de aprovação
            else if (opcao == 1)                                         // "Orçamentos pendentes de aprovação" - Id: 13
            {
                lista = lista.Where(l => l.IdStatus == 13).ToList();

                if (lista == null || lista.Count == 0)
                {
                    LstOrcamento_Cliente_02.IsVisible = false;

                    lbResultado_Vazio.Text = pendente;
                    lbResultado_Vazio.IsVisible = true;
                }
                else
                {
                    lbResultado_Vazio.IsVisible = false;

                    // Mostra a lista 02
                    stListaMoto_02.IsVisible = true;

                    LstOrcamento_Cliente_02.ItemsSource = lista;
                    LstOrcamento_Cliente_02.IsVisible = true;
                }
            }
            #endregion

            #region Orçamentos aceitos
            else if (opcao == 2)                                         // "Orçamentos aceitos" - Id: 1
            {
                lista = lista.Where(l => l.IdStatus == 1).ToList();

                if (lista == null || lista.Count == 0)
                {
                    LstOrcamento_Cliente_02.IsVisible = false;

                    lbResultado_Vazio.Text = aceito;
                    lbResultado_Vazio.IsVisible = true;
                }
                else
                {
                    lbResultado_Vazio.IsVisible = false;

                    // Mostra a lista 02
                    stListaMoto_02.IsVisible = true;

                    LstOrcamento_Cliente_02.ItemsSource = lista;
                    LstOrcamento_Cliente_02.IsVisible = true;
                }
            }
            #endregion

            #region Orçamentos finalizados
            else if (opcao == 3)                                         // "Orçamentos finalizados" - Id: 10
            {
                lista = lista.Where(l => l.IdStatus == 10).ToList();

                if (lista == null || lista.Count == 0)
                {
                    LstOrcamento_Cliente_02.IsVisible = false;

                    lbResultado_Vazio.Text = finalizados;
                    lbResultado_Vazio.IsVisible = true;
                }
                else
                {
                    lbResultado_Vazio.IsVisible = false;

                    // Mostra a lista 02
                    stListaMoto_02.IsVisible = true;

                    LstOrcamento_Cliente_02.ItemsSource = lista;
                    LstOrcamento_Cliente_02.IsVisible = true;
                }
            }
            #endregion

            #region Orçamentos recusados
            else if (opcao == 4)                                         // "Orçamentos recusado"
            {
                lista = lista.Where(l => l.IdStatus == 14).ToList();

                if (lista == null || lista.Count == 0)
                {
                    LstOrcamento_Cliente_02.IsVisible = false;

                    lbResultado_Vazio.Text = rejeitados;
                    lbResultado_Vazio.IsVisible = true;
                }
                else
                {
                    lbResultado_Vazio.IsVisible = false;

                    // Mostra a lista 02
                    stListaMoto_02.IsVisible = true;

                    LstOrcamento_Cliente_02.ItemsSource = lista;
                    LstOrcamento_Cliente_02.IsVisible = true;
                }
            }
            #endregion

            #region Orçamentos cancelados
            else if (opcao == 5)                                         // "Orçamentos cancelados" - Id: 6
            {
                lista = lista.Where(l => l.IdStatus == 6).ToList();

                if (lista == null || lista.Count == 0)
                {
                    LstOrcamento_Cliente_02.IsVisible = false;

                    lbResultado_Vazio.Text = cancelados;
                    lbResultado_Vazio.IsVisible = true;
                }
                else
                {
                    lbResultado_Vazio.IsVisible = false;

                    // Mostra a lista 02
                    stListaMoto_02.IsVisible = true;

                    LstOrcamento_Cliente_02.ItemsSource = lista;
                    LstOrcamento_Cliente_02.IsVisible = true;
                }
            }
            #endregion

        }
        #endregion

        #region Lista de coletas - Item selecionado - 02
        private async void LstOrcamentoCliente_02_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            // obtem o item do listview
            var orcamento = e.SelectedItem as Orcamento;

            orcam = orcamento;

            // Popula a parte do orçamento
            PopulaOrcamento_Motorista(orcamento);

            // Captura a coleta referente ao Orçamento selecionado
            coleta = await coletaControl.GetColeta(orcamento.IdColeta);

            // Captura as informações do motorista
            motorista = await motoristaController.GetMotorista(orcamento.IdMotorista);

            // Captura o nome do motorista
            cliMotorista = await clienteController.GetCliente(motorista.IdCliente);

            #region Dados do motorista
            
            // seleciona todos os orçamentos
            var number = await orcaControl.GetListOrcamento();

            // filtra pelo id do motorista e pelo status do orçamento (10 - finalizado)
            number = number.Where(l => l.IdMotorista == motorista.IdMotorista)
                           .Where(l => l.IdStatus == 10)
                           .ToList();

            int somaColetas = number.Count();

            // Popula campos
            lblNomeMotorista_.Text = cliMotorista.Cnome;
            lblQtdeMotorista_.Text = somaColetas.ToString();  // Total de coleta

            #endregion

            #region Popula

            // Popula as informações da coleta
            PopulaColeta_Cliente(coleta);

            // Popula Dados Orçamento
            PopulaOrcamento_Cliente(orcamento);

            #endregion

            // deseleciona o item do listview
            LstOrcamento_Motorista.SelectedItem = null;

            #region Esconde
            
            stListaMoto_02.IsVisible = false;   // Lista

            stBtnVoltar_Cliente_.IsVisible = false; // Botão

            stFiltrarColetas_Cliente_Orca.IsVisible = false;  // Filtro

            #endregion

            slClienteEncontrarColeta_Lista.IsVisible = true;

            // Verifica orçamento
            if(orcam.IdStatus == 13)  // 13 - Aguardando orçamento
            {
                lbRecusar.IsVisible = true;
                lbAceitar.IsVisible = true;
            }
            else if(orcam.IdStatus != 57 && orcam.IdStatus != 58 && orcam.IdStatus != 59 && orcam.IdStatus != 60)
            {
                lbCancelar.IsVisible = true;
            }
            else
            {
                lbRecusar.IsVisible = false;
                lbAceitar.IsVisible = false;
            }

            // Mostra os campos
            MostraOrcamento_();

            lbExpandir.IsVisible = true;

            // Variáveis 
            idOrca              = orcamento.IdOrcamento;
            idStatusOrcamento   = orcamento.IdStatus;
            idStatusColeta      = coleta.IdStatus;
            value               = orcamento.Valor;

            // Concatena o endereço (nome da rua, numero e bairro)
            enderecoRetirada = coleta.EndRetEndereco + ", " + coleta.EndRetNumero + " - " + coleta.EndRetBairro;
            enderecoEntrega  = coleta.EndEntEndereco + ", " + coleta.EndEntNumero + " - " + coleta.EndEntBairro;

            // Mostra o botão 'Voltar'
            stBtnVoltar_Cliente.IsVisible = true;

            #region Verifica se o orçamento já foi visualizado

            // Caso ainda não tenha sido visualizado
            if(orcamento.Visualiza == null)
            {
                // o atributo 'Visualiza' é atualizado para 1
                orcamento.Visualiza = 1;
                
                await orcaControl.UpdateOrcamento(orcamento, orcamento.IdOrcamento);
            }
            
            #endregion
        }
        #endregion

        #endregion

        #region Popula campos 

        #region PopulaOrcamento_Cliente(orcamento)

        private async void PopulaOrcamento_Cliente(Orcamento orcamento)
        {
            #region Busca status

            status = new Status();
            status = await statusController.GetStatus(orcamento.IdStatus);

            #endregion

            #region Campos

            etStatusOrcamento_C.Text       = status.DescricaoStatus.ToUpper();

            // Popula Dados Orçamento
            etValor_Cliente.Text = orcamento.Valor;
            etObservacoes_Orcamento_.Text = orcamento.Observacoes;

            #endregion
        }

        #endregion

        #region PopulaColeta_Cliente(coleta)

        private void PopulaColeta_Cliente(Coleta coleta)
        {
            #region Campos
            
            etEndRet_Cliente.Text              = coleta.EndRetEndereco;
            etEndEnt_Cliente.Text              = coleta.EndEntEndereco;
            lbTipoMaterial_Cliente.Text        = coleta.MatTipo;
            lbFragilidadeMaterial_Cliente.Text = coleta.MatFragilidade;
            lbDescricaoMaterial_Cliente.Text   = coleta.MatDescricao;
            etPeso_Cliente.Text                = coleta.MatPeso;
            etVolume_Cliente.Text              = coleta.MatVolume;
            etLargura_Cliente.Text             = coleta.MatLargura;
            etAltura_Cliente.Text              = coleta.MatAltura;
            etDataMax_Cliente.Text             = coleta.DataMaxima;
            etHorario_Cliente.Text             = coleta.HorarioLimite + ":" + coleta.HorarioLimite02;
            etValorPretendido_Cliente.Text     = coleta.ValorPretendido;
            etObservacoes_Cliente.Text         = coleta.Observacoes;
            etTipoVeiculo_Cliente.Text         = coleta.TipoVeiculo;
            #endregion
        }

        #endregion

        #endregion

        #region ListaCliente_OrcamentoAsync(int i)  (lista dos orçamentos por coleta)

        public async void ListaCliente_OrcamentoAsync(int i)
        {  
            #region Variáveis

            string naofinalizada = "Nenhum resultado foi encontrado para essa pesquisa.";
            string andamento     = "Nenhuma coleta em andamento foi encontrada."; 
            string canceladas    = "Nenhuma coleta cancelada recebeu orçamento.";
            string finalizadas   = "Nenhuma coleta finalizada foi encontrada.";
            string todas         = "Nenhuma coleta recebeu orçamento.";
            string aguardando    = "Nenhuma coleta está aguardando orçamentos.";

            List<Coleta>     list;
            List<Orcamento> _list;
            _list = null;
            list = null;

            #endregion

             list = await coletaControl.GetListColetaComOrcamento(idCliente);

            // Verifica se receberam orçamentos
            var lista = await orcaControl.GetListOrcamento();

            #region Coletas que ainda recebem orçamento / sem orçamentos aceitos

            if (i == 0)                                           // Coletas que ainda recebem orçamento
            {
                // Lista de coletas - Filtro pelo status
                list = list.Where(l => l.IdStatus == 2).ToList();  // IdStatus: 2 - Aguardando orçamento

                var listId = list.Select(l => l.IdColeta).ToList(); // seleciona os Ids das coletas (pesquisa acima)
                
                // Seleciona as coletas encontradas que possuem orçamentos
                lista = lista.Where(l => listId.Contains(l.IdColeta)).ToList();

                if (list == null || list.Count == 0)
                {
                    LstOrcamento_Cliente.IsVisible = false;

                    lbListaVaziaMoto_.Text      = aguardando;
                    lbListaVaziaMoto_.IsVisible = true;
                }
                else
                {
                    LstOrcamento_Cliente.ItemsSource = list;
                    LstOrcamento_Cliente.IsVisible   = true;

                    lbListaVaziaMoto_.IsVisible      = false;
                }
            }
            #endregion

            #region Coletas pendentes / ainda não iniciadas
            else if (i == 1)                                                             // Coletas pendentes / ainda não iniciadas
            {
                list = list.Where(l => l.IdStatus == 30).ToList();

                if (list == null || list.Count == 0)
                {
                    LstOrcamento_Cliente.IsVisible = false;

                    lbListaVaziaMoto_.Text = naofinalizada;
                    lbListaVaziaMoto_.IsVisible = true;
                }
                else
                {
                    LstOrcamento_Cliente.ItemsSource = list;
                    LstOrcamento_Cliente.IsVisible = true;

                    lbListaVaziaMoto_.IsVisible = false;
                }
            }
            #endregion

            #region Coletas em andamento
            else if (i == 2)                                                        // COLETAS EM ANDAMENTO
            {
                list = list.Where(l => l.IdStatus == 8).ToList();

                if (list == null || list.Count == 0)
                {
                    LstOrcamento_Cliente.IsVisible = false;

                    lbListaVaziaMoto_.Text  = andamento;
                    lbListaVaziaMoto_.IsVisible = true;
                }
                else
                {
                    LstOrcamento_Cliente.ItemsSource = list;
                    LstOrcamento_Cliente.IsVisible   = true;

                    lbListaVaziaMoto_.IsVisible = false;
                }
            }
            #endregion

            #region Coletas canceladas
            else if (i == 3)
            {
                list = list.Where(l => l.IdStatus == 6).ToList();

                if (list == null || list.Count == 0)
                {
                    LstOrcamento_Cliente.IsVisible = false;
            
                    lbListaVaziaMoto_.Text      = canceladas;
                    lbListaVaziaMoto_.IsVisible = true;
                }
                else
                {
                    LstOrcamento_Cliente.IsVisible   = true;
                    LstOrcamento_Cliente.ItemsSource = list;
            
                    lbListaVaziaMoto_.IsVisible = false;
                }
            }
            #endregion

            #region Coletas finalizadas
            else if (i == 4)                                                        // 10
            {
                list = list.Where(l => l.IdStatus == 10).ToList();

                if (list == null || list.Count == 0)
                {
                    LstOrcamento_Cliente.IsVisible = false;
            
                    lbListaVaziaMoto_.Text      = finalizadas;
                    lbListaVaziaMoto_.IsVisible = true;
                }
                else
                {
                    LstOrcamento_Cliente.ItemsSource = list;
                    LstOrcamento_Cliente.IsVisible   = true;
            
                    lbListaVaziaMoto_.IsVisible      = false;
                }
            }
                #endregion

            #region Todas as coletas que receberam orçamentos
            else if (i == 5)                                                        // Todas as coletas que receberam orçamentos
            {
                if (list == null || list.Count == 0)
                {
                    LstOrcamento_Cliente.IsVisible = false;

                    lbListaVaziaMoto_.Text      = todas;
                    lbListaVaziaMoto_.IsVisible = true;
                }
                else
                {
                    LstOrcamento_Cliente.IsVisible   = true;
                    LstOrcamento_Cliente.ItemsSource = list;
                    lbListaVaziaMoto_.IsVisible      = false;
                }
            }
            #endregion
    
        }
        #endregion

        #endregion

        #region Botões

        #region BtnVoltar_Clente_Clicked
        private async void BtnVoltar_Clente_Clicked(object sender, SelectedItemChangedEventArgs e)
        {
            if (slClienteEncontrarColeta_Lista.IsVisible)                           // Volta para a lista de orçamento
            {
                // Esconde os campos
                slClienteEncontrarColeta_Lista.IsVisible = false;

                stListaMoto_02.IsVisible = true;

                stFiltrarColetas_Cliente_Orca.IsVisible = true;

                // Esconde o botão 'Voltar'
                stBtnVoltar_Cliente.IsVisible = false;

                // Atualiza a lista de orçamentos
                ListaColetas_Orcamento_(idCol, 0);
            }
            else                                                          // Volta para a primeira lista (coletas)
            {
                // Mostra

                stListaCliente.IsVisible = true;

                stListaMoto_02.IsVisible = false;

                //  Esconde
                stListaCliente.IsVisible       = false;
                lbColetaOrcamento.IsVisible    = false;
                stBtnVoltar_Cliente_.IsVisible = false;

               // ListaColetas_Orcamento(idCliente);

                ListaColetas_Orcamento_(idCol, 0);
            }
        }
        #endregion

        #endregion

        #region Aceite e Recusa / Cancelar

        #region Recusar Orçamento

        private void LblCancelarOrcamento(object sender, SelectedItemChangedEventArgs e)
        {
            AceitarRecusar(3);  // Cancelar
        }

        #endregion

        #region Recusar Orçamento

        private void LblRecusarOrcamento(object sender, SelectedItemChangedEventArgs e)
        {
            AceitarRecusar(2);  // Recusa
        }

        #endregion

        #region AceitarRecusar(i)

        private async void AceitarRecusar(int tipo) // Verifica o tipo de operação e faz o que for necessário
        {
            // Aceitar  = 1
            // Recusar  = 2
            // Cancelar = 3
            
            if (idStatusOrcamento == 1)
            {
                await DisplayAlert("Aceito", "Este orçamento já foi aceito!", "OK");
            }
            else if (idStatusOrcamento == 14)
            {
                await DisplayAlert("Recusado", "Este orçamento já foi recusado!", "OK");
            }
            else if (idStatusOrcamento == 6)
            {
                await DisplayAlert("Cancelado", "Este orçamento já foi cancelado!", "OK");
            }

            #region Aceitar

            else if (tipo == 1)
            {
                StatusController statusControl = new StatusController();

                if (await DisplayAlert("Aceite", "Deseja mesmo aceitar este orçamento?", "OK", "Cancelar"))
                {
                    #region Atualiza status - Orçamento

                    // Captura o objeto
                    orcam = await orcaControl.GetOrcamento(idOrca);

                    // Atualiza o status
                    orcam.IdStatus = 1; // 'Aceito'

                    // Seleciona a descrição do status
                    var status = await statusControl.GetStatus(orcam.IdStatus);

                    // Atualiza a descrição no orçamento
                    orcam.DescStatus = status.DescricaoStatus;

                    // Atualiza o status no banco
                    await orcaControl.UpdateOrcamento(orcam, idOrca);

                    // Atualiza o status dos demais orçamentos como 'Recusado' (ID: 14)
                    AtualizaStatus(orcam.IdColeta, 14, orcam.IdOrcamento);
                    
                    #endregion

                    #region Salva dados para acompanhamento da coleta

                    int idAceite = 61; // Id - Orcamento aceito pelo cliente
                    
                    AcompanhaColeta acompanha; 

                    AcompanhaController acompanhaController = new AcompanhaController();
                    StatusController    statusController = new StatusController();

                    var statusColeta = await statusController.GetStatus(61);
                    
                    #region objeto

                    acompanha = new AcompanhaColeta()
                    {
                        IdColeta      = orcam.IdColeta,
                        IdOrcamento   = orcam.IdOrcamento,
                        IdCliente     = orcam.IdCliente,
                        IdMotorista   = orcam.IdMotorista,
                        DataHora      = DateTime.Now,
                        IdStatus      = idAceite,
                        StatusDesc    = statusColeta.DescricaoStatus
                    };

                    #endregion

                    await acompanhaController.PostAcompanhaAsync(acompanha);

                    #endregion

                    #region Atualiza IdStatus da coleta

                    var coleta = await coletaControl.GetColeta(orcam.IdColeta);

                    coleta.IdStatus = 30; // Aguardando motorista
                    
                    var status_ = await statusControl.GetStatus(coleta.IdStatus);

                    coleta.DescricaoStatus = status_.DescricaoStatus;

                    await coletaControl.UpdateColeta(coleta);

                    #endregion

                    await DisplayAlert("Aceito!", "Orçamento aceito com sucesso! Aguarde um momento que o motorista dará inicio a coleta.", "OK");

                    // Volta para os orçamentos da coleta em questão
                    slClienteEncontrarColeta_Lista.IsVisible = false;

                    // Atualiza a lista de orçamentos
                    ListaColetas_Orcamento_(idCol, 0);

                    stListaMoto_02.IsVisible = true;
                }
            }

            #endregion

            #region Recusar

            else if (tipo == 2)  // Recusa
            {
                if (await DisplayAlert("Recusa", "Deseja mesmo recusar este orçamento?", "OK", "Cancelar"))
                {
                    #region Atualiza o status do orçamento
                    // Captura o objeto
                    orcam = await orcaControl.GetOrcamento(idOrca);

                    // Atualiza o status
                    orcam.IdStatus = 14;  // 14 - Recusado

                    // Atualiza o status no banco
                    await orcaControl.UpdateOrcamento(orcam, idOrca);

                    #endregion

                    await DisplayAlert("Recusado", "Orçamento recusado com sucesso!", "OK");

                    // Volta para os orçamentos da coleta em questão
                    slClienteEncontrarColeta_Lista.IsVisible = false;

                    // Atualiza a lista de orçamentos
                    ListaColetas_Orcamento_(idCol, 0);

                    stListaMoto_02.IsVisible = true;
                }
            }
            #endregion

            #region Recusar

            else if (tipo == 3)  // Cancelar
            {
                if (await DisplayAlert("Cancelar", "Deseja mesmo cancelar este orçamento?", "OK", "Cancelar"))
                {
                    #region Atualiza o status do orçamento
                    // Captura o objeto
                    orcam = await orcaControl.GetOrcamento(idOrca);

                    // Atualiza o status
                    orcam.IdStatus = 6;  // 6 - Recusado

                    // Atualiza o status no banco
                    await orcaControl.UpdateOrcamento(orcam, idOrca);

                    #endregion

                    await DisplayAlert("Cancelado", "Orçamento cancelado com sucesso!", "OK");

                    // Volta para os orçamentos da coleta em questão
                    slClienteEncontrarColeta_Lista.IsVisible = false;

                    // Atualiza a lista de orçamentos
                    ListaColetas_Orcamento_(idCol, 0);

                    stListaMoto_02.IsVisible = true;
                }
            }
            #endregion

        }

        #endregion

        #region Aceitar Orçamento

        private  void LblAceitarOrcamento(object sender, SelectedItemChangedEventArgs e)
        {
            AceitarRecusar(1); // Aceitar
        }

        #endregion

        #region Aceite - Atualizar os demais orçamentos

        private async void AtualizaStatus(int idColeta, int idStatus, int idOrcaAceito)
        {
            // Assim que um orçamento é aceito, os demais orçamentos (relacionados a coleta em questão)
            // deverão ser recusados automaticamente

            await orcaControl.GetRecusaOrcamentos(idColeta, idOrcaAceito, idStatus);
            
        }

        #endregion
        
        #endregion

        #region Mostra e esconde campos

        #region VerDetalhes

        private void LblVerDetalhes(object sender, SelectedItemChangedEventArgs e)
        {
            MostraDados_();

            // Esconde a opção 'Ver detalhes da coleta'
            lbExpandir.IsVisible = false;

            // Mostra a opção 'Esconder detalhes da coleta'
            lbEsconder.IsVisible = true;
        }

        #endregion

        #region EsconderDetalhes

        private void LblEsconderDetalhes(object sender, SelectedItemChangedEventArgs e)
        {
            EscondeDados_Cliente();

            // Esconde a opção 'Ver detalhes da coleta'
            lbExpandir.IsVisible = true;

            // Mostra a opção 'Esconder detalhes da coleta'
            lbEsconder.IsVisible = false;
        }

        #endregion
        
        
        #region EscondeOrcamento_Cliente()

        public void EscondeOrcamento_Cliente()
        {
            #region Orçamento

            lbStatusOrcamento_C.IsVisible      = false;
            etStatusOrcamento_C.IsVisible      = false;
            lbValorOrcamento_.IsVisible        = false;
            etValor_Cliente.IsVisible          = false;
            etValorRS_Cliente.IsVisible        = false;
            lbObs_Orcamento_.IsVisible         = false;
            etObservacoes_Orcamento_.IsVisible = false;
            lbNomeMotorista.IsVisible          = false;
            lblNomeMotorista_.IsVisible        = false;
            lbQtdeMotorista.IsVisible          = false;
            lblQtdeMotorista_.IsVisible        = false;

            #endregion
        }

        #endregion

        #region EscondeDados_cliente()

        public void EscondeDados_Cliente()
        {
            #region Campos

            #region Endereços e descrição do material

            lbEndRet_.IsVisible                     = false;
            etEndRet_Cliente.IsVisible              = false;
            lbEndEnt_.IsVisible                     = false;
            etEndEnt_Cliente.IsVisible              = false;
            lbTipoMaterial_.IsVisible               = false;
            lbTipoMaterial_Cliente.IsVisible        = false;
            lbFragilidadeMaterial_.IsVisible        = false;
            lbFragilidadeMaterial_Cliente.IsVisible = false;
            lbDescricaoMaterial_.IsVisible          = false;
            lbDescricaoMaterial_Cliente.IsVisible   = false;

            #endregion

            #region Peso e dimensões

            lbPeso_.IsVisible           = false;
            etPeso_Cliente.IsVisible    = false;
            lbPeso2_.IsVisible          = false;
            lbVolume_.IsVisible         = false;
            etVolume_Cliente.IsVisible  = false;
            lbLargura_.IsVisible        = false;
            etLargura_Cliente.IsVisible = false;
            lbLargura2_.IsVisible       = false;
            lbAltura_.IsVisible         = false;
            etAltura_Cliente.IsVisible  = false;
            lbAltura2_.IsVisible        = false;

            #endregion

            #region Data, valor, obs e tipo de veiculo 

            lbDataMax_.IsVisible                  = false;
            etDataMax_Cliente.IsVisible           = false;
            lbHorario_.IsVisible                  = false;
            etHorario_Cliente.IsVisible           = false;
            lbValorPretendido_.IsVisible          = false;
            etValorPretendidoRS_Cliente.IsVisible = false;
            etValorPretendido_Cliente.IsVisible   = false;
            lbObservacoes_.IsVisible              = false;
            etObservacoes_Cliente.IsVisible       = false;
            lbTipoVeiculo_.IsVisible              = false;
            etTipoVeiculo_Cliente.IsVisible       = false;

            #endregion
            
            #endregion
        }

        #endregion


        #region MostraDados_()

        public void MostraDados_()
        {
            #region Campos
            
            #region Endereços e descrição do material

            lbEndRet_.IsVisible                     = true;
            etEndRet_Cliente.IsVisible              = true;
            lbEndEnt_.IsVisible                     = true;
            etEndEnt_Cliente.IsVisible              = true;
            lbTipoMaterial_.IsVisible               = true;
            lbTipoMaterial_Cliente.IsVisible        = true;
            lbFragilidadeMaterial_.IsVisible        = true;
            lbFragilidadeMaterial_Cliente.IsVisible = true;
            lbDescricaoMaterial_.IsVisible          = true;
            lbDescricaoMaterial_Cliente.IsVisible   = true;

            #endregion

            #region Dimensões

            lbPeso_.IsVisible            = true;
            etPeso_Cliente.IsVisible     = true;
            lbPeso2_.IsVisible           = true;
            lbVolume_.IsVisible          = true;
            etVolume_Cliente.IsVisible   = true;
            lbLargura_.IsVisible         = true;
            etLargura_Cliente.IsVisible  = true;
            lbLargura2_.IsVisible        = true;
            lbAltura_.IsVisible          = true;
            etAltura_Cliente.IsVisible   = true;
            lbAltura2_.IsVisible         = true;

            #endregion

            #region Data, valor, obs e tipo veiculo

            lbDataMax_.IsVisible                  = true;
            etDataMax_Cliente.IsVisible           = true;
            lbHorario_.IsVisible                  = true;
            etHorario_Cliente.IsVisible           = true;
            lbValorPretendido_.IsVisible          = true;
            etValorPretendidoRS_Cliente.IsVisible = true;
            etValorPretendido_Cliente.IsVisible   = true;
            lbObservacoes_.IsVisible              = true;
            etObservacoes_Cliente.IsVisible       = true;
            lbTipoVeiculo_.IsVisible              = true;
            etTipoVeiculo_Cliente.IsVisible       = true;

            #endregion

            #endregion
        }

        #endregion
                
        #region MostraOrcamento_()

        public void MostraOrcamento_()
        {
            #region Orçamento

            lbStatusOrcamento_C.IsVisible      = true;
            etStatusOrcamento_C.IsVisible      = true;
            lbValorOrcamento_.IsVisible        = true;
            etValor_Cliente.IsVisible          = true;
            etValorRS_Cliente.IsVisible        = true;
            lbObs_Orcamento_.IsVisible         = true;
            etObservacoes_Orcamento_.IsVisible = true;
            lbNomeMotorista.IsVisible          = true;
            lblNomeMotorista_.IsVisible        = true;
            lbQtdeMotorista.IsVisible          = true;
            lblQtdeMotorista_.IsVisible        = true;

            #endregion
        }

        #endregion

        #endregion

        #endregion
        
        //----------------------------------------------------------------------------------------------------------

        #region Motorista

        #region Filtro

        #region Filtro_Motorista()

        private void Filtro_Motorista()
        {
            #region Lista - Encontrar

            List<string> lstOrcamento_Motorista = new List<string>
            {
                "TODOS OS ORÇAMENTOS",
                "Orçamentos pendentes de aprovação",
                "Orçamentos aceitos e coletas não iniciadas",
                "Orçamentos finalizados",
                "Orçamentos rejeitados",
                "Orçamentos cancelados"
            };

            #endregion

            // 1 - Carrega o dropdown
            etMotoristaFiltroOrcamento.ItemsSource   = lstOrcamento_Motorista;
            etMotoristaFiltroOrcamento.SelectedIndex = 0;

            // 2 - Atualiza a lista de acordo com a opção escolhida
            ListaMotorista_OrcamentoAsync(0);
        }
        #endregion

        #region Filtro - Orçamento - Motorista
        private void pckMotoristaFiltroOrcamento(object sender, EventArgs e)
        {
            var itemSelecionado = etMotoristaFiltroOrcamento.Items[etMotoristaFiltroOrcamento.SelectedIndex];

            if (itemSelecionado.Equals("TODOS OS ORÇAMENTOS"))
            {
                ListaMotorista_OrcamentoAsync(0);
            }
            else if (itemSelecionado.Equals("Orçamentos pendentes de aprovação"))
            {
                ListaMotorista_OrcamentoAsync(1);
            }
            else if (itemSelecionado.Equals("Orçamentos aceitos e coletas não iniciadas"))
            {
                ListaMotorista_OrcamentoAsync(2);
            }
            else if (itemSelecionado.Equals("Orçamentos rejeitados"))
            {
                ListaMotorista_OrcamentoAsync(3);
            }
            else if (itemSelecionado.Equals("Orçamentos cancelados"))
            {
                ListaMotorista_OrcamentoAsync(4);
            }
            else if (itemSelecionado.Equals("Orçamentos finalizados"))
            {
                ListaMotorista_OrcamentoAsync(5);
            }
        }
        #endregion

        #endregion

        #region Lista - Motorista

        #region ListaMotorista_OrcamentoAsync(int i)

        public async void ListaMotorista_OrcamentoAsync(int i)
        {
            List<Orcamento> _list = new List<Orcamento>();

            _list = null;

            #region Variáveis

            string vazio      = "Nenhum orçamento cadastrado até o momento.";
            string pendente   = "Nenhum orçamento pendente de aprovação foi encontrado. ";
            string aceito     = "Nenhum orçamento aceito até o momento.";
            string rejeitado  = "Nenhum orçamento foi rejeitado até o momento.";
            string cancelado  = "Nenhum orçamento foi cancelado até o momento.";
            string finalizado = "Nenhum orçamento foi finalizado até agora.";

            #endregion

            #region Todos os orçamentos 
            if (i == 0)                                                               // TODOS OS ORÇAMENTOS
            {
                 _list = await orcaControl.GetListOrcamento_Geral(idMotorista);   // IdMotorista e IdStatus

                if (_list == null || _list.Count == 0)
                {
                    LstOrcamento_Motorista.IsVisible = false;   // Lista

                    lbListaVaziaMoto.Text = vazio;
                    lbListaVaziaMoto.IsVisible = true;
                }
                else
                {
                    lbListaVaziaMoto.IsVisible = false;

                    LstOrcamento_Motorista.ItemsSource = _list;
                }
            }
            #endregion

            #region Orçamentos pendentes de aprovação - Id: 13

            else if (i == 1)                                                        // ORÇAMENTOS PENDENTES DE APROVAÇÃO 
            {                                                                       // (Aguardando aprovacao - 13)
                _list = await orcaControl.GetListOrcamento(13, idMotorista);

                if (_list == null || _list.Count == 0)
                {
                    LstOrcamento_Motorista.IsVisible = false;   // Lista

                    lbListaVaziaMoto.Text = pendente;
                    lbListaVaziaMoto.IsVisible = true;
                }
                else
                {
                    lbListaVaziaMoto.IsVisible = false;

                    LstOrcamento_Motorista.ItemsSource = _list;
                }
            }
            #endregion

            #region Orçamentos aceitos - Id: 01

            else if (i == 2)                                                        // Coleta aceita e ainda não iniciada  (Aguardando motorista - 30)
            {
                _list = await orcaControl.GetListOrcamento(30, idMotorista);

                if (_list == null || _list.Count == 0)
                {
                    LstOrcamento_Motorista.IsVisible = false;   // Lista

                    lbListaVaziaMoto.Text = aceito;
                    lbListaVaziaMoto.IsVisible = true;
                }
                else
                {
                    lbListaVaziaMoto.IsVisible = false;

                    LstOrcamento_Motorista.ItemsSource = _list;
                }
            }

            #endregion

            #region Orçamentos rejeitados - Id: 14

            else if (i == 3)                                                        // ORÇAMENTOS REJEITADOS (Rejeitado - 14)
            {
                _list = await orcaControl.GetListOrcamento(14, idMotorista); ;

                if (_list == null || _list.Count == 0)
                {
                    LstOrcamento_Motorista.IsVisible = false;   // Lista

                    lbListaVaziaMoto.Text = rejeitado;
                    lbListaVaziaMoto.IsVisible = true;
                }
                else
                {
                    lbListaVaziaMoto.IsVisible = false;

                    LstOrcamento_Motorista.ItemsSource = _list;
                }
            }

            #endregion

            #region Orçamentos cancelados - Id: 6

            else if (i == 4)                                                        // ORÇAMENTOS CANCELADOS (Cancelado - 6)
            {
                _list = await orcaControl.GetListOrcamento(6, idMotorista);

                if (_list == null || _list.Count == 0)
                {
                    LstOrcamento_Motorista.IsVisible = false;   // Lista

                    lbListaVaziaMoto.IsVisible = true;
                    lbListaVaziaMoto.Text      = cancelado;
                }
                else
                {
                    lbListaVaziaMoto.IsVisible = false;

                    LstOrcamento_Motorista.ItemsSource = _list;
                }
            }

            #endregion

            #region Orçamentos finalizados - Id: 10

            else if (i == 5)                                                        // ORÇAMENTOS FINALIZADOS (Finalizada(o) - 10)
            {
                _list = await orcaControl.GetListOrcamento(10, idMotorista);

                if (_list == null || _list.Count == 0)
                {
                    LstOrcamento_Motorista.IsVisible = false;   // Lista

                    lbListaVaziaMoto.Text = finalizado;
                    lbListaVaziaMoto.IsVisible = true;
                }
                else
                {
                    lbListaVaziaMoto.IsVisible         = false;
                    LstOrcamento_Motorista.ItemsSource = _list;
                }
            }
            #endregion
        }
        #endregion

        #region Lista de coletas - Item selecionado
        private async void LstOrcamentoMotorista_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            // obtem o item do listview
            var orcamento = e.SelectedItem as Orcamento;

            orcam = orcamento;

            // Popula a parte do orçamento
            PopulaOrcamento_Motorista(orcamento);

            // Captura a coleta referente ao Orçamento selecionado
             coleta = await coletaControl.GetColeta(orcamento.IdColeta);

            // Popula as informações da coleta
            PopulaColeta_Motorista(coleta);

            // deseleciona o item do listview
            LstOrcamento_Motorista.SelectedItem = null;
            
            // Concatena o endereço (nome da rua, numero e bairro)
            enderecoRetirada = coleta.EndRetEndereco + ", " + coleta.EndRetNumero + " - " + coleta.EndRetBairro;
            enderecoEntrega  = coleta.EndEntEndereco + ", " + coleta.EndEntNumero + " - " + coleta.EndEntBairro;

            //var acompanha = await acompanhaController.GetAcompanhaLista_Coleta(idColeta);
            var acompanha = await acompanhaController.GetAcompanhaLista_Coleta__(coleta.IdColeta);

            acompanha = acompanha.Where(l => l.IdOrcamento == orcam.IdOrcamento)
                                 .ToList();
            
            if(orcam.IdStatus == 14 || orcam.IdStatus == 13) // Orçamento recusado  / Aguardando aprovação
            {
                btnRelatorioColeta.IsVisible = false;
                btnRelatorioColeta.IsVisible = false;
            }
            else if(acompanha.Count == 1)                    // Coleta não iniciada - tem apenas o registro 'Orçamento aceito'
            {
                btnRealizarColeta.IsVisible = true;
            }
            else if(acompanha.Count > 1)
            {
                var final = acompanha.Last();

                if (final.IdStatus == 10)                       // Permite visualizar o mesmo acompanhamento que o cliente (AcompanhaColeta)
                {
                    btnRelatorioColeta.IsVisible = true; 
                }
                else                                            // Permite continuar a coleta de onde parou
                {
                    btnContinuarColeta.IsVisible = true;
                }
            }
                    
            #region Esconde

            // Esconde o filtro
            stFiltrarColetas_Moto.IsVisible = false;

            // Esconde a lista
            stListaMoto.IsVisible = false;

            #endregion

            slMotoristaEncontrarColeta_Lista.IsVisible = true;

            // Mostra os campos
            MostraOrcamento();
            MostraDados_Moto();
            MostraDados_2_Moto();

            stBtnVoltar_Moto.IsVisible = true;

            // Variáveis 
            idOrca            = orcamento.IdOrcamento;
            idStatusOrcamento = orcamento.IdStatus;
            idStatusColeta    = coleta.IdStatus;
            value             = orcamento.Valor;
            idCol             = coleta.IdColeta;

            // Mostra o botão 'Realizar Coleta', caso o orçamento tenha sido aceito
          //  if (idStatusOrcamento == 1 && idStatusColeta != 10)       // IdStatus: 1 - Aceito + 10 - Finalizada
           // {
           //     btnRealizarColeta.IsVisible = true;
           // }        
        }
        #endregion

        #endregion

        #region PopulaOrcamento_Motorista(orcamento)

        private async void PopulaOrcamento_Motorista(Orcamento orcamento)
        {
            #region Busca status

            status = new Status();
            status = await statusController.GetStatus(orcamento.IdStatus);

            #endregion

            #region Campos

            etStatusOrcamento.Text       = status.DescricaoStatus.ToUpper();
            etValor_Moto.Text            = orcamento.Valor;
            etObservacoes_Orcamento.Text = orcamento.Observacoes;

            #endregion
        }

        #endregion

        #region PopulaColeta_Motorista(coleta)

        private void PopulaColeta_Motorista(Coleta coleta)
        {
            #region Campos

            etEndRet_Moto.Text              = coleta.EndRetEndereco;
            etEndEnt_Moto.Text              = coleta.EndEntEndereco;
            lbTipoMaterial_Moto.Text        = coleta.MatTipo;
            lbFragilidadeMaterial_Moto.Text = coleta.MatFragilidade;
            lbDescricaoMaterial_Moto.Text   = coleta.MatDescricao;
            etPeso_Moto.Text                = coleta.MatPeso;
            etVolume_Moto.Text              = coleta.MatVolume;
            etLargura_Moto.Text             = coleta.MatLargura;
            etAltura_Moto.Text              = coleta.MatAltura;
            etDataMax_Moto.Text             = coleta.DataMaxima;
            etHorario_Moto.Text             = coleta.HorarioLimite + ":" + coleta.HorarioLimite02;
            etValorPretendido_Moto.Text     = coleta.ValorPretendido;
            etObservacoes_Moto.Text         = coleta.Observacoes;
            etTipoVeiculo_Moto.Text         = coleta.TipoVeiculo;


            #endregion
        }

        #endregion

        #region Verifica ultimo registro - AcompanhaColeta

        private async Task VerificaUltimoRegistro(int idColeta, int i)
        {
            //var acompanha = await acompanhaController.GetAcompanhaLista_Coleta(idColeta);
            var acompanha = await acompanhaController.GetAcompanhaLista_Coleta__(idColeta);

            var last = acompanha.Last();

            if (i == 1) // Escolhe o botão que será mostrado - 'Realizar' ou 'Continuar'
            {
                if (last.IdStatus == 61 ) // IdStatus: 61 - "Orçamento aceito pelo cliente"
                {
                    ultimoRegistro = 1;  // libera o botão 'Realizar coleta'
                }
                else if (last.IdStatus == 52 || last.IdStatus == 54)  // IdStatus: 52 - "Aguardando motorista se preparar." // 54 - "Motorista seguindo para o local de retirada"
                {
                    ultimoRegistro = 2; // libera botão 'continuar'
                }
            }
            else  if(i == 2)
            {
                if (last.IdStatus == 59)         // IdStatus: 59 - Material entregue com sucesso!
                {
                    gridRealizar.IsVisible = false;

                    lblValor_Final.Text = orcam.Valor;

                    gridValor.IsVisible = true; // mostra a parte final com o valor do orçamento
                }

                else if(last.IdStatus == 52)                // IdStatus: 52 - "Aguardando motorista se preparar"
                {
                    lbEtapa01.IsVisible   = true;
                    btnRetirar.IsVisible  = true;

                    btnRetirado.IsVisible = true;
                }
                else if(last.IdStatus == 54)          // IdStatus: 54 - "Motorista seguindo para o local de retirada"
                {
                    lbEtapa01.IsVisible   = true;
                    btnRetirar.IsVisible  = true;

                    btnRetirado.IsVisible = true;
                }
                else if(last.IdStatus == 56)          // IdStatus: 56 - Material retirado
                {
                    lbEtapa01.IsVisible   = true;
                    btnRetirar.IsVisible  = true;

                    lbEtapa02.IsVisible   = true;
                    btnEntregar.IsVisible = true;
                }
                else if(last.IdStatus == 57)         // IdStatus: 57 - Seguindo para local de entrega
                {
                    lbEtapa01.IsVisible   = true;
                    btnRetirar.IsVisible  = true;

                    lbEtapa02.IsVisible   = true;
                    btnEntregar.IsVisible = true;

                    btnFinalizar.IsVisible = true;
                }
                
            }

        }

        #endregion

        #region Botões

        #region Btn - Realizar Coleta (direciona)

        private async void BtnRealizarColeta_Motorista_Clicked(object sender, SelectedItemChangedEventArgs e)
        {
            // Esconde o ScrollView
            slMotoristaEncontrarColeta_Lista.IsVisible = false;
            
            #region Salva dados para acompanhamento da coleta

            int idAceite = 52; // Id - Aguardando motorista se preparar

            AcompanhaColeta acompanha;

            AcompanhaController acompanhaController = new AcompanhaController();
            StatusController    statusController    = new StatusController();

            var statusColeta = await statusController.GetStatus(52);

            #region objeto

            acompanha = new AcompanhaColeta()
            {
                IdColeta    = orcam.IdColeta,
                IdOrcamento = orcam.IdOrcamento,
                IdCliente   = orcam.IdCliente,
                IdMotorista = orcam.IdMotorista,
                DataHora    = DateTime.Now,
                IdStatus    = idAceite,
                StatusDesc  = statusColeta.DescricaoStatus
            };

            #endregion

            await acompanhaController.PostAcompanhaAsync(acompanha);

            #endregion

            #region Atualiza status da coleta

            var coleta = await coletaControl.GetColeta(orcam.IdColeta); // captura objeto

            coleta.IdStatus = 8;                        // IdStatus: 8 - Em Andamento

            await coletaControl.UpdateColeta(coleta);   // atualiza

            #endregion

            #region Mostrar botões

            lbEtapa01.IsVisible  = true;

            btnRetirar.IsVisible = true;

            #endregion

            // Mostra a view 'Realizar Coleta'
            stRealizarColeta.IsVisible = true;

            stFiltrarColetas_Moto.IsVisible = false;

            #region Encontra cliente

            cli = await clienteController.GetCliente(idCliente);

            lblNomeCliente.Text = cli.Cnome;

            #endregion
            
        }

        #endregion

        #region Btn - Continuar Coleta (direciona)

        private async void BtnContinuarColeta_Clicked(object sender, SelectedItemChangedEventArgs e)
        {
            // Esconde o ScrollView
            slMotoristaEncontrarColeta_Lista.IsVisible = false;
            slClienteEncontrarColeta_Lista.IsVisible   = false;
            svClienteEncontrarColeta.IsVisible         = false;

            stListaMoto.IsVisible = false;

            // Verifica o ultimo registro
           // var ultimoRegistro = await acompanhaController.GetList();

           // ultimoRegistro = ultimoRegistro.Where(l => l.IdOrcamento == orcam.IdOrcamento).ToList();
           
           // var ultimo = ultimoRegistro.Last();

           // if (ultimo.IdStatus == 59)
           // {
           //     gridValor.IsVisible = true;
           // }
         //   else
            //{
                // Mostra a view 'Realizar Coleta'
                stRealizarColeta.IsVisible = true;
                
           // }

            stFiltrarColetas_Moto.IsVisible = false;

            // Mostrar botões

            await VerificaUltimoRegistro(idCol, 2);

            // Encontra cliente

            cli = await clienteController.GetCliente(orcam.IdCliente);

            PopulaCliente_Orcamento(cli, coleta);
            
        }

        #endregion

        #region Btn - Relatório final - coleta 

        private async void BtnRelatorioColeta_Clicked(object sender, SelectedItemChangedEventArgs e)
        {
            // Esconde o ScrollView
            slMotoristaEncontrarColeta_Lista.IsVisible = false;
            slClienteEncontrarColeta_Lista.IsVisible   = false;
            svClienteEncontrarColeta.IsVisible         = false;

            stListaMoto.IsVisible = false;

            lbLigar.IsVisible    = false;
            lbDesistir.IsVisible = false;

            // Mostra a view 'Realizar Coleta'
            stRealizarColeta.IsVisible      = true;

            stFiltrarColetas_Moto.IsVisible = false;
            
            // Popula lista
            PopulaLista_Andamento();

            // Mostrar Lista
            stAcompanha.IsVisible      = true;

            // Mostrar botão
            stBtnVoltar_Moto.IsVisible = true;

            // Encontra cliente

            var clienteContratante = await clienteController.GetCliente(orcam.IdCliente);

            PopulaCliente_Orcamento(clienteContratante, coleta);
        }

        #endregion

        #region PopulaLista_Andamento()

        public async void PopulaLista_Andamento()
        {
            int id = orcam.IdOrcamento;

            // var acompanha = await acompanhaController.GetAcompanhaLista_Cliente(id);
            //  acompanha = acompanha.Where(l => l.IdOrcamento == orcam.IdOrcamento).ToList();

            var acompanha = await acompanhaController.GetList();

            acompanha = acompanha.Where(l => l.IdOrcamento == orcam.IdOrcamento).ToList();
            

            if (acompanha.Count == 0 || acompanha == null)
            {
                lbListaVazia_.IsVisible = true;
            }
            else
            {
                lbListaVazia_.IsVisible = false;

                LstColeta_Acompanha.ItemsSource = acompanha;
            }
        }

        #endregion

        #region PopulaCliente_Orcamento()

        private void PopulaCliente_Orcamento(Cliente cliente, Coleta coleta)
        {
            #region Campos

            lblNomeCliente.Text     = cliente.Cnome;
            lblTelefoneCliente.Text = cliente.Ccelular;

            lblEndRetCliente.Text = coleta.EndRetEndereco + ", " + coleta.EndRetNumero + " - " + coleta.EndRetBairro;
            lblEndEntCliente.Text = coleta.EndEntEndereco + ", " + coleta.EndEntNumero + " - " + coleta.EndEntBairro;

            #endregion
        }

        #endregion

        #region Btn - Voltar

        private async void BtnVoltar_Moto_Clicked(object sender, SelectedItemChangedEventArgs e)
        {
            if (slMotoristaEncontrarColeta_Lista.IsVisible)
            {
                // Esconde o ScrollView
                slMotoristaEncontrarColeta_Lista.IsVisible = false;

                // Esconde o botão 'Voltar'
                stBtnVoltar_Moto.IsVisible = false;

                btnRealizarColeta.IsVisible = false;

                // Mostra o filtro e a lista
                stFiltrarColetas_Moto.IsVisible = true;
                stListaMoto.IsVisible = true;
            }
            else 
            {
                // Mostra o ScrollView
                slMotoristaEncontrarColeta_Lista.IsVisible = true;

                // Esconde o botão 'Voltar'
                stBtnVoltar_Moto.IsVisible = true;

                if (idStatusOrcamento == 1 && idStatusColeta != 10)  // IdStatus: 1 - Aceito  // IdStatus: 10 - Finalizada (coleta)
                {
                    // Verifica qual foi o último status salvo em 'AcompanhaColeta'
                    await VerificaUltimoRegistro(idCol, 1);

                    if (ultimoRegistro == 1)                    // Coleta não inicia
                    {
                        btnRealizarColeta.IsVisible = true;
                    }
                    else if (ultimoRegistro == 2)               // Coleta em andamento
                    {
                        btnContinuarColeta.IsVisible = true;
                    }
                }

                // Esconde a view 'Realizar Coleta'
                stRealizarColeta.IsVisible      = false;

                // Mostra o filtro e a lista
                stFiltrarColetas_Moto.IsVisible = false;
                stListaMoto.IsVisible           = false;
            }
        }

        #endregion
        
        #region Btn - Avançar

        private async void BtnAvancar_Motorista_Clicked(object sender, SelectedItemChangedEventArgs e)
        {

        }

        #endregion

        #region Btn - Finalizar

        private async void BtnFinalizar_Clicked(object sender, SelectedItemChangedEventArgs e)
        {

            #region Salva dados para acompanhamento da coleta

            int idAceite = 59; // Id - material entregue

            AcompanhaColeta acompanha;

            AcompanhaController acompanhaController = new AcompanhaController();
            StatusController    statusController    = new StatusController();

            var statusColeta = await statusController.GetStatus(59);

            #region objeto

            acompanha = new AcompanhaColeta()
            {
                IdColeta    = orcam.IdColeta,
                IdOrcamento = orcam.IdOrcamento,
                IdCliente   = orcam.IdCliente,
                IdMotorista = orcam.IdMotorista,
                DataHora    = DateTime.Now,
                IdStatus    = idAceite,
                StatusDesc  = statusColeta.DescricaoStatus
            };

            #endregion

            await acompanhaController.PostAcompanhaAsync(acompanha);

            #endregion

            // Mostra e esconde GRID
            gridRealizar.IsVisible = false;
            gridValor.IsVisible    = true;

            lblValor_Final.Text    = value;

        }

        #endregion

        #region Btn - Realizar Cobrança

        private async void BtnRealizarCobranca_Motorista_Clicked(object sender, SelectedItemChangedEventArgs e)
        {

            #region Salva dados para acompanhamento da coleta

            AcompanhaColeta acompanha;

            AcompanhaController acompanhaController = new AcompanhaController();
            StatusController    statusController    = new StatusController();

            #region Verifica se já foi inserido algum registro com esse ID referente a coleta

            var acompanaLista = await acompanhaController.GetList();

            acompanaLista = acompanaLista.Where(l => l.IdColeta == orcam.IdColeta)
                                         .Where(l => l.IdStatus == 10)
                                         .ToList();

          //  var acompanha_ = await acompanhaController.GetAcompanhaLista_Coleta(orcam.IdColeta);

         //   var verifica = acompanaLista.Where(l => l.IdStatus == 10).ToList();

            if(acompanaLista == null || acompanaLista.Count == 0) // verifica se algum registro igual foi inserido
            {
                int idAceite = 10; // Id - finalizada
                
                var statusColeta = await statusController.GetStatus(10);
            
                #region objeto

                acompanha = new AcompanhaColeta()
                {
                    IdColeta    = orcam.IdColeta,
                    IdOrcamento = orcam.IdOrcamento,
                    IdCliente   = orcam.IdCliente,
                    IdMotorista = orcam.IdMotorista,
                    DataHora    = DateTime.Now,
                    IdStatus    = idAceite,
                    StatusDesc  = statusColeta.DescricaoStatus
                };

                #endregion
                            
                await acompanhaController.PostAcompanhaAsync(acompanha);
            }
          
            #endregion
            
            #endregion

            var coleta = await coletaControl.GetColeta(orcam.IdColeta);

            coleta.IdStatus = 10;

            var status = await statusController.GetStatus(10);

            // Atualiza status da coleta
            coleta.DescricaoStatus = status.DescricaoStatus;

            await coletaControl.UpdateColeta(coleta);

            //await DisplayAlert("Sucesso!", "Cobrança realizada com sucesso!", "OK");
            await DisplayAlert("Sucesso!", "Cobrança realizada com sucesso!", "OK");

            // Esconde a view de 'Realizar Coleta'
            stRealizarColeta.IsVisible = false;

            slMotoristaEncontrarColeta_Lista.IsVisible = false;

            #region Atualiza o orçamento e a coleta (como finalizados)

            // Orçamento -------------------------------------------
            orcam.IdStatus = 10;  // IdStatus: 10 - Finalizado 

            var stat = await statusController.GetStatus(orcam.IdStatus);

            orcam.DescStatus = stat.DescricaoStatus;

            await orcaControl.UpdateOrcamento(orcam, orcam.IdOrcamento);

            // Coleta -----------------------------------------------
            coleta.IdStatus = 10;

            await coletaControl.UpdateColeta(coleta);

            #endregion

            // Recarrega a lista
            ListaMotorista_OrcamentoAsync(0);

            // Recarrega a página
            await Navigation.PushModalAsync(new Views.LgOrcamentos());
        }

        #endregion
        
        #region Btn - Retira

        private async void BtnRetirar_Clicked(object sender, SelectedItemChangedEventArgs e)
        {
            #region Salva dados para acompanhamento da coleta

            int idAceite = 54; // Id - Aguardando motorista se preparar

            AcompanhaColeta acompanha;

            AcompanhaController acompanhaController = new AcompanhaController();
            StatusController    statusController    = new StatusController();

            var statusColeta = await statusController.GetStatus(54);

            #region objeto

            acompanha = new AcompanhaColeta()
            {
                IdColeta    = orcam.IdColeta,
                IdOrcamento = orcam.IdOrcamento,
                IdCliente   = orcam.IdCliente,
                IdMotorista = orcam.IdMotorista,
                DataHora    = DateTime.Now,
                IdStatus    = idAceite,
                StatusDesc  = statusColeta.DescricaoStatus
            };

            #endregion

            await acompanhaController.PostAcompanhaAsync(acompanha);

            #endregion
                       
            string retira = enderecoRetirada.Replace(" ", "+");

            Uri uri = new Uri(google + "/" + retira);

            await OpenBrowser(uri);

            // Btn - Material retirado
            btnRetirado.IsVisible = true;
        }

        #endregion

        #region Btn - Retirado

        private async void btnRetirado_Clicked(object sender, EventArgs e)
        {
            #region Salva dados para acompanhamento da coleta

            int idAceite = 56; // Id - Aguardando motorista se preparar

            AcompanhaColeta acompanha;

            AcompanhaController acompanhaController = new AcompanhaController();
            StatusController    statusController    = new StatusController();

            var statusColeta = await statusController.GetStatus(56);

            #region objeto

            acompanha = new AcompanhaColeta()
            {
                IdColeta    = orcam.IdColeta,
                IdOrcamento = orcam.IdOrcamento,
                IdCliente   = orcam.IdCliente,
                IdMotorista = orcam.IdMotorista,
                DataHora    = DateTime.Now,
                IdStatus    = idAceite,
                StatusDesc  = statusColeta.DescricaoStatus
            };

            #endregion

            await acompanhaController.PostAcompanhaAsync(acompanha);

            #endregion

            btnRetirado.IsVisible = false;

            lbEtapa02.IsVisible   = true;
            btnEntregar.IsVisible = true;
        }

        #endregion
        
        #region Btn - Entrega

        private async void BtnEntregar_Clicked(object sender, SelectedItemChangedEventArgs e)
        {
            #region Salva dados para acompanhamento da coleta

            int idAceite = 57; // Id - Aguardando motorista se preparar

            AcompanhaColeta acompanha;

            AcompanhaController acompanhaController = new AcompanhaController();
            StatusController    statusController    = new StatusController();

            var statusColeta = await statusController.GetStatus(57);

            #region objeto

            acompanha = new AcompanhaColeta()
            {
                IdColeta    = orcam.IdColeta,
                IdOrcamento = orcam.IdOrcamento,
                IdCliente   = orcam.IdCliente,
                IdMotorista = orcam.IdMotorista,
                DataHora    = DateTime.Now,
                IdStatus    = idAceite,
                StatusDesc  = statusColeta.DescricaoStatus
            };

            #endregion

            await acompanhaController.PostAcompanhaAsync(acompanha);

            #endregion

            string entrega = enderecoEntrega.Replace(" ", "+");  // Substitui os espaços

            Uri uri = new Uri(google + "/" + entrega);

            await OpenBrowser(uri);     // Abre o browser

            // Btn - Entregue
            btnFinalizar.IsVisible = true;
        }

        #endregion


        #region Btn - Ligar

        private async void BtnLigarCliente(object sender, SelectedItemChangedEventArgs e)
        {

        }

        #endregion

        #region Btn - Desistir

        private async void BtnDesistir(object sender, SelectedItemChangedEventArgs e)
        {

        }

        #endregion
        
        #endregion
        
        #region OpenBrowser - Abre aplicativo externo (GoogleMaps)
        
        public async Task OpenBrowser(Uri uri)
        {
            await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        } 

        #endregion
        
        #region Mostra e esconde campos

        #region EscondeDados_Moto()

        public void EscondeDados_Moto()
        {
            #region Campos

            lbEndRet.IsVisible                   = false;
            etEndRet_Moto.IsVisible              = false;
            lbEndEnt.IsVisible                   = false;
            etEndEnt_Moto.IsVisible              = false;
            lbTipoMaterial.IsVisible             = false;
            lbTipoMaterial_Moto.IsVisible        = false;
            lbFragilidadeMaterial.IsVisible      = false;
            lbFragilidadeMaterial_Moto.IsVisible = false;
            lbDescricaoMaterial.IsVisible        = false;
            lbDescricaoMaterial_Moto.IsVisible   = false;

            #endregion
        }

        #endregion

        #region MostraDados_Moto()

        public void MostraDados_Moto()
        {
            #region Campos

            lbEndRet.IsVisible                   = true;
            etEndRet_Moto.IsVisible              = true;
            lbEndEnt.IsVisible                   = true;
            etEndEnt_Moto.IsVisible              = true;
            lbTipoMaterial.IsVisible             = true;
            lbTipoMaterial_Moto.IsVisible        = true;
            lbFragilidadeMaterial.IsVisible      = true;
            lbFragilidadeMaterial_Moto.IsVisible = true;
            lbDescricaoMaterial.IsVisible        = true;
            lbDescricaoMaterial_Moto.IsVisible   = true;

            #endregion
        }

        #endregion

        #region EscondeDados_2_Moto()

        public void EscondeDados_2_Moto()
        {
            #region Campos
            lbPeso.IsVisible         = false;
            etPeso_Moto.IsVisible    = false;
            lbPeso2.IsVisible        = false;
            lbVolume.IsVisible       = false;
            etVolume_Moto.IsVisible  = false;
            lbLargura.IsVisible      = false;
            etLargura_Moto.IsVisible = false;
            lbLargura2.IsVisible     = false;
            lbAltura.IsVisible       = false;
            etAltura_Moto.IsVisible  = false;
            lbAltura2.IsVisible      = false;

            lbDataMax.IsVisible      = false;
            etDataMax_Moto.IsVisible = false;
            lbHorario.IsVisible      = false;
            etHorario_Moto.IsVisible = false;
            lbValorPretendido.IsVisible      = false;
            etValorPretendidoRS_Moto.IsVisible = false;
            etValorPretendido_Moto.IsVisible = false;
            lbObservacoes.IsVisible          = false;
            etObservacoes_Moto.IsVisible     = false;
            lbTipoVeiculo.IsVisible          = false;
            etTipoVeiculo_Moto.IsVisible     = false;

            #endregion
        }

        #endregion

        #region MostraDados_2_Moto()

        public void MostraDados_2_Moto()
        {
            #region Campos
            lbPeso.IsVisible            = true;
            etPeso_Moto.IsVisible       = true;
            lbPeso2.IsVisible           = true;
            lbVolume.IsVisible          = true;
            etVolume_Moto.IsVisible     = true;
            lbLargura.IsVisible         = true;
            etLargura_Moto.IsVisible    = true;
            lbLargura2.IsVisible        = true;
            lbAltura.IsVisible          = true;
            etAltura_Moto.IsVisible     = true;
            lbAltura2.IsVisible         = true;
                                        
            lbDataMax.IsVisible              = true;
            etDataMax_Moto.IsVisible         = true;
            lbHorario.IsVisible              = true;
            etHorario_Moto.IsVisible         = true;
            lbValorPretendido.IsVisible      = true;
            etValorPretendidoRS_Moto.IsVisible = true;
            etValorPretendido_Moto.IsVisible = true;
            lbObservacoes.IsVisible          = true;
            etObservacoes_Moto.IsVisible     = true;
            lbTipoVeiculo.IsVisible          = true;
            etTipoVeiculo_Moto.IsVisible     = true;

            #endregion
        }

        #endregion

        #region EscondeOrcamento()

        public void EscondeOrcamento()
        {
            #region Campos
            
            lbStatusOrcamento.IsVisible       = false;
            etStatusOrcamento.IsVisible       = false;
            lbValorOrcamento.IsVisible        = false;
            etValor_Moto.IsVisible            = false;
            etValorRS_Moto.IsVisible          = false;
            lbObs_Orcamento.IsVisible         = false;
            etObservacoes_Orcamento.IsVisible = false;

            #endregion
        }

        #endregion

        #region MostraOrcamento()

        public void MostraOrcamento()
        {
            #region Campos
            
            lbStatusOrcamento.IsVisible       = true;
            etStatusOrcamento.IsVisible       = true;
            lbValorOrcamento.IsVisible        = true;
            etValor_Moto.IsVisible            = true;
            etValorRS_Moto.IsVisible          = true;
            lbObs_Orcamento.IsVisible         = true;
            etObservacoes_Orcamento.IsVisible = true;

            #endregion
        }

        #endregion

        #endregion

        #endregion

        //----------------------------------------------------------------------------------------------------------

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

        #region Btn - Chat
        private async void BtnChat_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Views.LgChat());
        }
        #endregion

        #region Btn - Orçamentos 
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
 