namespace Fingerprint_Matcher
{
    partial class Biometria
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Biometria));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.esquerda1 = new System.Windows.Forms.PictureBox();
            this.esquerda2 = new System.Windows.Forms.PictureBox();
            this.direita2 = new System.Windows.Forms.PictureBox();
            this.direita1 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.alunos = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.numRegistro = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contador = new System.Windows.Forms.TextBox();
            this.maoDireita = new System.Windows.Forms.GroupBox();
            this.maoEsquerda = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.configuraçõesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetarBiometriasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mostra = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.esquerda1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.esquerda2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.direita2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.direita1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.maoDireita.SuspendLayout();
            this.maoEsquerda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(9, 115);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(84, 99);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // esquerda1
            // 
            this.esquerda1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.esquerda1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.esquerda1.Image = ((System.Drawing.Image)(resources.GetObject("esquerda1.Image")));
            this.esquerda1.Location = new System.Drawing.Point(99, 38);
            this.esquerda1.Name = "esquerda1";
            this.esquerda1.Size = new System.Drawing.Size(132, 176);
            this.esquerda1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.esquerda1.TabIndex = 1;
            this.esquerda1.TabStop = false;
            this.esquerda1.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // esquerda2
            // 
            this.esquerda2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.esquerda2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.esquerda2.Image = ((System.Drawing.Image)(resources.GetObject("esquerda2.Image")));
            this.esquerda2.Location = new System.Drawing.Point(237, 38);
            this.esquerda2.Name = "esquerda2";
            this.esquerda2.Size = new System.Drawing.Size(137, 176);
            this.esquerda2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.esquerda2.TabIndex = 2;
            this.esquerda2.TabStop = false;
            this.esquerda2.Click += new System.EventHandler(this.esquerda2_Click);
            // 
            // direita2
            // 
            this.direita2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.direita2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.direita2.Image = ((System.Drawing.Image)(resources.GetObject("direita2.Image")));
            this.direita2.Location = new System.Drawing.Point(155, 64);
            this.direita2.Name = "direita2";
            this.direita2.Size = new System.Drawing.Size(140, 159);
            this.direita2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.direita2.TabIndex = 4;
            this.direita2.TabStop = false;
            this.direita2.Click += new System.EventHandler(this.direita2_Click);
            // 
            // direita1
            // 
            this.direita1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.direita1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.direita1.Image = ((System.Drawing.Image)(resources.GetObject("direita1.Image")));
            this.direita1.Location = new System.Drawing.Point(10, 64);
            this.direita1.Name = "direita1";
            this.direita1.Size = new System.Drawing.Size(137, 159);
            this.direita1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.direita1.TabIndex = 3;
            this.direita1.TabStop = false;
            this.direita1.Click += new System.EventHandler(this.direita1_Click);
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox6.Image")));
            this.pictureBox6.Location = new System.Drawing.Point(297, 129);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(78, 94);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox6.TabIndex = 5;
            this.pictureBox6.TabStop = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Tomato;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(489, 769);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(169, 57);
            this.button1.TabIndex = 7;
            this.button1.Text = "SALVA 02";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // alunos
            // 
            this.alunos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.alunos.ForeColor = System.Drawing.Color.Green;
            this.alunos.FormattingEnabled = true;
            this.alunos.Location = new System.Drawing.Point(17, 248);
            this.alunos.Name = "alunos";
            this.alunos.Size = new System.Drawing.Size(387, 28);
            this.alunos.TabIndex = 8;
            this.alunos.SelectedIndexChanged += new System.EventHandler(this.alunos_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Tomato;
            this.label3.Location = new System.Drawing.Point(119, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 29);
            this.label3.TabIndex = 10;
            this.label3.Text = "1ª foto";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Tomato;
            this.label4.Location = new System.Drawing.Point(262, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 29);
            this.label4.TabIndex = 11;
            this.label4.Text = "2ª foto";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Tomato;
            this.label5.Location = new System.Drawing.Point(181, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 29);
            this.label5.TabIndex = 13;
            this.label5.Text = "2ª foto";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Tomato;
            this.label6.Location = new System.Drawing.Point(36, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 29);
            this.label6.TabIndex = 12;
            this.label6.Text = "1ª foto";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Tomato;
            this.label8.Location = new System.Drawing.Point(12, 638);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(191, 29);
            this.label8.TabIndex = 16;
            this.label8.Text = "Nº de Registro:";
            // 
            // numRegistro
            // 
            this.numRegistro.AutoSize = true;
            this.numRegistro.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numRegistro.ForeColor = System.Drawing.Color.ForestGreen;
            this.numRegistro.Location = new System.Drawing.Point(205, 628);
            this.numRegistro.Name = "numRegistro";
            this.numRegistro.Size = new System.Drawing.Size(57, 39);
            this.numRegistro.TabIndex = 17;
            this.numRegistro.Text = "00";
            this.numRegistro.Click += new System.EventHandler(this.label9_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // contador
            // 
            this.contador.Location = new System.Drawing.Point(563, 33);
            this.contador.Name = "contador";
            this.contador.Size = new System.Drawing.Size(100, 20);
            this.contador.TabIndex = 18;
            this.contador.Visible = false;
            // 
            // maoDireita
            // 
            this.maoDireita.Controls.Add(this.label5);
            this.maoDireita.Controls.Add(this.label6);
            this.maoDireita.Controls.Add(this.pictureBox6);
            this.maoDireita.Controls.Add(this.direita2);
            this.maoDireita.Controls.Add(this.direita1);
            this.maoDireita.ForeColor = System.Drawing.Color.Green;
            this.maoDireita.Location = new System.Drawing.Point(272, 509);
            this.maoDireita.Name = "maoDireita";
            this.maoDireita.Size = new System.Drawing.Size(386, 234);
            this.maoDireita.TabIndex = 19;
            this.maoDireita.TabStop = false;
            this.maoDireita.Text = "Mão DIreita";
            this.maoDireita.Visible = false;
            // 
            // maoEsquerda
            // 
            this.maoEsquerda.Controls.Add(this.label4);
            this.maoEsquerda.Controls.Add(this.label3);
            this.maoEsquerda.Controls.Add(this.esquerda2);
            this.maoEsquerda.Controls.Add(this.esquerda1);
            this.maoEsquerda.Controls.Add(this.pictureBox1);
            this.maoEsquerda.ForeColor = System.Drawing.Color.Green;
            this.maoEsquerda.Location = new System.Drawing.Point(19, 282);
            this.maoEsquerda.Name = "maoEsquerda";
            this.maoEsquerda.Size = new System.Drawing.Size(385, 221);
            this.maoEsquerda.TabIndex = 20;
            this.maoEsquerda.TabStop = false;
            this.maoEsquerda.Text = "Mão Esquerda";
            this.maoEsquerda.Visible = false;
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.ForeColor = System.Drawing.Color.Green;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "MÃO ESQUERDA",
            "MÃO DIREITA"});
            this.comboBox1.Location = new System.Drawing.Point(430, 247);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(180, 28);
            this.comboBox1.TabIndex = 21;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Tomato;
            this.label9.Location = new System.Drawing.Point(430, 222);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(132, 20);
            this.label9.TabIndex = 22;
            this.label9.Text = "Escolha a mão:";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(5, 33);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(658, 70);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 24;
            this.pictureBox2.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Tomato;
            this.label11.Location = new System.Drawing.Point(15, 222);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(60, 20);
            this.label11.TabIndex = 28;
            this.label11.Text = "Aluno:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Tomato;
            this.label1.Location = new System.Drawing.Point(187, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(291, 24);
            this.label1.TabIndex = 25;
            this.label1.Text = "Vamos registrar as biometrias!";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Tomato;
            this.label2.Location = new System.Drawing.Point(186, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(361, 16);
            this.label2.TabIndex = 26;
            this.label2.Text = "Selecione um aluno, a mão que pretende cadastrar";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Tomato;
            this.label10.Location = new System.Drawing.Point(188, 165);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(274, 16);
            this.label10.TabIndex = 27;
            this.label10.Text = "e guarde duas fotos do mesmo dedo...";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configuraçõesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(670, 24);
            this.menuStrip1.TabIndex = 29;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // configuraçõesToolStripMenuItem
            // 
            this.configuraçõesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetarBiometriasToolStripMenuItem});
            this.configuraçõesToolStripMenuItem.Name = "configuraçõesToolStripMenuItem";
            this.configuraçõesToolStripMenuItem.Size = new System.Drawing.Size(96, 20);
            this.configuraçõesToolStripMenuItem.Text = "Configurações";
            // 
            // resetarBiometriasToolStripMenuItem
            // 
            this.resetarBiometriasToolStripMenuItem.Name = "resetarBiometriasToolStripMenuItem";
            this.resetarBiometriasToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.resetarBiometriasToolStripMenuItem.Text = "Resetar Biometrias";
            this.resetarBiometriasToolStripMenuItem.Click += new System.EventHandler(this.resetarBiometriasToolStripMenuItem_Click);
            // 
            // mostra
            // 
            this.mostra.AutoSize = true;
            this.mostra.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mostra.ForeColor = System.Drawing.Color.Tomato;
            this.mostra.Location = new System.Drawing.Point(80, 810);
            this.mostra.Name = "mostra";
            this.mostra.Size = new System.Drawing.Size(186, 16);
            this.mostra.TabIndex = 30;
            this.mostra.Text = "Cadastrando biometrias...";
            this.mostra.Visible = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Tomato;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(309, 769);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(169, 57);
            this.button2.TabIndex = 31;
            this.button2.Text = "SALVA 01";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Biometria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(670, 835);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.mostra);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.maoEsquerda);
            this.Controls.Add(this.maoDireita);
            this.Controls.Add(this.contador);
            this.Controls.Add(this.numRegistro);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.alunos);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Biometria";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Biometria";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.esquerda1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.esquerda2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.direita2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.direita1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.maoDireita.ResumeLayout(false);
            this.maoDireita.PerformLayout();
            this.maoEsquerda.ResumeLayout(false);
            this.maoEsquerda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox esquerda1;
        private System.Windows.Forms.PictureBox esquerda2;
        private System.Windows.Forms.PictureBox direita2;
        private System.Windows.Forms.PictureBox direita1;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox alunos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label numRegistro;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox contador;
        private System.Windows.Forms.GroupBox maoDireita;
        private System.Windows.Forms.GroupBox maoEsquerda;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem configuraçõesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetarBiometriasToolStripMenuItem;
        private System.Windows.Forms.Label mostra;
        private System.Windows.Forms.Button button2;
    }
}