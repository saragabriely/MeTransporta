using System;
using System.Linq;
using System.Threading.Tasks;
using Teste03.ClassesComuns;
using Teste03.Controllers;
using Teste03.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Teste03.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Motorista_Cadastro : ContentPage
	{
        #region Variaveis - Controllers - Referência as classes utilizadas

        public int verificaOperacao;
        public int idCli;
        public int idMoto;
        public int verificaOperacao_;

        Cliente   cliente;
        Motorista motorista;

        ClienteController       control          = new ClienteController();
        CartaoController        controlCartao    = new CartaoController();
        ContaBancariaController contaControl     = new ContaBancariaController();
        LoginController         loginController  = new LoginController();
        MotoristaController     motoristaControl = new MotoristaController();
        
        #endregion
        
        public Motorista_Cadastro ()
		{
			InitializeComponent ();

            // Session.Instance.IdCliente = 8;

            #region Usuário Logado
            
            if (Session.Instance.IdCliente != 0)
            {
                // Esconde os campos e botões
                DadosPessoaisNotVisible();

                stMeuCadastro.IsVisible     = false;

                stMeuCadastro_2.IsVisible   = true;

                stCadastrarSe.IsVisible     = false;

                btnAvancar.IsVisible        = false;

                lblCadastrar.IsVisible      = false;

                lblMeuCadastro.IsVisible    = true;

                grdLogado.IsVisible         = true;         // Menu inferior - Logado

                grdNaoLogado.IsVisible      = false;         // Menu inferior - Não logado

               // verificaOperacao = 2;
            }
            #endregion

            else
            {
                verificaOperacao = 1; // cadastro
            }
        }

        #region NÃO LOGADO

        #region Conhecer o APP
        private async void BtnConhecerApp_Clicked(object sender, EventArgs e)
        {
            if ((etMotoristaNome.Text != null && btnVoltar.IsVisible) || (etMotoristaNome.Text == null && !btnVoltar.IsVisible))
            {
                await Navigation.PushModalAsync(new Views.ConhecerApp());
            }
            else if (await DisplayAlert("Deseja sair?", "Tem certeza que deseja sair? Todos os dados serão perdidos.",
                    "OK", "Cancelar"))
            {
                await Navigation.PushModalAsync(new Views.ConhecerApp());
            }
        }
        #endregion

        #region Btn - Cadastrar-se
        private async void BtnCadastreSe_Clicked(object sender, EventArgs e)
        {
            if ((etMotoristaNome.Text != null && btnVoltar.IsVisible) || (etMotoristaNome.Text == null && !btnVoltar.IsVisible))
            {
                await Navigation.PushModalAsync(new Views.CadastreSe());
            }
            else if (await DisplayAlert("Deseja sair?", "Tem certeza que deseja sair? Todos os dados serão perdidos.",
                    "OK", "Cancelar"))
            {
                await Navigation.PushModalAsync(new Views.CadastreSe());
            }
        }
        #endregion

        #region Btn - Login
        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            if ((etMotoristaNome.Text != null && btnVoltar.IsVisible) || (etMotoristaNome.Text == null && !btnVoltar.IsVisible))
            {
                await Navigation.PushModalAsync(new Views.Login());
            }
            else if (await DisplayAlert("Deseja sair?", "Tem certeza que deseja sair? Todos os dados serão perdidos.",
                    "OK", "Cancelar"))
            {
                await Navigation.PushModalAsync(new Views.Login());
            }
        }
        #endregion

        #endregion

        #region VerificaCamposAsync()

        private async Task VerificaCamposAsync(int i)
        {
            #region Mensagens de retorno
            string vazio        = "Campo vazio";       
            string nulo         = "Preencha o campo: ";
            string finalizado   = "Cadastro finalizado com sucesso!";
            string emails       = "Os e-mails não coincidem!";
            string senhas       = "As senhas não coincidem!";
            string dadosInsuf   = "Dados insuficientes no campo: ";
            string caracter     = "': este campos não aceita caracteres especiais.";
            string caracterInv  = "Caracteres inválidos";
            string senhaCurta   = "A senha deve ter mais de 6 caracteres!";
            #endregion
 
            #region Variáveis

            int IdTipoUsuario = 3;
            int IdStatus      = 4;

            #region Dados Pessoais
            string nome       = etMotoristaNome.Text;
            string rg         = etMotoristaRg.Text;
            string cpf        = etMotoristaCpf.Text;
            string sexo       = etMotoristaSexo.Items[etMotoristaSexo.SelectedIndex];
            string dataNascto = etMotoristaDataNascto.Text;
            string celular    = etMotoristaCelular.Text;
            string celular2   = etMotoristaCelular02.Text;

            #endregion

            #region Endereço
            string endereco    = etMotoristaEndereco.Text;
            string numero      = etMotoristaNumero.Text;
            string complemento = etMotoristaCompl.Text;
            string bairro      = etMotoristaBairro.Text;
            string cidade      = etMotoristaCidade.Text;
            string cep         = etMotoristaCep.Text;
            string uf          = etMotoristaUf.Text;
            #endregion

            #region CNH
            string numCnh           = etMotoristaNumCnh.Text;
            /* string categoriaCnh  = etMotoristaCategoriaCnh.Items[etMotoristaCategoriaCnh.SelectedIndex]; */
            string categoriaCnh     = etMotoristaCategoriaCnh.Text;
            string validadeCnh      = etMotoristaValidadeCnh.Text;

            #endregion

            #region Dados bancários

            int      idContaBancaria = 0;
            int      idBanco         = 1;
            int      agencia         = Convert.ToInt32(etMotoristaAgencia.Text);
            int      digAgencia      = Convert.ToInt32(etMotoristaDigitoAgencia.Text);
            int      conta           = Convert.ToInt32(etMotoristaConta.Text);
            int      digConta        = Convert.ToInt32(etMotoristaDigitoConta.Text);
            int      idTipoConta     = 1;
            DateTime dataCadastro    = DateTime.Now;
            DateTime ultimaAtualizacao;
            string   bancoDesc       = etMotoristaBanco.Text;
            string   tipoContaBan    = etMotoristaTipoConta.Text;

            #endregion

            #region Email e senha
            string email     = etMotoristaEmail.Text;
            string confemail = etMotoristaConfEmail.Text;
            string senha     = etMotoristaSenha.Text;
            string confsenha = etMotoristaConfSenha.Text;
            #endregion

            #endregion

            try
            {
                #region Valida - Dados Pessoais
                if (btnDadosPessoais.IsEnabled && etMotoristaNome.IsVisible)
                {
                    #region Campos
                    if (string.IsNullOrEmpty(nome))                     // NOME 
                    {
                        await DisplayAlert(vazio, nulo + lblMotoristaNome.Text, "OK");
                    }
                    else if (nome.Length < 6)
                    {
                        await DisplayAlert("Dados insuficientes", dadosInsuf + lblMotoristaNome.Text, "OK");
                    }

                    else if (string.IsNullOrEmpty(rg))                  // RG   
                    {
                        await DisplayAlert(vazio, nulo + lblMotoristaRg.Text, "OK");
                    }
                    else if (rg.Length < 9)
                    {
                        await DisplayAlert("Dados insuficientes", dadosInsuf + lblMotoristaRg.Text, "OK");
                    }
                    else if (ValidaCampos.CaracterEspecial(rg))
                    {
                        await DisplayAlert(caracterInv, "'" + lblMotoristaRg.Text + caracter, "OK");
                    }

                    else if (string.IsNullOrEmpty(cpf))                 // CPF
                    {
                        await DisplayAlert(vazio, nulo + lblMotoristaCpf.Text, "OK");
                    }
                    else if (cpf.Length < 11)
                    {
                        await DisplayAlert("Dados insuficientes", dadosInsuf + lblMotoristaCpf.Text, "OK");
                    }
                    else if (ValidaCampos.CaracterEspecial(cpf))
                    {
                        await DisplayAlert(caracterInv, "'" + lblMotoristaCpf.Text + caracter, "OK");
                    }

                    /*
                    else if (string.IsNullOrEmpty(sexo))                // SEXO
                    {
                        lblAlerta.IsVisible = true;
                        lblAlerta.Text = "";
                        lblAlerta.Text = nulo + lblMotoristaSexo.Text; }        */

                    else if (string.IsNullOrEmpty(dataNascto))          // DATA DE NASCIMENTO
                    {
                        await DisplayAlert(vazio, nulo + lblMotoristaDataNascto.Text, "OK");
                    }
                    else if (dataNascto.Length < 8)
                    {
                        await DisplayAlert("Dados insuficientes", dadosInsuf + lblMotoristaDataNascto.Text, "OK");
                    }

                    else if (string.IsNullOrEmpty(celular))             // CELULAR
                    {
                        await DisplayAlert(vazio, nulo + lblMotoristaCelular.Text, "OK");
                    }
                    else if (celular.Length < 9)
                    {
                        await DisplayAlert("Dados insuficientes", dadosInsuf + lblMotoristaCelular.Text, "OK");
                    }
                    else if (ValidaCampos.CaracterEspecial(celular))
                    {
                        await DisplayAlert(caracterInv, "'" + lblMotoristaCelular.Text + caracter, "OK");
                    }

                    else if (string.IsNullOrEmpty(celular2))            // CELULAR 2
                    {
                        await DisplayAlert(vazio, nulo + lblMotoristaCelular02.Text, "OK");
                    }
                    else if (celular2.Length < 9)
                    {
                        await DisplayAlert("Dados insuficientes", dadosInsuf + lblMotoristaCelular02.Text, "OK");
                    }
                    else if (ValidaCampos.CaracterEspecial(celular2))
                    {
                        await DisplayAlert(caracterInv, "'" + lblMotoristaCelular02.Text + caracter, "OK");
                    }
                    #endregion

                    else
                    {
                        #region Comentado - Valida CPF ****

                        /*
                         var verificar = await control.GetCpf(cpf);

                        string verificaCpf = verificar.Ccpf.ToString();

                        if (verificar != null)
                        {
                            lblAlerta.IsVisible = true;
                            lblAlerta.Text = "";
                            lblAlerta.Text = "CPF já cadastrado!";
                        }
                         */

                        //  var verificar = await control.GetCpf(cpf);

                        //string verificaCpf = verificar.Ccpf.ToString();

                        //if (verificar.Ccpf.ToString() == null)
                        // {
                        /*
                        DadosPessoaisNotVisible();

                        btnEndereco.IsEnabled = true;

                        btnAvancar.IsVisible = false;
                        btnAvancar2.IsVisible = true;
                        btnVoltar.IsVisible = true;

                        lblAlerta.IsVisible = false;

                        EnderecoVisible(); */
                        //  }
                        //  else
                        //  {
                        //     lblAlerta.IsVisible = true;
                        //     lblAlerta.Text = "";
                        //      lblAlerta.Text = "CPF já cadastrado!";


                        // }

                        #endregion

                        DadosPessoaisNotVisible();

                        if (verificaOperacao == 1)
                        {
                            btnEndereco.IsEnabled = true;
                        }

                        btnAvancar.IsVisible = false;
                        btnAvancar2.IsVisible = true;

                        if (verificaOperacao == 1)       // INSERT
                        {
                            btnVoltar.IsVisible = true;
                        }
                        else                            // UPDATE
                        { stBtnVoltar.IsVisible = true; }

                        lblAlerta.IsVisible = false;

                        EnderecoVisible();
                    }
                }
                #endregion

                #region Valida - Endereço
                else if (btnEndereco.IsEnabled && etMotoristaEndereco.IsVisible)
                {
                    #region Campos

                    if (string.IsNullOrEmpty(endereco))                     // ENDEREÇO
                    {
                        await DisplayAlert(vazio, nulo + lblMotoristaEndereco.Text, "OK");
                    }
                    else if (endereco.Length < 5)
                    {
                        await DisplayAlert("Dados insuficientes", dadosInsuf + lblMotoristaEndereco.Text, "OK");
                    }

                    else if (string.IsNullOrEmpty(numero))                      // NUMERO
                    {
                        await DisplayAlert(vazio, nulo + lblMotoristaNumero.Text, "OK");
                    }
                    else if (ValidaCampos.CaracterEspecial(numero))
                    {
                        await DisplayAlert(caracterInv, "'" + lblMotoristaNumero.Text + caracter, "OK");
                    }

                    else if (string.IsNullOrEmpty(complemento))                 // COMPLEMENTO
                    {
                        await DisplayAlert(vazio, nulo + lblMotoristaCompl.Text, "OK");
                    }

                    else if (string.IsNullOrEmpty(bairro))                      // BAIRRO
                    {
                        await DisplayAlert(vazio, nulo + lblMotoristaBairro.Text, "OK");
                    }
                    else if (bairro.Length < 4)
                    {
                        await DisplayAlert("Dados insuficientes", dadosInsuf + lblMotoristaBairro.Text, "OK");
                    }

                    else if (string.IsNullOrEmpty(cidade))                  // CIDADE
                    {
                        await DisplayAlert(vazio, nulo + lblMotoristaCidade.Text, "OK");
                    }
                    else if (cidade.Length < 6)
                    {
                        await DisplayAlert("Dados insuficientes", dadosInsuf + lblMotoristaCidade.Text, "OK");
                    }

                    else if (string.IsNullOrEmpty(cep))                     // CEP
                    {
                        await DisplayAlert(vazio, nulo + lblMotoristaCep.Text, "OK");
                    }
                    else if (cep.Length < 8)
                    {
                        await DisplayAlert("Dados insuficientes", dadosInsuf + lblMotoristaCep.Text, "OK");
                    }
                    else if (ValidaCampos.CaracterEspecial(cep))
                    {
                        await DisplayAlert(caracterInv, "'" + lblMotoristaCep.Text + caracter, "OK");
                    }

                    else if (string.IsNullOrEmpty(uf))                      // UF
                    {
                        await DisplayAlert(vazio, nulo + lblMotoristaUf.Text, "OK");
                    }
                    else if (uf.Length < 2)
                    {
                        await DisplayAlert("Dados insuficientes", dadosInsuf + lblMotoristaUf.Text, "OK");
                    }
                    else if (ValidaCampos.CaracterEspecial(uf))
                    {
                        await DisplayAlert(caracterInv, "'" + lblMotoristaUf.Text + caracter, "OK");
                    }
                    #endregion

                    else
                    {
                        EnderecoNotVisible();

                        if (verificaOperacao != 2)  // insert
                        {
                            btnCnhDadosBancarios.IsEnabled = true;

                            CnhDadosBancariosVisible();
                        }
                        else                        // update
                        {
                            btnAvancar2.IsVisible = false;
                            btnSalvar.IsVisible = true;

                            EmailSenhaVisible();
                        }
                        lblAlerta.IsVisible = false;

                    }
                }
                #endregion

                else if ((btnCnhDadosBancarios.IsEnabled && etMotoristaNumCnh.IsVisible) || i == 3)
                {
                    #region Valida - CNH
                    if (string.IsNullOrEmpty(numCnh))                       // CNH
                    {
                        await DisplayAlert(vazio, nulo + lblMotoristaNumCnh.Text, "OK");
                    }
                    else if (numCnh.Length < 11)
                    {
                        await DisplayAlert("Dados insuficientes", dadosInsuf + lblMotoristaNumCnh.Text, "OK");
                    }
                    else if (ValidaCampos.CaracterEspecial(numCnh))
                    {
                        await DisplayAlert(caracterInv, "'" + lblMotoristaNumCnh.Text + caracter, "OK");
                    }

                    else if (string.IsNullOrEmpty(categoriaCnh))            // CATEGORIA CNH
                    {
                        await DisplayAlert(vazio, nulo + lblMotoristaCategoriaCnh.Text, "OK");
                    }

                    else if (string.IsNullOrEmpty(validadeCnh))             // VALIDADE CNH
                    {
                        await DisplayAlert(vazio, nulo + lblMotoristaValidadeCnh.Text, "OK");
                    }
                    else if (validadeCnh.Length < 8)
                    {
                        await DisplayAlert("Dados insuficientes", dadosInsuf + lblMotoristaValidadeCnh.Text, "OK");
                    }
                    else if (ValidaCampos.CaracterEspecial(validadeCnh))
                    {
                        await DisplayAlert(caracterInv, "'" + lblMotoristaValidadeCnh.Text + caracter, "OK");
                    }

                    #endregion 

                    #region Dados bancários

                    else if (i != 3)
                    {
                        if (string.IsNullOrEmpty(bancoDesc))                       // BANCO - DESCRIÇÃO
                        {
                            await DisplayAlert(vazio, nulo + lblMotoristaBanco.Text, "OK");
                        }
                        else if (bancoDesc.Length < 2)
                        {
                            await DisplayAlert("Dados insuficientes", dadosInsuf + lblMotoristaBanco.Text, "OK");
                        }

                        else if (agencia < 0)                                              // AGENCIA
                        {
                            await DisplayAlert(vazio, nulo + lblMotoristaAgencia.Text, "OK");
                        }
                        else if (ValidaCampos.CaracterEspecial(agencia.ToString()))
                        {
                            await DisplayAlert(caracterInv, "'" + lblMotoristaAgencia.Text + caracter, "OK");
                        }

                        else if (conta < 0)                                             // DADOS
                        {
                            await DisplayAlert(vazio, nulo + lblMotoristaConta.Text, "OK");
                        }
                        else if (ValidaCampos.CaracterEspecial(conta.ToString()))
                        {
                            await DisplayAlert(caracterInv, "'" + lblMotoristaAgencia.Text + caracter, "OK");
                        }

                        else if (digConta < 0)                                          // DIGITO CONTA
                        {
                            await DisplayAlert(vazio, nulo + lblMotoristaDigitoConta.Text, "OK");
                        }
                        else if (ValidaCampos.CaracterEspecial(digConta.ToString()))
                        {
                            await DisplayAlert(caracterInv, "'" + lblMotoristaDigitoConta.Text + caracter, "OK");
                        }

                        else if (string.IsNullOrEmpty(tipoContaBan))                    // TIPO CONTA BANCÁRIA
                        {
                            await DisplayAlert(vazio, nulo + lblMotoristaTipoConta.Text, "OK");
                        }
                        else if (i != 3)
                        {
                            CnhDadosBancariosNotVisible();

                            btnAvancar2.IsVisible = false;
                            btnFinalizar.IsVisible = true;

                            lblAlerta.IsVisible = false;

                            EmailSenhaVisible();
                        }
                    }
                    #endregion

                    else if (i == 3)   // UPDATE - MOTORISTA ---------------------------------------
                    {
                         #region Motorista()
                                Motorista motorista = new Motorista()
                                {
                                    IdCliente     = idCli,
                                    MnumeroCnh    = numCnh,
                                    McategoriaCnh = categoriaCnh,
                                    MvalidadeCnh  = validadeCnh,
                                    IdStatus      = IdStatus,
                                    Ccpf          = cpf
                                };
                                #endregion

                         motorista.IdMotorista = Session.Instance.IdMotorista;

                         #region Atualiza as variaveis globais

                                Session.Instance.MnumeroCnh    = motorista.MnumeroCnh;
                                Session.Instance.McategoriaCnh = motorista.McategoriaCnh;
                                Session.Instance.MvalidadeCnh  = motorista.MvalidadeCnh;

                                #endregion

                         await motoristaControl.UpdateMotorista(motorista);

                         btnSalvar.IsVisible = false;

                         await DisplayAlert("Sucesso!", "Cadastro realizado com sucesso!", "OK");  // Confirma a atualização

                         // Direciona para a tela de login
                         await Navigation.PushModalAsync(new Views.LgMinhaContaa());
                    }
                }
                

                else if (btnEmailSenha.IsEnabled && etMotoristaEmail.IsVisible)
                {
                    #region Valida - Email e senha

                    if (string.IsNullOrEmpty(email))                        // E-MAIL
                    {
                        await DisplayAlert(vazio, nulo + lblMotoristaEmail.Text, "OK");
                    }
                    
                    else if (string.IsNullOrEmpty(senha))                   // SENHA
                    {
                        await DisplayAlert(vazio, nulo + lblMotoristaSenha.Text, "OK");
                    }
                    else if (senha.Length < 6)
                    {
                        await DisplayAlert("Dados insuficientes", senhaCurta, "OK");
                    }

                    else if (!ValidaCampos.IsEmail(email))                  // VALIDANDO E-MAIL
                    {
                        await DisplayAlert(dadosInsuf, "E-mail inválido!", "OK");
                    }
                    #endregion
                   
                    if (verificaOperacao == 1 || i == 2) // Cadastro || Update 
                    {

                        #region Insert !
                        if (verificaOperacao == 1) // Insert
                        {
                            if (string.IsNullOrEmpty(confemail))
                            {
                                await DisplayAlert(vazio, nulo + lblMotoristaConfEmail.Text, "OK");
                            }
                            else if (string.IsNullOrEmpty(confsenha))
                            {
                                await DisplayAlert(vazio, nulo + lblMotoristaConfSenha.Text, "OK");
                            }
                            else if (!email.Equals(confemail))                      // COMPARA E-MAIL
                            {
                                await DisplayAlert("E-mails", emails, "OK");
                            }
                            else if (!senha.Equals(confsenha))                      // COMPARA SENHA
                            {
                                await DisplayAlert("Senhas", senhas, "OK");
                            }
                            else
                            {
                                //-------------------------------------------------------------------------
                                // Salvando ...

                                // Dados pessoais ---------------------------------------------------------

                                #region Cliente()

                                Cliente cli = new Cliente()
                                {
                                    Cnome = nome,
                                    Crg = rg,
                                    Ccpf = cpf,
                                    Csexo = sexo,
                                    CdataNascto = dataNascto,
                                    Ccelular = celular,
                                    Ccelular2 = celular2,
                                    Cendereco = endereco,
                                    Cnumero = numero,
                                    Ccomplemento = complemento,
                                    Cbairro = bairro,
                                    Ccidade = cidade,
                                    Ccep = cep,
                                    Cuf = "vc",
                                    Cemail = email,
                                    Csenha = senha,
                                    IdTipoUsuario = IdTipoUsuario,
                                    IdStatus = IdStatus
                                };

                                #endregion

                                //-------------------------------------------------------------------------

                                #region INSERT 
                                if (i == 1)             // CADASTRO --------------------------------------
                                {
                                    await control.PostAsync(cli);

                                    // Captura o IdCliente gerado no banco  ------------------------------
                                    Cliente clien = await control.GetCpf(cpf);

                                    int idCliente = clien.IdCliente;

                                    // Dados da CNH ------------------------------------------------------

                                    #region Motorista()
                                    Motorista motorista = new Motorista()
                                    {
                                        IdCliente     = idCliente,
                                        MnumeroCnh    = numCnh,
                                        McategoriaCnh = categoriaCnh,
                                        MvalidadeCnh  = validadeCnh,
                                        IdStatus      = IdStatus,
                                        Ccpf          = cpf
                                    };
                                    #endregion

                                    await motoristaControl.PostMotoristaAsync(motorista);

                                    // Conta bancária -----------------------------------------------------

                                    #region ContaBancaria()
                                    ContaBancaria contaBan = new ContaBancaria()
                                    {
                                        IdCliente = idCliente,
                                        IdBanco = idBanco,
                                        MAgencia = agencia,
                                        MDigAgencia = digAgencia,
                                        MConta = conta,
                                        MDigConta = digConta,
                                        IdTipoConta = idTipoConta,
                                        MDataCadastro = DateTime.Now,
                                        IdStatus = IdStatus,
                                        Ccpf = cpf,
                                        MUltimaAtualizacao = null,
                                        BancoDesc = bancoDesc,
                                        TipoContaBanDesc = tipoContaBan
                                    };
                                    #endregion

                                    await contaControl.PostContaAsync(contaBan);
                                    
                                    
                                    btnFinalizar.IsVisible = false;
                                    btnVoltar.IsVisible    = false;

                                    // Direciona para a tela de login
                                    await Navigation.PushModalAsync(new Views.Login());
                                }
                                #endregion

                                //------------------------------------------------------------------------------
                            }
                        }
                        #endregion

                        else if(i == 2) // Atualizar
                        {
                            // Verifica se email / senha foram alterados
                             var clienteEmail = await control.GetCliente(Session.Instance.IdCliente);             
                            
                            #region Email alterado
                            if (Session.Instance.cliente.Cemail != email || Session.Instance.cliente.Csenha != senha)
                            {
                                if (string.IsNullOrEmpty(confemail))
                                {
                                    await DisplayAlert(vazio, nulo + lblMotoristaConfEmail.Text, "OK");
                                }
                                else if (string.IsNullOrEmpty(confsenha))
                                {
                                    await DisplayAlert(vazio, nulo + lblMotoristaConfSenha.Text, "OK");
                                }
                                else if (!email.Equals(confemail))                      // COMPARA E-MAIL
                                {
                                    await DisplayAlert("E-mails", emails, "OK");
                                }
                                else if (!senha.Equals(confsenha))                      // COMPARA SENHA
                                {
                                    await DisplayAlert("Senhas", senhas, "OK");
                                }
                                else
                                {
                                    //-------------------------------------------------------------------------
                                    // Salvando ...

                                    // Dados pessoais ---------------------------------------------------------

                                    #region Cliente()

                                    Cliente cli = new Cliente()
                                    {
                                        Cnome       = nome,
                                        Crg         = rg,
                                        Ccpf        = cpf,
                                        Csexo       = sexo,
                                        CdataNascto = dataNascto,
                                        Ccelular    = celular,
                                        Ccelular2   = celular2,
                                        Cendereco   = endereco,
                                        Cnumero     = numero,
                                        Ccomplemento = complemento,
                                        Cbairro     = bairro,
                                        Ccidade     = cidade,
                                        Ccep        = cep,
                                        Cuf         = uf,
                                        Cemail      = email,
                                        Csenha      = senha,
                                        IdTipoUsuario = IdTipoUsuario,
                                        IdStatus    = IdStatus
                                    };

                                    #endregion

                                    //------------------------------------------------------------------------------

                                    #region UPDATE

                                    cli.IdCliente = idCli;

                                    // Verifica se o e-mail foi alterado
                                    var clienteEmail_ = await control.GetCliente(idCli);

                                    await control.UpdateCliente(clienteEmail_, idCli);

                                    // Atualiza - Login

                                    var loga = await loginController.GetCpf(clienteEmail_.Ccpf);

                                    loga.Email = email;
                                    loga.Senha = senha;

                                    await loginController.UpdateLogin(loga);

                                    #region Atualiza as variáveis globais

                                    Session.Instance.IdCliente = cli.IdCliente;
                                    Session.Instance.IdTipoUsuario = cli.IdTipoUsuario;
                                    Session.Instance.Email = cli.Cemail;
                                    Session.Instance.Senha = cli.Csenha;
                                    Session.Instance.Cnome = cli.Cnome;
                                    Session.Instance.Crg = cli.Crg;
                                    Session.Instance.Ccpf = cli.Ccpf;
                                    Session.Instance.Csexo = cli.Csexo;
                                    Session.Instance.CdataNascto = cli.CdataNascto;
                                    Session.Instance.Ccelular = cli.Ccelular;
                                    Session.Instance.Ccelular2 = cli.Ccelular2;
                                    Session.Instance.Cendereco = cli.Cendereco;
                                    Session.Instance.Cnumero = cli.Cnumero;
                                    Session.Instance.Ccomplemento = cli.Ccomplemento;
                                    Session.Instance.Cbairro = cli.Cbairro;
                                    Session.Instance.Ccidade = cli.Ccidade;
                                    Session.Instance.Ccep = cli.Ccep;
                                    Session.Instance.Cuf = cli.Cuf;
                                    Session.Instance.IdStatus = cli.IdStatus;

                                    #endregion

                                    btnSalvar.IsVisible = false;

                                    await DisplayAlert("Sucesso!", "Cadastro atualizado com sucesso!", "OK");  // Confirma a atualização

                                    EmailSenhaNotVisible();

                                    // Direciona para a tela de login
                                    await Navigation.PushModalAsync(new Views.LgMinhaContaa());

                                    #endregion
                                }
                            }
                            #endregion

                            else
                            {
                                //-------------------------------------------------------------------------
                                // Salvando ...

                                // Dados pessoais ---------------------------------------------------------

                                #region Cliente()

                                Cliente cli = new Cliente()
                                {
                                    Cnome = nome,
                                    Crg = rg,
                                    Ccpf = cpf,
                                    Csexo = sexo,
                                    CdataNascto = dataNascto,
                                    Ccelular = celular,
                                    Ccelular2 = celular2,
                                    Cendereco = endereco,
                                    Cnumero = numero,
                                    Ccomplemento = complemento,
                                    Cbairro = bairro,
                                    Ccidade = cidade,
                                    Ccep = cep,
                                    Cuf = uf,
                                    Cemail = email,
                                    Csenha = senha,
                                    IdTipoUsuario = IdTipoUsuario,
                                    IdStatus = IdStatus
                                };

                                #endregion

                                //------------------------------------------------------------------------------

                                #region UPDATE

                                cli.IdCliente = idCli;

                                // Verifica se o e-mail foi alterado
                                var clienteEmail_ = await control.GetCliente(idCli);

                                await control.UpdateCliente(cli, idCli);

                                // Atualiza - Login

                                var loga = await loginController.GetCpf(clienteEmail_.Ccpf);

                                loga.Email = email;
                                loga.Senha = senha;

                                await loginController.UpdateLogin(loga);

                                #region Atualiza as variáveis globais

                                Session.Instance.IdCliente     = cli.IdCliente;
                                Session.Instance.IdTipoUsuario = cli.IdTipoUsuario;
                                Session.Instance.Email         = cli.Cemail;
                                Session.Instance.Senha         = cli.Csenha;
                                Session.Instance.Cnome         = cli.Cnome;
                                Session.Instance.Crg           = cli.Crg;
                                Session.Instance.Ccpf          = cli.Ccpf;
                                Session.Instance.Csexo         = cli.Csexo;
                                Session.Instance.CdataNascto   = cli.CdataNascto;
                                Session.Instance.Ccelular      = cli.Ccelular;
                                Session.Instance.Ccelular2     = cli.Ccelular2;
                                Session.Instance.Cendereco     = cli.Cendereco;
                                Session.Instance.Cnumero       = cli.Cnumero;
                                Session.Instance.Ccomplemento  = cli.Ccomplemento;
                                Session.Instance.Cbairro       = cli.Cbairro;
                                Session.Instance.Ccidade       = cli.Ccidade;
                                Session.Instance.Ccep          = cli.Ccep;
                                Session.Instance.Cuf           = cli.Cuf;
                                Session.Instance.IdStatus      = cli.IdStatus;

                                #endregion

                                btnSalvar.IsVisible = false;

                                await DisplayAlert("Sucesso!", "Cadastro atualizado com sucesso!", "OK");  // Confirma a atualização

                                EmailSenhaNotVisible();

                                // Direciona para a tela de login
                                await Navigation.PushModalAsync(new Views.LgMinhaContaa());
                                
                                #endregion
                            }
                        }
                        
                        else
                        {                            
                            //-------------------------------------------------------------------------
                            // Salvando ...
                            
                            // Dados pessoais ---------------------------------------------------------
                            
                            #region Cliente()
                            
                            Cliente cli = new Cliente()
                            {
                                Cnome = nome,
                                Crg = rg,
                                Ccpf = cpf,
                                Csexo = sexo,
                                CdataNascto = dataNascto,
                                Ccelular = celular,
                                Ccelular2 = celular2,
                                Cendereco = endereco,
                                Cnumero = numero,
                                Ccomplemento = complemento,
                                Cbairro = bairro,
                                Ccidade = cidade,
                                Ccep = cep,
                                Cuf = "vc",
                                Cemail = email,
                                Csenha = senha,
                                IdTipoUsuario = IdTipoUsuario,
                                IdStatus = IdStatus
                            };
                            
                            #endregion
                            
                            //------------------------------------------------------------------------------
                            
                            #region UPDATE
                            
                            cli.IdCliente = idCli;
                            
                            // Verifica se o e-mail foi alterado
                            var clienteEmail_ = await control.GetCliente(idCli);
                            
                            await control.UpdateCliente(cli, idCli);
                         
                            // await loginController.UpdateLogin(login);
                            
                            #region Atualiza as variáveis globais
                            
                            Session.Instance.IdCliente = cli.IdCliente;
                            Session.Instance.IdTipoUsuario = cli.IdTipoUsuario;
                            Session.Instance.Email = cli.Cemail;
                            Session.Instance.Senha = cli.Csenha;
                            Session.Instance.Cnome = cli.Cnome;
                            Session.Instance.Crg = cli.Crg;
                            Session.Instance.Ccpf = cli.Ccpf;
                            Session.Instance.Csexo = cli.Csexo;
                            Session.Instance.CdataNascto = cli.CdataNascto;
                            Session.Instance.Ccelular = cli.Ccelular;
                            Session.Instance.Ccelular2 = cli.Ccelular2;
                            Session.Instance.Cendereco = cli.Cendereco;
                            Session.Instance.Cnumero = cli.Cnumero;
                            Session.Instance.Ccomplemento = cli.Ccomplemento;
                            Session.Instance.Cbairro = cli.Cbairro;
                            Session.Instance.Ccidade = cli.Ccidade;
                            Session.Instance.Ccep = cli.Ccep;
                            Session.Instance.Cuf = cli.Cuf;
                            Session.Instance.IdStatus = cli.IdStatus;
                            
                            #endregion
                            
                            btnSalvar.IsVisible = false;
                            
                            await DisplayAlert("Sucesso!", "Cadastro atualizado com sucesso!", "OK");  // Confirma a atualização
                            
                            EmailSenhaNotVisible();
                            
                            // Direciona para a tela de login
                            await Navigation.PushModalAsync(new Views.LgMinhaContaa());
                            
                            
                            #endregion
                            
                        }
                        //-------------------------------------------------------------------------
                    }
                    else if (i == 3)   // UPDATE - MOTORISTA ---------------------------------------
                    {
                        #region Motorista()
                        Motorista motorista = new Motorista()
                        {
                            IdCliente     = idCli,
                            MnumeroCnh    = numCnh,
                            McategoriaCnh = categoriaCnh,
                            MvalidadeCnh  = validadeCnh,
                            IdStatus      = IdStatus,
                            Ccpf          = cpf
                        };
                        #endregion

                        motorista.IdMotorista = Session.Instance.IdMotorista;

                        await motoristaControl.UpdateMotorista(motorista);

                        btnSalvar.IsVisible = false;

                        await DisplayAlert("Sucesso!", "Cadastro atualizado com sucesso!", "OK");  // Confirma a atualização

                        // Direciona para a tela de login
                        await Navigation.PushModalAsync(new Views.LgMinhaContaa());

                        EmailSenhaNotVisible();
                    }

                }
            }
            catch (Exception ex)
            {
                if (ex.Source != null)
                {
                    await DisplayAlert("Erro", ex.ToString(), "OK");

                    Console.WriteLine("Exception source: {0}", ex.Source);

                    // Caso não seja possível realizar o processo de cadastro total (de todas as classes),
                    // é excluído o que já foi cadastrado.

                    #region Exclui registros cadastrados caso não tenha sido possível inserir os dados em todas as classes envolvidas

                    if (verificaOperacao == 1)
                    {
                        Cliente clien = await control.GetCpf(cpf);

                        // Pega o ID do cliente gerado 
                        int idCliente = clien.IdCliente;

                        Motorista mot = await motoristaControl.GetMotoristaCliente(idCliente);

                        if (control.GetCpf(cpf) != null)
                        {
                            await control.DeleteCliente(idCliente);
                        }

                        // Caso tenha sido cadastrado em 'Motorista', será deletado.
                        if (mot != null)
                        {
                            await motoristaControl.DeleteMotorista(mot.IdMotorista);
                        }
                    }
                    #endregion
                }
                throw;
            }
        }
        #endregion

        #region LOGADO

        #region MeuCadastroAsync()

        private void MeuCadastroAsync()
        {
            // Captura o ID do cliente logado
            idCli = Session.Instance.IdCliente;

            cliente = Session.Instance.cliente;
                        
            #region Populando 
            
             etMotoristaNome.Text       = Session.Instance.Cnome;
             etMotoristaRg.Text         = Session.Instance.Crg;
             etMotoristaCpf.Text        = Session.Instance.Ccpf;
             //etMotoristaSexo.Text     
             etMotoristaDataNascto.Text = Session.Instance.CdataNascto;
             etMotoristaCelular.Text    = Session.Instance.Ccelular;
             etMotoristaCelular02.Text  = Session.Instance.Ccelular2;
                                       
             etMotoristaEndereco.Text   = Session.Instance.Cendereco;
             etMotoristaNumero.Text     = Session.Instance.Cnumero;
             etMotoristaCompl.Text      = Session.Instance.Ccomplemento;
             etMotoristaBairro.Text     = Session.Instance.Cbairro;
             etMotoristaCidade.Text     = Session.Instance.Ccidade;
             etMotoristaCep.Text        = Session.Instance.Ccep;
             etMotoristaUf.Text         = Session.Instance.Cuf;
             
             etMotoristaEmail.Text      = Session.Instance.Email;
             etMotoristaSenha.Text      = Session.Instance.Senha;
             
             int idStatus               = Session.Instance.IdStatus;


            #endregion
            
            // Menu superior
            stMeuCadastro.IsVisible = true;       

            // Mostra os campos
            DadosPessoaisVisible();

            // Bloqueia os campos para alteração
            DadosNotEnabled();

            slEditarVeiculos.IsVisible = true;

            // Esconde o botão 'Avançar'
            btnAvancar.IsVisible = false;

            // Mostra os botões 'Voltar' e 'Avançar'
            stBtnVoltar.IsVisible  = true;
            stBtnAvancar.IsVisible = true;
        }

        #endregion
        
        #region MeuCadastroCnhAsync()

        private void MeuCadastroCnhAsync()
        {
            // Captura o ID do cliente e motorista logado
            idCli  = Session.Instance.IdCliente;
            idMoto = Session.Instance.IdMotorista;

            stMeuCadastro.IsVisible = false;

            #region Populando 

            etMotoristaNumCnh.Text       = Session.Instance.MnumeroCnh;
            etMotoristaValidadeCnh.Text  = Session.Instance.MvalidadeCnh;
            etMotoristaCategoriaCnh.Text = Session.Instance.McategoriaCnh;
             
             int idStatus                = Session.Instance.IdStatus;

            #endregion
            
            // Menu superior
            stMeuCadastro.IsVisible = false;

            // Mostra os campos
            CnhVisible();

            // Bloqueia os campos para alteração
            CnhNotEnabled();

            slEditarVeiculos.IsVisible = true;

            // Esconde o botão 'Avançar'
            btnAvancar.IsVisible = false;

            // Mostra os botões 'Voltar' e 'Avançar'
            stBtnVoltar.IsVisible  = true;
        }

        #endregion
        

        #endregion
        
        #region Botões

        #region Botões - NÃO LOGADO

        #region Btn - Avançar
        private async void BtnAvancar_Clicked(object sender, EventArgs e)
        {
            if(Session.Instance.IdCliente != 0)
            {
                if (etMotoristaNumCnh.IsVisible) // UPDATE - CNH
                {
                    await VerificaCamposAsync(3);
                }
                else                             // UPDATE - DADOS PESSOAIS
                {
                    await VerificaCamposAsync(2);
                }                
            }
            else
            {
                await VerificaCamposAsync(1);
            }
        }
        #endregion

        #region Btn - Finalizar
        private async void BtnFinalizar_Clicked(object sender, EventArgs e)
        {
            await VerificaCamposAsync(1);
        }
        #endregion

        #region Btn - Voltar
        private void BtnVoltar_Clicked(object sender, EventArgs e)
        {
            if (etMotoristaEndereco.IsVisible)
            {
                EnderecoNotVisible();
                DadosPessoaisVisible();
                btnAvancar2.IsVisible   = false;
                btnVoltar.IsVisible     = false;
                btnAvancar.IsVisible    = true;
                btnDadosPessoais.IsEnabled = true;
            }
            else if (etMotoristaNumCnh.IsVisible)
            {
                CnhDadosBancariosNotVisible();
                EnderecoVisible();

            }
            else if (etMotoristaEmail.IsVisible)
            {
                EmailSenhaNotVisible();
                CnhDadosBancariosVisible();
                btnFinalizar.IsVisible = false;
                btnAvancar2.IsVisible  = true;
            }
        }
        #endregion
        
        #region Btn - Realizar login
        private async void BtnLogar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Views.LgHome());
        }
        #endregion

        #endregion

        #region Botões - LOGADO

        #region Btn - Voltar
        private async void BtnVoltar2_Clicked(object sender, EventArgs e)
        {
            btnSalvar.IsVisible = false;
            
            if (etMotoristaNome.IsVisible)
            {
                // Esconde os campos dos dados pessoais
                DadosPessoaisNotVisible();

                // mostra o botão 'Avançar'
                stBtnAvancar.IsVisible = true;

                // Mostra e esconde labels
                lblMeuCadastro.IsVisible = false;

                // Direciona para a tela 'Minha Conta'
                await Navigation.PushModalAsync(new Views.LgMinhaContaa());
            }
            else if (etMotoristaEndereco.IsVisible)
            {
                EnderecoNotVisible();
                DadosPessoaisVisible();

                stBtnAvancar.IsVisible = true;
            }
            else if (etMotoristaEmail.IsVisible)
            {
                EmailSenhaNotVisible();
                EnderecoVisible();
                stBtnAvancar.IsVisible = true;
            }
            else if (lblCadastroCnh.IsVisible)
            {
                // Esconde
                lblCadastroCnh.IsVisible   = false;
                CnhNotVisible();                     // Campos
                slEditarVeiculos.IsVisible = false;  // Btn Editar
                stBtnVoltar.IsVisible      = false;  // Btn Voltar

                btnMeuCadastro_Pessoais.IsVisible = true;
                btnMeuCadastro_CNH.IsVisible      = true;

                stMeuCadastro_2.IsVisible         = true;

                // Título - CNH
                lblCadastroCnh.IsVisible = false;
                
                // Mostra
                stMeuCadastro_2.IsVisible = true;
            }
        }
        #endregion

        #region Btn - Avançar - Buscar
        private void BtnAvancar2_Clicked(object sender, EventArgs e)
        {
            if (etMotoristaNome.IsVisible)
            {
                DadosPessoaisNotVisible();
                EnderecoVisible();
            }
            else if (etMotoristaEndereco.IsVisible)
            {
                EnderecoNotVisible();
                EmailSenhaVisible();

                btnAvancar2.IsVisible = false;
                stBtnAvancar.IsVisible = false;

                if (!slEditarVeiculos.IsVisible && etMotoristaEmail.IsVisible)
                {
                    btnSalvar.IsVisible = true;
                }
            }
        }
        #endregion

        #region Btn - Menu superior 2

        #region Dados Pessoais
        private async void BtnDadosPessoais2_Clicked(object sender, EventArgs e)
        {
            // Mostra os dados pessoais
            DadosPessoaisVisible();

            // Esconde os demais campos
            EnderecoNotVisible();
            EmailSenhaNotVisible();
        }
        #endregion

        #region Endereço
        private async void BtnEndereco2_Clicked(object sender, EventArgs e)
        {
            // Mostra os dados do endereço
            EnderecoVisible();

            // Esconde os demais campos
            DadosPessoaisNotVisible();
            EmailSenhaNotVisible();
        }
        #endregion

        #region Dados Pessoais
        private async void BtnEmailSenha2_Clicked(object sender, EventArgs e)
        {
            // Mostra os dados pessoas
            EmailSenhaVisible();

            // Esconde os demais campos
            DadosPessoaisNotVisible();
            EnderecoNotVisible();
        }
        #endregion

        #endregion

        #region Btn - CRUD

        #region Btn - Editar 
        private void BtnEditarCadastro_Clicked(object sender, EventArgs e)
        {
            //  Libera os campos para serem alterados
            DadosEnabled();

            #region Botões - Mostrar e esconder

            slEditarVeiculos.IsVisible  = false;        // EDITAR
            slExcluirVeiculos.IsVisible = false;        // EXCLUIR

            if (etMotoristaEmail.IsVisible)
            {
                btnSalvar.IsVisible = true;
            }
            else if(etMotoristaNumCnh.IsVisible)
            {
                CnhEnabled();
                btnSalvar.IsVisible = true;
            }
            
            //stBtnVoltar.IsVisible  = false;              // VOLTAR
           // stBtnAvancar.IsVisible = false;              // AVANÇAR

            verificaOperacao = 2;

            #endregion
        }
        #endregion

        #region Btn - Excluir cadastro
        private async void BtnExcluirCadastro_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Excluir", "Deseja mesmo excluir o seu cadastro?", "OK", "Cancelar"))
            {
                //await veiculoController.DeleteVeiculo(idVeiculo);

                await DisplayAlert("Excluído", "Cadastro excluído com sucesso!", "OK");

                await Navigation.PushModalAsync(new Views.Login());
            }
        }
        #endregion

        #endregion

        #region Btn - MeuCadastro - Dados Pessoais
        private async void BtnMeuCadastro_DadosP_Clicked(object sender, EventArgs e)
        {
            #region Esconde os botões 

            btnMeuCadastro_Pessoais.IsVisible = false;
            btnMeuCadastro_CNH.IsVisible      = false;
            stMeuCadastro_2.IsVisible         = false;

            #endregion

            #region Mostra os botões do menu superior

            btnDadosPessoais2.IsVisible = true;
            btnEndereco2.IsVisible      = true;
            btnEmailSenha2.IsVisible    = true;

            #endregion

            MeuCadastroAsync();
        }
        #endregion

        #region Btn - MeuCadastro - Cnh
        private async void BtnMeuCadastro_Cnh_Clicked(object sender, EventArgs e)
        {
            btnMeuCadastro_Pessoais.IsVisible = false;
            btnMeuCadastro_CNH.IsVisible      = false;

            stMeuCadastro_2.IsVisible         = false;

            // Título - CNH
            lblCadastroCnh.IsVisible = true;

            MeuCadastroCnhAsync(); // Mostra os campos e popula
        }
        #endregion
        
        #region Navegação entre as telas

        #region Btn - Home
        private async void BtnHome_Clicked(object sender, EventArgs e)
        {
            #region Usuário não logado

            if (Session.Instance.IdCliente == 0) 
            {
                if ((etMotoristaNome.Text != null && btnVoltar.IsVisible) || (etMotoristaNome.Text == null && !btnVoltar.IsVisible) &&
                    !etMotoristaNome.IsEnabled)
                {
                    await Navigation.PushModalAsync(new Views.PaginaInicial());
                }
                else
                if (await DisplayAlert("Deseja sair?", "Tem certeza que deseja sair? Todos os dados serão perdidos.", "OK", "Cancelar"))
                {
                    await Navigation.PushModalAsync(new Views.PaginaInicial());
                }
            }
            #endregion

            #region Usuário logado
            else
            {
                if ((etMotoristaNome.Text != null && etMotoristaNome.IsEnabled))
                {
                    await Navigation.PushModalAsync(new Views.LgHome());
                }
                else
                if (await DisplayAlert("Deseja sair?", "Tem certeza que deseja sair? Todos os dados serão perdidos.",
                        "OK", "Cancelar"))
                {
                    await Navigation.PushModalAsync(new Views.LgHome());
                }
            }
            #endregion

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

        #region Bloqueia e libera campo para alteração

        #region DadosEnabled()

        public void DadosEnabled()
        {
            #region Enabled = true;

            etMotoristaNome.IsEnabled        = true;
            etMotoristaRg.IsEnabled          = true;
            etMotoristaCpf.IsEnabled         = true;
            etMotoristaDataNascto.IsEnabled  = true;
            etMotoristaSexo.IsEnabled        = true;
            etMotoristaCelular.IsEnabled     = true;
            etMotoristaCelular02.IsEnabled   = true;
            etMotoristaEndereco.IsEnabled    = true;
            etMotoristaNumero.IsEnabled      = true;
            etMotoristaCompl.IsEnabled       = true;
            etMotoristaBairro.IsEnabled      = true;
            etMotoristaCidade.IsEnabled      = true;
            etMotoristaCep.IsEnabled         = true;
            etMotoristaUf.IsEnabled          = true;
            etMotoristaEmail.IsEnabled       = true;
            etMotoristaConfEmail.IsEnabled   = true;
            etMotoristaSenha.IsEnabled       = true;
            etMotoristaConfSenha.IsEnabled   = true;

            #endregion
        }

        #endregion

        #region DadosNotEnabled()

        public void DadosNotEnabled()
        {
            #region Enabled = false

            etMotoristaNome.IsEnabled        = false;
            etMotoristaRg.IsEnabled          = false;
            etMotoristaCpf.IsEnabled         = false;
            etMotoristaDataNascto.IsEnabled  = false;
            etMotoristaSexo.IsEnabled        = false;
            etMotoristaCelular.IsEnabled     = false;
            etMotoristaCelular02.IsEnabled   = false;
            etMotoristaEndereco.IsEnabled    = false;
            etMotoristaNumero.IsEnabled      = false;
            etMotoristaCompl.IsEnabled       = false;
            etMotoristaBairro.IsEnabled      = false;
            etMotoristaCidade.IsEnabled      = false;
            etMotoristaCep.IsEnabled         = false;
            etMotoristaUf.IsEnabled          = false;
            etMotoristaEmail.IsEnabled       = false;
            etMotoristaConfEmail.IsEnabled   = false;
            etMotoristaSenha.IsEnabled       = false;
            etMotoristaConfSenha.IsEnabled   = false;

            #endregion
        }

        #endregion

        #region CnhEnabled()
        public void CnhEnabled()
        {
            #region Enabled = true

            etMotoristaCategoriaCnh.IsEnabled = true;
            etMotoristaNumCnh.IsEnabled       = true;
            etMotoristaValidadeCnh.IsEnabled  = true;

            #endregion
        }
        #endregion

        #region CnhNotEnabled()
        public void CnhNotEnabled()
        {
            #region Enabled = false

            etMotoristaCategoriaCnh.IsEnabled = false;
            etMotoristaNumCnh.IsEnabled       = false;
            etMotoristaValidadeCnh.IsEnabled  = false;

            #endregion
        }
        #endregion

        #endregion

        #endregion

        #endregion

        #region Others
        private void BtnDadosPessoais_Clicked(object sender, EventArgs e)
        {

        }

        private void BtnEndereco_Clicked(object sender, EventArgs e)
        {

        }

        private void BtnCnhDadosBancarios_Clicked(object sender, EventArgs e)
        {

        }

        private void BtnEmailSenha_Clicked(object sender, EventArgs e)
        {

        }
        #endregion

        #region Mostra e esconde campos

        #region Dados Pessoais visiveis
        void DadosPessoaisVisible()
        {
            etMotoristaNome.IsVisible        = true;
            lblMotoristaNome.IsVisible       = true;
            etMotoristaRg.IsVisible          = true;
            lblMotoristaRg.IsVisible         = true;
            etMotoristaCpf.IsVisible         = true;
            lblMotoristaCpf.IsVisible        = true;
            etMotoristaSexo.IsVisible        = true;
            lblMotoristaSexo.IsVisible       = true;
            etMotoristaDataNascto.IsVisible  = true;
            lblMotoristaDataNascto.IsVisible = true;
            etMotoristaCelular.IsVisible     = true;
            lblMotoristaCelular.IsVisible    = true;
            etMotoristaCelular02.IsVisible   = true;
            lblMotoristaCelular02.IsVisible  = true;

            btnDadosPessoais.IsEnabled = false;
        }
        #endregion

        #region Dados pessoais invisiveis
        void DadosPessoaisNotVisible()
        {
            etMotoristaNome.IsVisible        = false;
            lblMotoristaNome.IsVisible       = false;
            etMotoristaRg.IsVisible          = false;
            lblMotoristaRg.IsVisible         = false;
            etMotoristaCpf.IsVisible         = false;
            lblMotoristaCpf.IsVisible        = false;
            etMotoristaSexo.IsVisible        = false;
            lblMotoristaSexo.IsVisible       = false;
            etMotoristaDataNascto.IsVisible  = false;
            lblMotoristaDataNascto.IsVisible = false;
            etMotoristaCelular.IsVisible     = false;
            lblMotoristaCelular.IsVisible    = false;
            etMotoristaCelular02.IsVisible   = false;
            lblMotoristaCelular02.IsVisible  = false;

            btnDadosPessoais.IsEnabled       = true;
        }
        #endregion

        #region Endereco visivel
        void EnderecoVisible()
        {
            etMotoristaEndereco.IsVisible  = true;
            lblMotoristaEndereco.IsVisible = true;
            etMotoristaNumero.IsVisible  = true;
            lblMotoristaNumero.IsVisible = true;
            etMotoristaCompl.IsVisible   = true;
            lblMotoristaCompl.IsVisible  = true;
            etMotoristaBairro.IsVisible  = true;
            lblMotoristaBairro.IsVisible = true;
            etMotoristaCidade.IsVisible  = true;
            lblMotoristaCidade.IsVisible = true;
            etMotoristaCep.IsVisible     = true;
            lblMotoristaCep.IsVisible    = true;
            etMotoristaUf.IsVisible      = true;
            lblMotoristaUf.IsVisible     = true;

            btnEndereco.IsEnabled        = true;
        }
        #endregion

        #region Endereco Visivel
        void EnderecoNotVisible()
        {
            etMotoristaEndereco.IsVisible  = false;
            lblMotoristaEndereco.IsVisible = false;
            etMotoristaNumero.IsVisible    = false;
            lblMotoristaNumero.IsVisible   = false;
            etMotoristaCompl.IsVisible     = false;
            lblMotoristaCompl.IsVisible    = false;
            etMotoristaBairro.IsVisible    = false;
            lblMotoristaBairro.IsVisible   = false;
            etMotoristaCidade.IsVisible    = false;
            lblMotoristaCidade.IsVisible   = false;
            etMotoristaCep.IsVisible       = false;
            lblMotoristaCep.IsVisible      = false;
            etMotoristaUf.IsVisible        = false;
            lblMotoristaUf.IsVisible       = false;
        }
        #endregion

        #region Cnh visivel
        void CnhVisible()
        {
            lblMotoristaNumCnh.IsVisible        = true;
            etMotoristaNumCnh.IsVisible         = true;
            lblMotoristaCategoriaCnh.IsVisible  = true;
            etMotoristaCategoriaCnh.IsVisible   = true;
            lblMotoristaValidadeCnh.IsVisible   = true;
            etMotoristaValidadeCnh.IsVisible    = true;
        }
        #endregion

        #region Cnh invisivel
        void CnhNotVisible()
        {
            lblMotoristaNumCnh.IsVisible       = false;
            etMotoristaNumCnh.IsVisible        = false;
            lblMotoristaCategoriaCnh.IsVisible = false;
            etMotoristaCategoriaCnh.IsVisible  = false;
            lblMotoristaValidadeCnh.IsVisible  = false;
            etMotoristaValidadeCnh.IsVisible   = false;
        }
        #endregion

        #region Cnh e dados bancários visiveis
        void CnhDadosBancariosVisible()
        {
            lblMotoristaNumCnh.IsVisible        = true;
            etMotoristaNumCnh.IsVisible         = true;
            lblMotoristaCategoriaCnh.IsVisible  = true;
            etMotoristaCategoriaCnh.IsVisible   = true;
            lblMotoristaValidadeCnh.IsVisible   = true;
            etMotoristaValidadeCnh.IsVisible    = true;
            /*lblDadosBancarios.IsVisible = true; */
            lblMotoristaBanco.IsVisible         = true;
            etMotoristaBanco.IsVisible          = true;
            lblMotoristaAgencia.IsVisible       = true;
            etMotoristaAgencia.IsVisible        = true;
            lblMotoristaDigitoAgencia.IsVisible = true;
            etMotoristaDigitoAgencia.IsVisible  = true;
            lblMotoristaConta.IsVisible         = true;
            etMotoristaConta.IsVisible          = true;
            lblMotoristaDigitoConta.IsVisible   = true;
            etMotoristaDigitoConta.IsVisible    = true;
            lblMotoristaTipoConta.IsVisible     = true;
            etMotoristaTipoConta.IsVisible      = true;

            btnCnhDadosBancarios.IsEnabled  = true;
        }
        #endregion

        #region Cnh e dados bancários invisiveis
        void CnhDadosBancariosNotVisible()
        {
            lblMotoristaNumCnh.IsVisible        = false;
            etMotoristaNumCnh.IsVisible         = false;
            lblMotoristaCategoriaCnh.IsVisible  = false;
            etMotoristaCategoriaCnh.IsVisible   = false;
            lblMotoristaValidadeCnh.IsVisible   = false;
            etMotoristaValidadeCnh.IsVisible    = false;
            /*lblDadosBancarios.IsVisible = false; */
            lblMotoristaBanco.IsVisible         = false;
            etMotoristaBanco.IsVisible          = false;
            lblMotoristaAgencia.IsVisible       = false;
            etMotoristaAgencia.IsVisible        = false;
            lblMotoristaDigitoAgencia.IsVisible = false;
            etMotoristaDigitoAgencia.IsVisible  = false;
            lblMotoristaConta.IsVisible         = false;
            etMotoristaConta.IsVisible          = false;
            lblMotoristaDigitoConta.IsVisible   = false;
            etMotoristaDigitoConta.IsVisible    = false;
            lblMotoristaTipoConta.IsVisible     = false;
            etMotoristaTipoConta.IsVisible      = false;
        }
        #endregion

        #region Email e senha visiveis
        void EmailSenhaVisible()
        {
            lblMotoristaEmail.IsVisible     = true;
            etMotoristaEmail.IsVisible      = true;
            lblMotoristaConfEmail.IsVisible = true;
            etMotoristaConfEmail.IsVisible  = true;
            lblMotoristaSenha.IsVisible     = true;
            etMotoristaSenha.IsVisible      = true;
            lblMotoristaConfSenha.IsVisible = true;
            etMotoristaConfSenha.IsVisible  = true;

            btnEmailSenha.IsEnabled         = true;
        }
        #endregion

        #region Email e senha invisiveis
        void EmailSenhaNotVisible()
        {
            lblMotoristaEmail.IsVisible     = false;
            etMotoristaEmail.IsVisible      = false;
            lblMotoristaConfEmail.IsVisible = false;
            etMotoristaConfEmail.IsVisible  = false;
            lblMotoristaSenha.IsVisible     = false;
            etMotoristaSenha.IsVisible      = false;
            lblMotoristaConfSenha.IsVisible = false;
            etMotoristaConfSenha.IsVisible  = false;
        }
        #endregion

        #endregion

    }
}