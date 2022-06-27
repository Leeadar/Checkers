namespace CheckersGui
{
    public partial class FormGameSettings
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
            this.lableBoardSize = new System.Windows.Forms.Label();
            this.radioButtonSize6x6 = new System.Windows.Forms.RadioButton();
            this.radioButtonSize8x8 = new System.Windows.Forms.RadioButton();
            this.radioButtonSize10x10 = new System.Windows.Forms.RadioButton();
            this.lablePlayers = new System.Windows.Forms.Label();
            this.lablePlayer1 = new System.Windows.Forms.Label();
            this.checkBoxPlayer2 = new System.Windows.Forms.CheckBox();
            this.textBoxFirstPlayer = new System.Windows.Forms.TextBox();
            this.textBoxSecondPlayer = new System.Windows.Forms.TextBox();
            this.buttonDone = new System.Windows.Forms.Button();
            this.errorProviderPlayerName = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderPlayerName)).BeginInit();
            this.SuspendLayout();
            // 
            // lableBoardSize
            // 
            this.lableBoardSize.AutoSize = true;
            this.lableBoardSize.BackColor = System.Drawing.Color.Transparent;
            this.lableBoardSize.Font = new System.Drawing.Font("Microsoft JhengHei UI Light", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lableBoardSize.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lableBoardSize.Location = new System.Drawing.Point(230, 42);
            this.lableBoardSize.Name = "lableBoardSize";
            this.lableBoardSize.Size = new System.Drawing.Size(218, 52);
            this.lableBoardSize.TabIndex = 0;
            this.lableBoardSize.Text = "Board Size";
            // 
            // radioButtonSize6x6
            // 
            this.radioButtonSize6x6.AutoSize = true;
            this.radioButtonSize6x6.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonSize6x6.Font = new System.Drawing.Font("Microsoft JhengHei Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonSize6x6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.radioButtonSize6x6.Location = new System.Drawing.Point(158, 98);
            this.radioButtonSize6x6.Name = "radioButtonSize6x6";
            this.radioButtonSize6x6.Size = new System.Drawing.Size(111, 44);
            this.radioButtonSize6x6.TabIndex = 2;
            this.radioButtonSize6x6.TabStop = true;
            this.radioButtonSize6x6.Text = "6 x 6";
            this.radioButtonSize6x6.UseVisualStyleBackColor = false;
            // 
            // radioButtonSize8x8
            // 
            this.radioButtonSize8x8.AutoSize = true;
            this.radioButtonSize8x8.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonSize8x8.Font = new System.Drawing.Font("Microsoft JhengHei Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonSize8x8.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.radioButtonSize8x8.Location = new System.Drawing.Point(292, 98);
            this.radioButtonSize8x8.Name = "radioButtonSize8x8";
            this.radioButtonSize8x8.Size = new System.Drawing.Size(111, 44);
            this.radioButtonSize8x8.TabIndex = 2;
            this.radioButtonSize8x8.TabStop = true;
            this.radioButtonSize8x8.Text = "8 x 8";
            this.radioButtonSize8x8.UseVisualStyleBackColor = false;
            // 
            // radioButtonSize10x10
            // 
            this.radioButtonSize10x10.AutoSize = true;
            this.radioButtonSize10x10.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonSize10x10.Font = new System.Drawing.Font("Microsoft JhengHei Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonSize10x10.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.radioButtonSize10x10.Location = new System.Drawing.Point(426, 98);
            this.radioButtonSize10x10.Name = "radioButtonSize10x10";
            this.radioButtonSize10x10.Size = new System.Drawing.Size(135, 44);
            this.radioButtonSize10x10.TabIndex = 3;
            this.radioButtonSize10x10.TabStop = true;
            this.radioButtonSize10x10.Text = "10 x 10";
            this.radioButtonSize10x10.UseVisualStyleBackColor = false;
            // 
            // lablePlayers
            // 
            this.lablePlayers.AutoSize = true;
            this.lablePlayers.BackColor = System.Drawing.Color.Transparent;
            this.lablePlayers.Font = new System.Drawing.Font("Microsoft JhengHei Light", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablePlayers.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lablePlayers.Location = new System.Drawing.Point(262, 151);
            this.lablePlayers.Name = "lablePlayers";
            this.lablePlayers.Size = new System.Drawing.Size(151, 51);
            this.lablePlayers.TabIndex = 4;
            this.lablePlayers.Text = "Players";
            // 
            // lablePlayer1
            // 
            this.lablePlayer1.AutoSize = true;
            this.lablePlayer1.BackColor = System.Drawing.Color.Transparent;
            this.lablePlayer1.Font = new System.Drawing.Font("Microsoft JhengHei Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablePlayer1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lablePlayer1.Location = new System.Drawing.Point(183, 229);
            this.lablePlayer1.Name = "lablePlayer1";
            this.lablePlayer1.Size = new System.Drawing.Size(133, 40);
            this.lablePlayer1.TabIndex = 5;
            this.lablePlayer1.Text = "Player 1:";
            // 
            // checkBoxPlayer2
            // 
            this.checkBoxPlayer2.AutoSize = true;
            this.checkBoxPlayer2.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxPlayer2.Font = new System.Drawing.Font("Microsoft JhengHei Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxPlayer2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.checkBoxPlayer2.Location = new System.Drawing.Point(158, 275);
            this.checkBoxPlayer2.Name = "checkBoxPlayer2";
            this.checkBoxPlayer2.Size = new System.Drawing.Size(165, 44);
            this.checkBoxPlayer2.TabIndex = 7;
            this.checkBoxPlayer2.Text = "Player 2:";
            this.checkBoxPlayer2.UseVisualStyleBackColor = false;
            this.checkBoxPlayer2.CheckedChanged += new System.EventHandler(this.checkBoxPlayer2_CheckedChanged);
            // 
            // textBoxFirstPlayer
            // 
            this.textBoxFirstPlayer.Location = new System.Drawing.Point(330, 235);
            this.textBoxFirstPlayer.Name = "textBoxFirstPlayer";
            this.textBoxFirstPlayer.Size = new System.Drawing.Size(168, 26);
            this.textBoxFirstPlayer.TabIndex = 8;
            this.textBoxFirstPlayer.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxPlayer_Validating);
            // 
            // textBoxSecondPlayer
            // 
            this.textBoxSecondPlayer.Enabled = false;
            this.textBoxSecondPlayer.Location = new System.Drawing.Point(330, 285);
            this.textBoxSecondPlayer.Name = "textBoxSecondPlayer";
            this.textBoxSecondPlayer.Size = new System.Drawing.Size(168, 26);
            this.textBoxSecondPlayer.TabIndex = 9;
            this.textBoxSecondPlayer.Text = "[Computer]";
            this.textBoxSecondPlayer.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxPlayer_Validating);
            // 
            // buttonDone
            // 
            this.buttonDone.BackColor = System.Drawing.Color.Transparent;
            this.buttonDone.BackgroundImage = global::CheckersGui.Properties.Resources.play;
            this.buttonDone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonDone.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDone.ForeColor = System.Drawing.Color.Black;
            this.buttonDone.Location = new System.Drawing.Point(292, 357);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(111, 104);
            this.buttonDone.TabIndex = 10;
            this.buttonDone.UseVisualStyleBackColor = false;
            this.buttonDone.Click += new System.EventHandler(this.buttonDone_Click);
            // 
            // errorProviderPlayerName
            // 
            this.errorProviderPlayerName.ContainerControl = this;
            // 
            // FormGameSettings
            // 
            this.AcceptButton = this.buttonDone;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::CheckersGui.Properties.Resources.hro;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(676, 497);
            this.Controls.Add(this.buttonDone);
            this.Controls.Add(this.textBoxSecondPlayer);
            this.Controls.Add(this.textBoxFirstPlayer);
            this.Controls.Add(this.checkBoxPlayer2);
            this.Controls.Add(this.lablePlayer1);
            this.Controls.Add(this.lablePlayers);
            this.Controls.Add(this.radioButtonSize10x10);
            this.Controls.Add(this.radioButtonSize8x8);
            this.Controls.Add(this.radioButtonSize6x6);
            this.Controls.Add(this.lableBoardSize);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormGameSettings";
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chckers";
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderPlayerName)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lableBoardSize;
        private System.Windows.Forms.RadioButton radioButtonSize6x6;
        private System.Windows.Forms.RadioButton radioButtonSize8x8;
        private System.Windows.Forms.RadioButton radioButtonSize10x10;
        private System.Windows.Forms.Label lablePlayers;
        private System.Windows.Forms.Label lablePlayer1;
        private System.Windows.Forms.CheckBox checkBoxPlayer2;
        private System.Windows.Forms.TextBox textBoxFirstPlayer;
        private System.Windows.Forms.TextBox textBoxSecondPlayer;
        private System.Windows.Forms.Button buttonDone;
        private System.Windows.Forms.ErrorProvider errorProviderPlayerName;
    }
}