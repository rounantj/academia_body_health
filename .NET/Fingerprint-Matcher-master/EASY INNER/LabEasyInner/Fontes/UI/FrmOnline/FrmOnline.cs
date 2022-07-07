using System;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

using EasyInnerSDK.Entity;
using System.Threading;
using System.Runtime.InteropServices;
using System.Net.Sockets;
using System.IO;
using System.Net;

namespace EasyInnerSDK.UI
{
    public partial class FrmOnline : Form
    {
        static bool aberto = false;

        #region Propriedades

        private int ListIndex = -1;

        TcpListener escutando;
        private Socket conexao;

        private Thread tipoThread;

        public bool clientConnected = false;
        public NetworkStream clientStream;

        private const int TRANSMIT_HEIGHT = 240;
        private const int TRANSMIT_IMAGE_SIZE = 320 * 240 * 4;
        private const int TRANSMIT_WIDTH = 320;

        bool backgroundSent = false;
      //  WriteableBitmap clientImage;
        int imageSendCounter = 1;
        private int portSend = 9000;
        Socket s;


        private System.ComponentModel.Container components1 = null;

        private NetworkStream socketStream;

        private BinaryWriter escreve;

        private BinaryReader le;
        public bool Ativa {get;set;}

        public bool FechouMaquina { get; set; }

        public FrmOnlineController ControlOnline { get; set; }

        #endregion

        private static Form FPai;
        public FrmOnline(Form pai)
        {
           

            if (!aberto)
            {
                InitializeComponent();
                optDireita.Checked = true;
                int index = cboEquipamento.FindString("Catraca Entrada/Saída");
                cboEquipamento.SelectedIndex = index;
                ckbBIO.Checked = true;


                FPai = pai;
                MdiParent = pai;
                aberto = true;
                ControlOnline = new FrmOnlineController(this);
               
                //this.cboPadraoCartao.Items.Add("Topdata");
                //this.cboPadraoCartao.Items.Add("Livre");
                //this.cboPadraoCartao.SelectedIndex = 1;

                this.cboTipoConexaoOnline.Items.Add("Serial");
                this.cboTipoConexaoOnline.Items.Add("TCP/IP porta variável");
                this.cboTipoConexaoOnline.Items.Add("TCP/IP porta fixa");
                this.cboTipoConexaoOnline.SelectedIndex = 2;

                this.cboTipoLeitor.Items.Clear();
                this.cboTipoLeitor.Items.Add("Código de Barras");
                this.cboTipoLeitor.Items.Add("Magnético");
                this.cboTipoLeitor.Items.Add("Proximidade Abatrack/Smart Card");
                this.cboTipoLeitor.Items.Add("Proximidade Wiegand/Smart Card");
                this.cboTipoLeitor.Items.Add("Proximidade Smart Card Serial");
                this.cboTipoLeitor.Items.Add("Código de barras serial");
                this.cboTipoLeitor.Items.Add("Prox. Wiegand FC Sem Separador");
                this.cboTipoLeitor.Items.Add("Prox. Wiegand FC Com Separador");
                this.cboTipoLeitor.Items.Add("Barras, Prox, QR Code c/ letras");
                this.cboTipoLeitor.SelectedIndex = 0;

                this.cboEquipamento.Items.Clear();

                this.cboEquipamento.Items.Add("Coletor");
                this.cboEquipamento.Items.Add("Catraca Entrada/Saída");
                this.cboEquipamento.Items.Add("Catraca Entrada");
                this.cboEquipamento.Items.Add("Catraca Saída");
                this.cboEquipamento.Items.Add("Catraca Saída Liberada");
                this.cboEquipamento.Items.Add("Catraca Entrada Liberada");
                this.cboEquipamento.Items.Add("Catraca Liberada 2 Sentidos");
                this.cboEquipamento.Items.Add("Catraca Liberada 2 Sentidos(Sentido Giro)");
                this.cboEquipamento.Items.Add("Catraca com Urna");
                this.cboEquipamento.SelectedIndex = 1;

                Show();

               // START();
            }
            else
            {
                Dispose();
            }
        }

        public   void SERVIDOR() {


            try
            {
               
                    IPAddress ipAddress = IPAddress.Any;
                    TcpListener listener = new TcpListener(ipAddress, portSend);
                    listener.Start();
                    Console.WriteLine("Server is running");
                    Console.WriteLine("Listening on port " + portSend);
                    Console.WriteLine("Waiting for connections...");
                    while (!clientConnected)
                    {
                        s = listener.AcceptSocket();
                        s.SendBufferSize = 256000;
                        Console.WriteLine("Connection accepted from " + s.RemoteEndPoint);
                        byte[] b = new byte[65535];
                        int k = s.Receive(b);
                        ASCIIEncoding enc = new ASCIIEncoding();
                        Console.WriteLine("Received:" + enc.GetString(b, 0, k) + "..");
                        //Ensure the client is who we want
                        if (enc.GetString(b, 0, k) == "libera" || enc.GetString(b, 0, k) == "LIBERAR")
                        {
                           // clientConnected = true;
                            if (this.lstInnersCadastrados.Items.Count == 1)
                            {
                                this.lstInnersCadastrados.SetSelected(0, true);
                            }

                            if ((Inner)lstInnersCadastrados.SelectedItem != null)
                            {
                                Inner InnerAtual = ((Inner)lstInnersCadastrados.SelectedItem);

                                if (InnerAtual.Catraca)
                                {
                                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_LIBERAR_CATRACA;
                                    LiberarCatracaDoisSentidos(1);
                                    Console.Write("Catraca liberada!");
                                   

                                }
                                else
                                {
                                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_ACIONAR_RELE1;

                                }
                            }
                            else
                            {
                                MessageBox.Show("Selecione um Inner para liberar!", "Liberar Acesso");
                            }
                           // escreve.Write("Catraca liberada!");

                            Console.WriteLine(enc.GetString(b, 0, k));
                           
                        }
                    }
                
            }
            catch (Exception er)
            {
                Console.WriteLine(er);
            }
            
        

    }


        private bool LiberaCatraca() 
        {
            if (lstInnersCadastrados.SelectedIndex == -1)
                lstInnersCadastrados.SelectedIndex = 0;

            foreach (Inner inner in ControlOnline.ListInners.Values)
            {
                if (inner.Numero == ((Inner)lstInnersCadastrados.SelectedItem).Numero)
                {
                    inner.EstadoAtual = Enumeradores.EstadosInner.ESTADO_LIBERAR_CATRACA;
                }
            }
            return true;
        }

        public void ExibirMensagembox(string Mensagem, string Titulo)
        {
            MessageBox.Show(Mensagem, Titulo);
        }

        #region Eventos

        #region btnAdicionarUsuarioInnerOnline_Click
        public void START()
        {
            //this.cboTipoLeitor.SelectedIndex = 1;
            ControlOnline.AdicionarInner();
            btnIniciarMaquina.Enabled = true;
            btnPararMaquina.Enabled = true;
            btnIniciarMaquina.Enabled = false;
            Application.DoEvents();
            ControlOnline.IniciarMaquina();
            























            /*
            ControlOnline.acionaDoisLadosDaCatraca();


            try
            {
                Console.WriteLine("LIBERACAO CATRACA -> "+LiberarCatracaDoisSentidos(1).ToString());

            }
            catch (Exception err)
            {
                MessageBox.Show("erro \n" + err);
            }
            */
        }
        private void btnAdicionarUsuarioInnerOnline_Click(object sender, EventArgs e)
        {

            ControlOnline.AdicionarInner();
            btnIniciarMaquina.Enabled = true;
            btnPararMaquina.Enabled = true;
            btnIniciarMaquina.Enabled = false;
            Application.DoEvents();
            ControlOnline.IniciarMaquina();
           ControlOnline.acionaDoisLadosDaCatraca();

            try
            {
              Console.WriteLine(LiberarCatracaDoisSentidos(1).ToString());
            }
            catch (Exception err)
            {
                MessageBox.Show("erro \n" + err);
            }

            try
            {
                Console.WriteLine("Aqi -> " + LiberarCatracaDoisSentidos(1).ToString());
            }
            catch (Exception err)
            {
                Console.WriteLine("erro \n" + err);
            }



        }
        #endregion

        #region btnIniciarMaquina_Click
       
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ConfigurarAcionamento1(byte Funcao, byte Tempo);

        public bool acionaCatraca()
        {
            try
            {
                ConfigurarAcionamento1(8, 5);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void btnIniciarMaquina_Click(object sender, EventArgs e)
        {
            btnPararMaquina.Enabled = true;
            btnIniciarMaquina.Enabled = false;
            Application.DoEvents();
            ControlOnline.IniciarMaquina();
        }
        #endregion

        #region btnPararMaquina_Click
        private void btnPararMaquina_Click(object sender, EventArgs e)
        {
            ControlOnline.PararMaquina();
            lstVersaoInners.Items.Clear();
            btnIniciarMaquina.Enabled = true;

            cmdEntrada.Enabled = false;
            btnPararMaquina.Enabled = false;
            cmdSair.Enabled = false;
        }
        #endregion

        #region MainBIO_FormClosing
        private void MainBIO_FormClosing(object sender, FormClosingEventArgs e)
        {
            
           // FrmOnlineController nob = new FrmOnlineController(this);
           // MessageBox.Show("GLOBAL -> " + nob.GLOBAL);
  

        }
        #endregion

        #region btnRemoverInnerLista_Click
        private void btnRemoverInnerLista_Click(object sender, EventArgs e)
        {
            if (this.lstInnersCadastrados.Items.Count != 0)
            {
                if (this.lstInnersCadastrados.Items.Count == 1)
                {
                    this.lstInnersCadastrados.SetSelected(0, true);
                }

                if ((Inner)lstInnersCadastrados.SelectedItem != null)
                {
                    Inner InnerAtual = ((Inner)lstInnersCadastrados.SelectedItem);
                    ControlOnline.UpdateDisplay.RemoverInner(InnerAtual.Numero);
                    ControlOnline.RemoverInnerLista(InnerAtual);
                    if (lstInnersCadastrados.Items.Count == 0)
                    {
                        btnIniciarMaquina.Enabled = false;
                    }
                }
                else
                {
                    MessageBox.Show("Selecione um Inner para remover!", "Remover da Lista");
                }
            }
        }
        #endregion

        #region FrmOnline_Load
        private void FrmOnline_Load(object sender, EventArgs e)
        {
            this.cboPadraoCartao.Items.Add("Topdata");
            this.cboPadraoCartao.Items.Add("Livre");
            this.cboPadraoCartao.SelectedIndex = 1;

            this.cboTipoConexaoOnline.Items.Add("Serial");
            this.cboTipoConexaoOnline.Items.Add("TCP/IP porta variável");
            this.cboTipoConexaoOnline.Items.Add("TCP/IP porta fixa");
            this.cboTipoConexaoOnline.SelectedIndex = 2;

            this.cboTipoLeitor.Items.Clear();
            this.cboTipoLeitor.Items.Add("Código de Barras");
            this.cboTipoLeitor.Items.Add("Magnético");
            this.cboTipoLeitor.Items.Add("Proximidade Abatrack/Smart Card");
            this.cboTipoLeitor.Items.Add("Proximidade Wiegand/Smart Card");
            this.cboTipoLeitor.Items.Add("Proximidade Smart Card Serial");
            this.cboTipoLeitor.Items.Add("Código de barras serial");
            this.cboTipoLeitor.Items.Add("Prox. Wiegand FC Sem Separador");
            this.cboTipoLeitor.Items.Add("Prox. Wiegand FC Com Separador");
            this.cboTipoLeitor.Items.Add("Barras, Prox, QR Code c/ letras");
            this.cboTipoLeitor.SelectedIndex = 0;

            this.cboEquipamento.Items.Clear();

            this.cboEquipamento.Items.Add("Coletor");
            this.cboEquipamento.Items.Add("Catraca Entrada/Saída");
            this.cboEquipamento.Items.Add("Catraca Entrada");
            this.cboEquipamento.Items.Add("Catraca Saída");
            this.cboEquipamento.Items.Add("Catraca Saída Liberada");
            this.cboEquipamento.Items.Add("Catraca Entrada Liberada");
            this.cboEquipamento.Items.Add("Catraca Liberada 2 Sentidos");
            this.cboEquipamento.Items.Add("Catraca Liberada 2 Sentidos(Sentido Giro)");
            this.cboEquipamento.Items.Add("Catraca com Urna");
            this.cboEquipamento.SelectedIndex = 1;

            
            btnPararMaquina.Enabled = false;
          START();
        }
        #endregion

        #region cboTipoConexaoOnline_SelectedIndexChanged
        private void cboTipoConexaoOnline_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTipoConexaoOnline.SelectedIndex == 0)
            {
                txtPortaOnline.Value = 1;
            }
            else
            {
                txtPortaOnline.Value = 3570;
            }
        }
        #endregion

        #region ckbBIO_Click
        private void ckbBIO_Click(object sender, EventArgs e)
        {
            if (ckbBIO.Checked)
            {
                chkVerificacao.Enabled = true;
                chkIdentificacao.Enabled = true;
                chkListaBio.Enabled = true;
                chkModuloLC.Enabled = true;
            }
            else
            {
                chkVerificacao.Enabled = false;
                chkIdentificacao.Enabled = false;
                chkListaBio.Enabled = false;
                chkVerificacao.Checked = false;
                chkIdentificacao.Checked = false;
                chkListaBio.Checked = false;
                chkModuloLC.Enabled = false;
                chkModuloLC.Checked = false;
            }
        }
        #endregion

        #region cboPadraoCartao_SelectedIndexChanged
        private void cboPadraoCartao_SelectedIndexChanged(object sender, EventArgs e)
        {
            ckbDoisLeitores.Enabled = (!(cboTipoLeitor.SelectedIndex == (int)Enumeradores.TipoLeitor.CODIGO_DE_BARRAS) && !(cboTipoLeitor.SelectedIndex == (int)Enumeradores.TipoLeitor.MAGNETICO));
            ckbDoisLeitores.Checked = false;
            if (cboTipoLeitor.SelectedIndex == 2)
            {
                udQtdDigitosCartao.Value = 14;
            }
            else if (cboTipoLeitor.SelectedIndex == 3)
            {
                udQtdDigitosCartao.Value = 6;
            }
            else if (cboTipoLeitor.SelectedIndex == 6)
            {
                udQtdDigitosCartao.Value = 8;
            }
            else if (cboTipoLeitor.SelectedIndex == 7)
            {
                udQtdDigitosCartao.Value = 10;
            }
          
        }
        #endregion

        #region cmdLimpar_Click
        private void cmdLimpar_Click(object sender, EventArgs e)
        {
            lstBilhetes.Items.Clear();
        }
        #endregion

        #region cmdEntrada_Click
        private void cmdEntrada_Click(object sender, EventArgs e)
        {
            if (this.lstInnersCadastrados.Items.Count == 1)
            {
                this.lstInnersCadastrados.SetSelected(0, true);
            }

            if ((Inner)lstInnersCadastrados.SelectedItem != null)
            {
                Inner InnerAtual = ((Inner)lstInnersCadastrados.SelectedItem);

                if (InnerAtual.Catraca)
                {
                    
                    ControlOnline.HABILITA_LADO_CATRACA("Entrada", InnerAtual.CatInvertida);
                    ControlOnline.HABILITA_LADO_CATRACA("Entrada", InnerAtual.CatInvertida);
                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_LIBERAR_CATRACA;
                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_LIBERAR_CATRACA;
                }
                else
                {
                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_ACIONAR_RELE1;

                }
            }
            else
            {
                MessageBox.Show("Selecione um Inner para liberar!", "Liberar Acesso");
            }
        }
        #endregion

        #region lstInnersCadastrados_Click
        private void lstInnersCadastrados_Click(object sender, EventArgs e)
        {
            ListIndex = lstInnersCadastrados.SelectedIndex;
        }
        #endregion

        #region cmdSair_Click
        private void cmdSair_Click(object sender, EventArgs e)
        {
            if (this.lstInnersCadastrados.Items.Count == 1)
            {
                this.lstInnersCadastrados.SetSelected(0, true);
            }

            if ((Inner)lstInnersCadastrados.SelectedItem != null)
            {
                Inner InnerAtual = ((Inner)lstInnersCadastrados.SelectedItem);
                

                if (InnerAtual.Catraca)
                {
                    ControlOnline.HABILITA_LADO_CATRACA("Saida", InnerAtual.CatInvertida);
                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_LIBERAR_CATRACA;
                }
                else
                {
                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_ACIONAR_RELE2;
                }
            }
            else
            {
                MessageBox.Show("Selecione um Inner para liberar!", "Liberar Acesso");
            }   
        }
        #endregion

        private void ckbListaBio_Click(object sender, EventArgs e)
        {
            if (chkListaBio.Checked)
                chkVerificacao.Checked = true;
        }

        #endregion

        private void ckbVerificacao_Click(object sender, EventArgs e)
        {
            if (!chkVerificacao.Checked)
               chkListaBio.Checked = false;
        }

        private void FrmOnline_FormClosed(object sender, FormClosedEventArgs e)
        {
            aberto = false;
        }

        private void optEsquerda_CheckedChanged(object sender, EventArgs e)
        {
            imgCatraca.Image = global::EasyInnerSDK.Properties.Resources.Esquerda_invertidaa;
        }

        private void optDireita_CheckedChanged(object sender, EventArgs e)
        {
            imgCatraca.Image = global::EasyInnerSDK.Properties.Resources.Direita_normall;
        }

        private void cboEquipamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((cboEquipamento.SelectedIndex != (byte)Enumeradores.Acionamento.Acionamento_Coletor))
            {
                optEsquerda.Enabled = true;
                optDireita.Enabled = true;
                ckbDoisLeitores.Enabled = true;
                    
                if ((cboEquipamento.SelectedIndex == (byte)Enumeradores.Acionamento.Acionamento_Catraca_Urna))
                {
                    optDireita.Checked=true;
                    optEsquerda.Enabled = false;
                    optDireita.Enabled = false;
                    imgCatraca.Image = global::EasyInnerSDK.Properties.Resources.Direita_normall;
                    gbLadoCatraca.Enabled = true;
                    cboTipoLeitor.SelectedIndex = 4;//proximidade
                    ckbDoisLeitores.Checked = true;
               
                }
                else
                {
                    if (optDireita.Checked)
                    {
                        imgCatraca.Image = global::EasyInnerSDK.Properties.Resources.Direita_normall;
                    }
                    else
                    {
                        if (optEsquerda.Checked)
                        {
                            imgCatraca.Image = global::EasyInnerSDK.Properties.Resources.Esquerda_invertidaa;
                        }
                    }

                    gbLadoCatraca.Enabled = true;
                    
                }

            }
            else
            {
                    optEsquerda.Enabled = false;
                    optDireita.Enabled = false;
                    gbLadoCatraca.Enabled = false;
                    imgCatraca.Image = global::EasyInnerSDK.Properties.Resources.nenhum;
            
            }
        }

        private void chkCartaoMaster_CheckedChanged(object sender, EventArgs e)
        {
            txtCartaoMaster.Enabled = chkCartaoMaster.Checked;
        }

        private void cboPadraoCartaoOnline_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPadraoCartao.SelectedIndex == 0)
            {
                MessageBox.Show("Este tipo é para uso exclusivo de cartões fabricado pela Topdata !");
            }
        }
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte LiberarCatracaDoisSentidos(int Inner);

        private void button1_Click(object sender, EventArgs e)
        {
            SERVIDOR();
            /*
            if (this.lstInnersCadastrados.Items.Count == 1)
            {
                this.lstInnersCadastrados.SetSelected(0, true);
            }

            if ((Inner)lstInnersCadastrados.SelectedItem != null)
            {
                Inner InnerAtual = ((Inner)lstInnersCadastrados.SelectedItem);

                if (InnerAtual.Catraca)
                {

                    //  ControlOnline.HABILITA_LADO_CATRACA("Ambas", InnerAtual);


                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_LIBERAR_CATRACA;
                    LiberarCatracaDoisSentidos(1);
                  //  EasyInner.EnviarMensagemPadraoOnLine(InnerAtual.Numero, 0, "                 LIBERADA NOS DOIS SENTIDOS");
                }
                else
                {
                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_ACIONAR_RELE1;

                }
            }
            else
            {
                MessageBox.Show("Selecione um Inner para liberar!", "Liberar Acesso");
            }
            */
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StartOK novo = new StartOK();
            novo.ShowDialog();
        }
    }
}