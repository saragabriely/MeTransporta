using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Teste03.ClassesComuns;
using Teste03.Controllers;
using Teste03.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Teste03.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LgVeiculos : ContentPage
	{
        #region variáveis e controllers

        public static int    idTipoUser =  Session.Instance.IdTipoUsuario;
        public static int    idCli      =  Session.Instance.IdCliente;   // 8; //
        public static int    id         =  Session.Instance.IdMotorista; // 1; //
        public static int    idVeic;   
        public static int    verificaOperacao = 0;
        public static int    idVeiculo;
        public static int    idMotorista      = id;

        public static string Placa;
        public static string Modelo;
        public static string Marca;
        public static string Renavam;
        public static string Chassi;
        public static int    AnoFabricacao;
        public static int    IdTipoVeiculo;
        public static string CarroceriaAltura;
        public static string CarroceriaLargura;
        public static string CarroceriaComprimento;
        public static string Refrigeracao;
        public static string CapacidadeCarga;
        public static int    IdStatus;
        public static string TipoVeiculoDesc;
        public static string TipoCarroceriaDesc;
        public static int    IdTipoCarroceria;

        VeiculoController veiculoController = new VeiculoController();
        StatusController  statusController  = new StatusController();

        #endregion

        public LgVeiculos()
        {
            InitializeComponent();

            ListaAsync();       // Mostra a lista de veículos cadastrados
        }
        
        public LgVeiculos(Veiculo veiculo)
        {
            if(veiculo != null)
            {
                Popula(veiculo);
            }
        }

        #region Popula os campos com dados do campo

        public void Popula(Veiculo veiculo)
        {
            if(veiculo != null)
            {
                slMeusVeiculos.IsVisible       = false;
                slAdicionar.IsVisible          = true;
                btnAdicionarVeiculos.IsVisible = false;

                DadosNotEnabled();

                #region Popula os campos

                etPlaca.Text          = veiculo.Placa;
                etModelo.Text         = veiculo.Modelo;
                etMarca.Text          = veiculo.Marca;
                etRenavam.Text        = veiculo.Renavam;
                etChassi.Text         = veiculo.Chassi;
                etAnoFabr.Text        = veiculo.AnoFabricacao.ToString();
                etAltura.Text         = veiculo.CarroceriaAltura;
                etLargura.Text        = veiculo.CarroceriaLargura;
                etComprimento.Text    = veiculo.CapacidadeCarga;
                etRefrigerado.Text    = veiculo.Refrigeracao;
                etCapacidade.Text     = veiculo.CapacidadeCarga;
                etTipoVeiculo.Text    = veiculo.TipoVeiculoDesc;
                etTipoCarroceria.Text = veiculo.TipoCarroceriaDesc;

                #endregion
                
                btnAvancar.IsVisible        = false;
                stBtnVoltar.IsVisible       = true;
                stBtnAvancar.IsVisible      = true;
                slEditarVeiculos.IsVisible  = true;
                slExcluirVeiculos.IsVisible = true;

                idVeic = veiculo.IdVeiculo;
            }
        } 

        #endregion
        
        #region Lista

        #region ListaAsync()
        public async void ListaAsync()
        {
            List<Veiculo> _list = await veiculoController.GetListVeiculo(id);

            if (_list == null || _list.Count == 0)
            {
                LstVeiculos.IsVisible = false;

                lbListaVazia.IsVisible = true;
            }
            else
            {
                lbListaVazia.IsVisible = false;

                LstVeiculos.IsVisible = true;
                LstVeiculos.ItemsSource = _list;
            } 
        }
        #endregion

        #region Lista de veículos - Item selecionado
        private void LstVeiculos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            // obtem o item do listview
            var carro = e.SelectedItem as Veiculo;

            // deseleciona o item do listview
            LstVeiculos.SelectedItem = null;

            // Popula os campos como o objeto retornado
            Popula(carro);

            idVeiculo = carro.IdVeiculo;
        }
        #endregion

        #endregion

        #region Botões

        #region CRUD

        #region Btn - Excluir Veículo
        private async void BtnExcluirVeiculos_Clicked(object sender, EventArgs e)
        {
            if(await DisplayAlert("Excluir", "Deseja mesmo excluir este veículo?", "OK", "Cancelar"))
            {
                await veiculoController.DeleteVeiculo(idVeiculo);

                await DisplayAlert("Excluído", "Veículo excluído com sucesso!", "OK");

                await Navigation.PushModalAsync(new Views.LgVeiculos());
            }
        }
        #endregion

        #region Btn - Editar Veiculos
        private void BtnEditarVeiculos_Clicked(object sender, EventArgs e)
        {
            // Libera os campos para serem alterados
            DadosEnabled();

            #region Botões - Mostrar e esconder

            slEditarVeiculos.IsVisible  = false;        // EDITAR
            slExcluirVeiculos.IsVisible = false;        // EXCLUIR

            btnAvancar.IsVisible   = true;              // AVANÇAR

            stBtnVoltar.IsVisible  = false;              // VOLTAR
            stBtnAvancar.IsVisible = false;              // AVANÇAR

            verificaOperacao = 2;

            #endregion
        }
        #endregion

        #endregion

        #region Btn (Busca) - Editar - Excluir

        #region Btn - Avançar - Buscar
        private void BtnAvancar2_Clicked(object sender, EventArgs e)
        {
            ChassiNotVisible();
            DimensoesVisible();
            stBtnAvancar.IsVisible = false;

            slEditarVeiculos.IsVisible  = true;
            slExcluirVeiculos.IsVisible = true;
        }
        #endregion

        #region Btn - Voltar - Busca
        private void BtnVoltar2_Clicked(object sender, EventArgs e)
        {
            if (stBtnAvancar.IsVisible)
            {
                slMeusVeiculos.IsVisible    = true;
                slAdicionar.IsVisible       = false;
                slEditarVeiculos.IsVisible  = false;
                slExcluirVeiculos.IsVisible = false;
            }
            else
            {
                DimensoesNotVisible();
                ChassiVisible();
                stBtnAvancar.IsVisible      = true;
                slEditarVeiculos.IsVisible  = true;
                slExcluirVeiculos.IsVisible = true;
            }
        }
        #endregion

        #endregion
        
        #region Btn - Meus Veiculos
        private void BtnMeusVeiculos_Clicked(object sender, EventArgs e)
        {
            slMeusVeiculos.IsVisible       = true;
            slAdicionar.IsVisible          = false;

            btnAdicionarVeiculos.IsVisible = true;
        }
        #endregion

        #region Btn - Adicionar Veículos
        private void BtnAdicionar_Clicked(object sender, EventArgs e)
        {
            slMeusVeiculos.IsVisible       = false;
            slAdicionar.IsVisible          = true;

            btnAdicionarVeiculos.IsVisible = false;

            verificaOperacao = 1;
        }
        #endregion
        
        #region Btn - Avançar
        private async void BtnAvancar_Clicked(object sender, EventArgs e)
        {
            if (verificaOperacao == 1)     // insert
            {
                await VerificaCamposAsync(1);
            }
            else                           // update
            {
                await VerificaCamposAsync(2);
            }
        }
        #endregion

        #region Btn - Voltar - Cadastro
        private void BtnVoltar_Clicked(object sender, EventArgs e)
        {
            // Mostra
            ChassiVisible();
            btnAvancar.IsVisible    = true;

            // Esconde
            btnSalvar.IsVisible     = false;
            btnFinalizar.IsVisible  = false;
            btnVoltar.IsVisible     = false;

            DimensoesNotVisible();
        }
        #endregion

        #endregion

        #region Verifica campos (int)
        private async Task VerificaCamposAsync(int operacao)
        {
            Veiculo veiculo;

            #region Mensagens de retorno
            string nulo        = "Preencha o campo: ";
            string finalizado  = "Cadastro finalizado com sucesso!";
            string campoObrig  = "Campo obrigatório";
            string dadosInsuf  = "Dados insuficientes no campo: ";
            string dadosIns    = "Dados insuficientes";
            string caracterEsp = "Caracteres especiais";
            #endregion

            #region Parâmetros
            Placa                 = etPlaca.Text;
            Modelo                = etModelo.Text;
            Marca                 = etMarca.Text;
            Renavam               = etRenavam.Text;
            Chassi                = etChassi.Text;
            AnoFabricacao         = Convert.ToInt32(etAnoFabr.Text);
            IdTipoVeiculo         = 2;
            CarroceriaAltura      = etAltura.Text;
            CarroceriaLargura     = etLargura.Text;
            CarroceriaComprimento = etChassi.Text;
            Refrigeracao          = etRefrigerado.Text;
            CapacidadeCarga       = etCapacidade.Text;
            IdStatus              = 4;
            TipoVeiculoDesc       = etTipoVeiculo.Text;
            TipoCarroceriaDesc    = etTipoCarroceria.Text;
            IdTipoCarroceria      = 0;
            #endregion

            try
            {
                if (btnChassi.IsEnabled && etPlaca.IsVisible)
                {
                    #region Validação dos campos

                    if (Placa == null)                                                  // PLACA
                    {
                        await DisplayAlert(campoObrig, nulo + lblPlaca.Text, "OK");
                    }
                    else if(Placa.Length < 7)
                    {
                        await DisplayAlert(dadosIns, dadosInsuf + lblPlaca.Text, "OK");
                    }

                    else if (AnoFabricacao.ToString() == null)                          // ANO FABRICAÇÃO
                    {
                        await DisplayAlert(campoObrig, nulo + lblAnoFabr.Text, "OK");
                    }
                    else if(AnoFabricacao < 4)
                    {
                        await DisplayAlert(dadosIns, dadosInsuf + lblAnoFabr.Text, "OK");
                    }
                    else if (ValidaCampos.CaracterEspecial(AnoFabricacao.ToString()))
                    {
                        await DisplayAlert(caracterEsp, "O campo '" + lblAnoFabr.Text + "' aceita apenas números", "OK");
                    }

                    else if (string.IsNullOrEmpty(Modelo))                              // MODELO
                    {
                        await DisplayAlert(campoObrig, nulo + lblModelo.Text, "OK");
                    }
                    else if(Modelo.Length < 4)
                    {
                        await DisplayAlert(dadosIns, dadosInsuf + lblModelo.Text, "OK");
                    }
                 
                    else if (string.IsNullOrEmpty(Marca))                               // MARCA
                    {
                        await DisplayAlert(campoObrig, nulo + lblMarca.Text, "OK");
                    }
                    else if(Marca.Length < 3)
                    {
                        await DisplayAlert(dadosIns, dadosInsuf + lblMarca.Text, "OK");      
                    }

                    else if (string.IsNullOrEmpty(TipoVeiculoDesc))                     // TIPO VEICULO
                    {
                        await DisplayAlert(campoObrig, nulo + lblTipoVeiculo.Text, "OK");
                    }
                    else if(TipoVeiculoDesc.Length < 3)
                    {
                        await DisplayAlert(dadosIns, dadosInsuf + lblTipoVeiculo, "OK");
                    }
                    /*
                    else if (ValidaCampos.CaracterEspecial(TipoVeiculoDesc))
                    {
                        await DisplayAlert(caracterEsp, "O campo '" + lblTipoVeiculo.Text + "' aceita apenas letras e números", "OK");
                    } */

                    else if (Chassi == null)                                            // CHASSI
                    {
                        await DisplayAlert(campoObrig, nulo + lblChassi.Text, "OK");
                    }
                    else if(Chassi.Length < 17)
                    {
                        await DisplayAlert(dadosIns, dadosInsuf + lblChassi.Text, "OK");
                    }
                    else if (ValidaCampos.CaracterEspecial(Chassi))
                    {
                        await DisplayAlert(caracterEsp, "O campo '" + lblChassi.Text + "' aceita apenas letras e números", "OK");
                    }

                    else if (Renavam == null)                                           // RENAVAM
                    {
                        await DisplayAlert("Campo obrigatório", nulo + lblRenavam.Text, "OK");
                    }
                    else if(Renavam.Length < 11)
                    {
                        await DisplayAlert(dadosIns, dadosInsuf + lblRenavam.Text, "OK");
                    }

                    else
                    {
                        ChassiNotVisible();
                        btnDimensoes.IsEnabled = true;
                        btnAvancar.IsVisible   = false;

                        if (operacao == 1) {  btnFinalizar.IsVisible = true; }
                        else               {  btnSalvar.IsVisible    = true; }

                        btnVoltar.IsVisible        = true;

                        DimensoesVisible();
                    }
                    #endregion
                }
                else if(btnDimensoes.IsEnabled && lblCapacidade.IsVisible){

                    #region Validação dos campos - 2
                    if (CapacidadeCarga == null)                                    // CAPACIDADE DE CARGA
                    {
                        await DisplayAlert(campoObrig, nulo + lblCapacidade.Text, "OK");
                    }
                    
                    else if (CapacidadeCarga.Length < 3)
                    {
                        await DisplayAlert(dadosIns, dadosInsuf + lblCapacidade.Text, "OK");
                    } 

                    else if (string.IsNullOrEmpty(TipoCarroceriaDesc))              // TIPO CARROCERIA
                    {
                        await DisplayAlert(campoObrig, nulo + lblTipoCarroceria.Text, "OK");
                    }

                    else if (TipoCarroceriaDesc.Length < 5)
                    {
                        await DisplayAlert(dadosIns, dadosInsuf + lblTipoCarroceria.Text, "OK");
                    }
                    /*
                    else if (ValidaCampos.CaracterEspecial(TipoCarroceriaDesc))
                    {
                        await DisplayAlert(caracterEsp, "O campo '" + lblTipoCarroceria.Text + "' aceita apenas letras e números", "OK");
                    } */

                    else if (string.IsNullOrEmpty(CarroceriaAltura))                // CARROCERIA ALTURA
                    {
                        await DisplayAlert("Campo obrigatório", nulo + lblAltura.Text, "OK");
                    }
                    /*
                    else if (CarroceriaAltura.Length < 3)
                    {
                        await DisplayAlert(dadosIns, dadosInsuf + lblAltura.Text, "OK");
                    } */

                    else if (string.IsNullOrEmpty(CarroceriaLargura))               // CARROCERIA LARGURA
                    {
                        await DisplayAlert("Campo obrigatório", nulo + lblLargura.Text, "OK");
                    }

                    /*
                    else if (CarroceriaLargura.Length < 3)
                    {
                        await DisplayAlert(dadosIns, dadosInsuf + lblLargura.Text, "OK");
                    }
                    */

                    else if (string.IsNullOrEmpty(CarroceriaComprimento))               // CARROCERIA COMPRIMENTO
                    {
                        await DisplayAlert(campoObrig, nulo + lblComprimento.Text, "OK");
                    }
                    /*
                    else if (CarroceriaComprimento.Length < 3)
                    {
                        await DisplayAlert(dadosIns, dadosInsuf + lblComprimento.Text, "OK");
                    }
                    */

                    else if (string.IsNullOrEmpty(Refrigeracao))                        // REFRIGERAÇÃO
                    {
                        await DisplayAlert(campoObrig, nulo + lblRefrigerado.Text, "OK");
                    }
                    else if (Refrigeracao.Length < 3)
                    {
                        await DisplayAlert(dadosIns, dadosInsuf + lblRefrigerado.Text, "OK");
                    }

                    #endregion

                    else
                    {
                        DimensoesNotVisible();

                        // status
                        var status = await statusController.GetStatus(IdStatus);
                        
                        #region Veiculo()

                        veiculo = new Veiculo()
                        {
                             IdMotorista           = idMotorista,
                             Placa                 = Placa,
                             Modelo                = Modelo,
                             Marca                 = Marca,
                             Renavam               = Renavam,
                             Chassi                = Chassi,
                             AnoFabricacao         = AnoFabricacao,
                             IdTipoVeiculo         = IdTipoVeiculo,
                             CarroceriaAltura      = CarroceriaAltura,
                             CarroceriaLargura     = CarroceriaLargura,
                             CarroceriaComprimento = CapacidadeCarga,
                             Refrigeracao          = Refrigeracao,
                             CapacidadeCarga       = CapacidadeCarga,
                             IdStatus              = IdStatus,
                             TipoVeiculoDesc       = TipoVeiculoDesc,
                             TipoCarroceriaDesc    = TipoCarroceriaDesc,
                             IdTipoCarroceria      = IdTipoCarroceria,
                             DescStatus            = status.DescricaoStatus
                        };
                        #endregion

                        if(operacao == 1)     // INSERT ------------------------
                        {
                            await veiculoController.PostVeiculoAsync(veiculo);
                            // DisplayAlert("Finalizado", finalizado, "OK");

                            lblFinalizado.IsVisible = true;
                            lblFinalizado.Text      = finalizado;
                        }
                        else if(operacao == 2) // UPDATE -----------------------
                        {
                            await veiculoController.UpdateVeiculo(veiculo, idVeic);
                        }

                        // Views
                        slMeusVeiculos.IsVisible = true;
                        slAdicionar.IsVisible    = false;

                        // Botões
                        btnAdicionarVeiculos.IsVisible = true;

                        btnSalvar.IsVisible = false;
                        btnVoltar.IsVisible = false;

                        await DisplayAlert("Sucesso!", "Cadastro finalizado com sucesso!", "OK");

                        // Chama a lista novamente para trazer os novos dados
                        await Navigation.PushModalAsync(new Views.LgVeiculos());
                        //ListaAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Source != null)
                    Console.WriteLine("Exception source: {0}", ex.Source);
                throw;
            }
        }
        #endregion

        #region Dados Enabled

        #region DadosEnabled()

        public void DadosEnabled()
        {
            #region Enabled = true

                etPlaca.IsEnabled          = true;
                etModelo.IsEnabled         = true;
                etMarca.IsEnabled          = true;
                etRenavam.IsEnabled        = true;
                etChassi.IsEnabled         = true;
                etAnoFabr.IsEnabled        = true;
                etAltura.IsEnabled         = true;
                etLargura.IsEnabled        = true;
                etComprimento.IsEnabled    = true;
                etRefrigerado.IsEnabled    = true;
                etCapacidade.IsEnabled     = true;
                etTipoVeiculo.IsEnabled    = true;
                etTipoCarroceria.IsEnabled = true;

                #endregion
        }

        #endregion

        #region DadosNotEnabled()

        public void DadosNotEnabled()
        {
            #region Enabled = true

                etPlaca.IsEnabled          = false;
                etModelo.IsEnabled         = false;
                etMarca.IsEnabled          = false;
                etRenavam.IsEnabled        = false;
                etChassi.IsEnabled         = false;
                etAnoFabr.IsEnabled        = false;
                etAltura.IsEnabled         = false;
                etLargura.IsEnabled        = false;
                etComprimento.IsEnabled    = false;
                etRefrigerado.IsEnabled    = false;
                etCapacidade.IsEnabled     = false;
                etTipoVeiculo.IsEnabled    = false;
                etTipoCarroceria.IsEnabled = false;

                #endregion
        }

        #endregion

        #endregion

        #region Mostrar e ocultar campos

        #region Chassi visível
        void ChassiVisible()
        {
            lblPlaca.IsVisible   = true;
            etPlaca.IsVisible    = true;
            lblAnoFabr.IsVisible = true;
            etAnoFabr.IsVisible  = true;
            lblModelo.IsVisible  = true;
            etModelo.IsVisible   = true;
            lblMarca.IsVisible   = true;
            etMarca.IsVisible    = true;
            lblTipoVeiculo.IsVisible = true;
            etTipoVeiculo.IsVisible  = true;
            lblChassi.IsVisible  = true;
            etChassi.IsVisible   = true;
            lblRenavam.IsVisible = true;
            etRenavam.IsVisible  = true;

            btnChassi.IsEnabled  = true;
        }
        #endregion

        #region Chassi invisivel
        void ChassiNotVisible()
        {
            lblPlaca.IsVisible      = false;
            etPlaca.IsVisible       = false;
            lblAnoFabr.IsVisible    = false;
            etAnoFabr.IsVisible     = false;
            lblModelo.IsVisible     = false;
            etModelo.IsVisible      = false;
            lblMarca.IsVisible      = false;
            etMarca.IsVisible       = false;
            lblTipoVeiculo.IsVisible = false;
            etTipoVeiculo.IsVisible  = false;
            lblChassi.IsVisible     = false;
            etChassi.IsVisible      = false;
            lblRenavam.IsVisible    = false;
            etRenavam.IsVisible     = false;

            btnChassi.IsEnabled     = true;
        }
        #endregion

        #region Dimensoes visiveis
        void DimensoesVisible()
        {
            lblCapacidade.IsVisible     = true;
            lblCapacidade2.IsVisible    = true;
            etCapacidade.IsVisible      = true;
            lblTipoCarroceria.IsVisible = true;
            etTipoCarroceria.IsVisible  = true;
            lblDimensoesBau.IsVisible   = true;
            lblAltura.IsVisible         = true;
            lblAltura2.IsVisible        = true;
            etAltura.IsVisible          = true;
            lblLargura.IsVisible        = true;
            lblLargura2.IsVisible       = true;
            etLargura.IsVisible         = true;
            lblComprimento.IsVisible    = true;
            lblComprimento2.IsVisible   = true;
            etComprimento.IsVisible     = true;
            lblRefrigerado.IsVisible    = true;
            etRefrigerado.IsVisible     = true;

            btnDimensoes.IsEnabled      = true;
        }
        #endregion

        #region Dimensoes invisiveis
        void DimensoesNotVisible()
        {
            lblCapacidade.IsVisible     = false;
            lblCapacidade2.IsVisible    = false;
            etCapacidade.IsVisible      = false;
            lblTipoCarroceria.IsVisible = false;
            etTipoCarroceria.IsVisible  = false;
            lblDimensoesBau.IsVisible   = false;
            lblAltura.IsVisible         = false;
            lblAltura2.IsVisible        = false;
            etAltura.IsVisible          = false;
            lblLargura.IsVisible        = false;
            lblLargura2.IsVisible       = false;
            etLargura.IsVisible         = false;
            lblComprimento.IsVisible    = false;
            lblComprimento2.IsVisible   = false;
            etComprimento.IsVisible     = false;
            lblRefrigerado.IsVisible    = false;
            etRefrigerado.IsVisible     = false;

            btnDimensoes.IsEnabled      = true;
        }
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