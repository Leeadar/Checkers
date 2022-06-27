using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CheckersGui
{
    public partial class FormGameSettings : Form
    {
        public FormGameSettings()
        {
            InitializeComponent();
        }

        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            if (checkBox.Checked == true)
            {
                textBoxSecondPlayer.Enabled = true;
                textBoxSecondPlayer.Clear();
            }
            else
            {
                textBoxSecondPlayer.Enabled = false;
                textBoxSecondPlayer.Text = "[Computer]";
            }
        }

        public string FirstPlayerName
        {
            get
            {
                return textBoxFirstPlayer.Text;
            }
        }

        public string SecondPlayerName
        {
            get
            {
                string secondPlayerName;

                if (textBoxSecondPlayer.Enabled == false)
                {
                    secondPlayerName = "Computer";
                }
                else
                {
                    secondPlayerName = textBoxSecondPlayer.Text;
                }

                return secondPlayerName;
            }
        }

        public int BoardSize
        {
            get
            {
                int boardSize = 8;

                if (radioButtonSize10x10.Checked)
                {
                    boardSize = 10;
                }
                else if (radioButtonSize6x6.Checked)
                {
                    boardSize = 6;
                }

                return boardSize;
            }
        }

        public CheckersLogic.ePlayerType SecondPlayerType
        {
            get
            {
                return checkBoxPlayer2.Checked ? CheckersLogic.ePlayerType.Human : CheckersLogic.ePlayerType.Computer;
            }
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren(ValidationConstraints.Enabled))
            {
                MessageBox.Show("Error - Please enter valid name", "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void textBoxPlayer_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if ((textBox.Name == "textBoxFirstPlayer" && string.IsNullOrWhiteSpace(textBoxFirstPlayer.Text)) || textBoxFirstPlayer.Text.Length > 20)
            {
                e.Cancel = true;
                textBoxFirstPlayer.Focus();
                errorProviderPlayerName.SetError(textBoxFirstPlayer, "Name should not be left blank");
            }
            else if ((textBox.Name == "textBoxSecondPlayer" && string.IsNullOrWhiteSpace(textBoxSecondPlayer.Text)) || textBoxSecondPlayer.Text.Length > 20)
            {
                e.Cancel = true;
                textBoxSecondPlayer.Focus();
                errorProviderPlayerName.SetError(textBoxSecondPlayer, "Name should not be left blank");
            }
            else
            {
                e.Cancel = false;
                errorProviderPlayerName.SetError(textBoxFirstPlayer, string.Empty);
                errorProviderPlayerName.SetError(textBoxSecondPlayer, string.Empty);
            }
        }
    }
}
