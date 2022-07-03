using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;
using System.Drawing;
using System.Windows.Forms;
using CheckersLogic;

namespace CheckersGui
{
    public class FormCheckers : Form
    {
        private const int k_ButtonSize = 70;
        private const int k_SpaceFromMargins = 2;
        private readonly GameManager r_GameManager;
        private readonly GameBoard r_GameBoard;
        private ButtonBoardSquare[,] m_ButtonBoardSquare = null;
        private ButtonBoardSquare m_ButtonStartBoardSquare = null;
        private bool m_IsGameOn = true;
        private Label m_LabelFirstPlayer;
        private Label m_LabelFirstPlayerScore;
        private Label m_LabelSecondPlayer;
        private Label m_LabelSecondPlayerScore;
        private PictureBox m_PictureWhitePiece;
        private PictureBox m_PictureBlackPiece;
        private SoundPlayer m_SoundCheckerPieceStep;
        private SoundPlayer m_SoundCheckerPieceJump;
        private SoundPlayer m_SoundGameOverWin;
        private SoundPlayer m_SoundGameOverLose;
        private Timer m_TimerComputerMove;

        public bool IsButtonBoardSquareMarks { get; set; }

        public FormCheckers(GameManager i_GameManager, GameBoard i_GameBoard)
        {
            r_GameBoard = i_GameBoard;
            r_GameManager = i_GameManager;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            initializeForm();
            initializeBoardSquares();
            initializeLabels();
            initializePictureBoxs();
            initializeSoundPlayers();
            initializeTimer();
        }

        private void initializeForm()
        {
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Text = "Checkers";
            this.Name = "FormCheckers";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormCheckers_FormClosing);
        }

        private void initializeBoardSquares()
        {
            m_ButtonBoardSquare = new ButtonBoardSquare[r_GameBoard.BoardSize, r_GameBoard.BoardSize];

            int top = k_SpaceFromMargins + k_ButtonSize;
            int left = k_SpaceFromMargins;

            for (int i = 0; i < r_GameBoard.BoardSize; i++)
            {
                for (int j = 0; j < r_GameBoard.BoardSize; j++)
                {
                    m_ButtonBoardSquare[i, j] = new ButtonBoardSquare(r_GameBoard.Board[i, j]);
                    m_ButtonBoardSquare[i, j].Location = new Point(left, top);
                    m_ButtonBoardSquare[i, j].Size = new Size(k_ButtonSize, k_ButtonSize);
                    m_ButtonBoardSquare[i, j].FlatStyle = FlatStyle.Flat;
                    m_ButtonBoardSquare[i, j].FlatAppearance.BorderColor = Color.Black;
                    m_ButtonBoardSquare[i, j].FlatAppearance.BorderSize = 1;
                    left += k_ButtonSize;
                    this.Controls.Add(m_ButtonBoardSquare[i, j]);
                    this.m_ButtonBoardSquare[i, j].Click += new System.EventHandler(this.buttonBoardSquare_Click);
                }

                top += k_ButtonSize;
                left = k_SpaceFromMargins;
            }
        }

        private void initializeLabels()
        {
            int maxTextLen;

            m_LabelFirstPlayer = new Label();
            m_LabelFirstPlayerScore = new Label();
            m_LabelSecondPlayer = new Label();
            m_LabelSecondPlayerScore = new Label();

            m_LabelFirstPlayer.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            m_LabelFirstPlayer.Location = new System.Drawing.Point(54, 10);
            m_LabelFirstPlayer.TabIndex = 2;
            m_LabelFirstPlayer.Text = r_GameBoard.FirstPlayer.PlayerName + ": ";
            m_LabelFirstPlayer.AutoSize = true;

            m_LabelSecondPlayer.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            m_LabelSecondPlayer.Location = new System.Drawing.Point(54, 45);
            m_LabelSecondPlayer.TabIndex = 2;
            m_LabelSecondPlayer.Text = r_GameBoard.SecondPlayer.PlayerName + ": ";
            m_LabelSecondPlayer.AutoSize = true;

            maxTextLen = Math.Max(((10 * m_LabelFirstPlayer.Text.Length) + m_LabelFirstPlayer.Right), ((10 * m_LabelSecondPlayer.Text.Length) + m_LabelSecondPlayer.Right));

            m_LabelFirstPlayerScore.AutoSize = true;
            m_LabelFirstPlayerScore.Text = "0";
            m_LabelFirstPlayerScore.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            m_LabelFirstPlayerScore.Location = new System.Drawing.Point(maxTextLen, 10);
            m_LabelFirstPlayerScore.TabIndex = 2;

            m_LabelSecondPlayerScore.AutoSize = true;
            m_LabelSecondPlayerScore.Text = "0";
            m_LabelSecondPlayerScore.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            m_LabelSecondPlayerScore.Location = new System.Drawing.Point(maxTextLen, 45);
            m_LabelSecondPlayerScore.TabIndex = 2;

            r_GameBoard.FirstPlayer.ChangedScoreEventHandler += this.player_ChangedScore;
            r_GameBoard.SecondPlayer.ChangedScoreEventHandler += this.player_ChangedScore;

            this.Controls.Add(m_LabelFirstPlayer);
            this.Controls.Add(m_LabelFirstPlayerScore);
            this.Controls.Add(m_LabelSecondPlayer);
            this.Controls.Add(m_LabelSecondPlayerScore);
        }

        private void initializePictureBoxs()
        {
            m_PictureWhitePiece = new PictureBox();
            m_PictureBlackPiece = new PictureBox();

            m_PictureWhitePiece.BackColor = System.Drawing.Color.Transparent;
            m_PictureWhitePiece.BackgroundImage = global::CheckersGui.Properties.Resources.WhiteCheckerPiece;
            m_PictureWhitePiece.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            m_PictureWhitePiece.Location = new System.Drawing.Point(22, 5);
            m_PictureWhitePiece.Name = "m_PictureWhitePiece";
            m_PictureWhitePiece.Size = new System.Drawing.Size(30, 30);
            m_PictureWhitePiece.TabIndex = 5;
            m_PictureWhitePiece.TabStop = false;

            m_PictureBlackPiece.BackColor = System.Drawing.Color.Transparent;
            m_PictureBlackPiece.BackgroundImage = global::CheckersGui.Properties.Resources.BlackCheckerPiece;
            m_PictureBlackPiece.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            m_PictureBlackPiece.Location = new System.Drawing.Point(22, 40);
            m_PictureBlackPiece.Name = "m_PictureBlackPiece";
            m_PictureBlackPiece.Size = new System.Drawing.Size(30, 30);
            m_PictureBlackPiece.TabIndex = 5;
            m_PictureBlackPiece.TabStop = false;

            this.Controls.Add(m_PictureWhitePiece);
            this.Controls.Add(m_PictureBlackPiece);
        }

        private void initializeSoundPlayers()
        {
            m_SoundCheckerPieceStep = new SoundPlayer(CheckersGui.Properties.Resources.CheckerPieceStep);
            m_SoundCheckerPieceJump = new SoundPlayer(CheckersGui.Properties.Resources.CheckerPieceJump);
            m_SoundGameOverWin = new SoundPlayer(CheckersGui.Properties.Resources.GameOverWin);
            m_SoundGameOverLose = new SoundPlayer(CheckersGui.Properties.Resources.GameOverLose);
        }

        private void initializeTimer()
        {
            this.m_TimerComputerMove = new Timer();
            this.m_TimerComputerMove.Interval = 1400;
            this.m_TimerComputerMove.Tick += new EventHandler(this.timerComputerMove_Tick);
        }

        private void timerComputerMove_Tick(object sender, EventArgs e)
        {
            m_TimerComputerMove.Stop();
            if (m_IsGameOn)
            {
                r_GameManager.PlayComputerMove();
            }
        }

        public void StartTimerAndPlayComputerMove()
        {
            m_TimerComputerMove.Start();
        }

        private void player_ChangedScore(Player i_Player)
        {
            if (i_Player == r_GameBoard.FirstPlayer)
            {
                m_LabelFirstPlayerScore.Text = i_Player.Score.ToString();
            }
            else
            {
                m_LabelSecondPlayerScore.Text = i_Player.Score.ToString();
            }
        }

        private void buttonBoardSquare_Click(object sender, EventArgs e)
        {
            ButtonBoardSquare buttonBoardSquare = sender as ButtonBoardSquare;
            Move moveToExecute;

            if (!IsButtonBoardSquareMarks)
            {
                markStartBoardSquare(buttonBoardSquare);
            }
            else
            {
                removeMarksFromBoardSquares();
                if (r_GameManager.CheckIfMoveIsLegal(m_ButtonStartBoardSquare.BoardSquare, buttonBoardSquare.BoardSquare, out moveToExecute))
                {
                    r_GameManager.ManageTurn(moveToExecute);
                }
                else if (!(IsButtonBoardSquareMarks && buttonBoardSquare == m_ButtonStartBoardSquare))
                {
                    MessageBox.Show("Error - move is not valid", "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                IsButtonBoardSquareMarks = false;
                m_ButtonStartBoardSquare = null;
            }
        }

        public void PlayStepSound()
        {
            m_SoundCheckerPieceStep.Play();
        }

        public void PlayJumpSound()
        {
            m_SoundCheckerPieceJump.Play();
        }

        public void ResetPlayerLabelMark()
        {
            m_LabelFirstPlayer.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            m_LabelSecondPlayer.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        public void ResetButtonBoardSquareMark()
        {
            removeMarksFromBoardSquares();
            IsButtonBoardSquareMarks = false;
            m_ButtonStartBoardSquare = null;
        }

        private void markStartBoardSquare(ButtonBoardSquare i_StartButtonBoardSquare)
        {
            List<Move> legalMovesFromBoardSquare = r_GameBoard.GetLegalMovesFromBoardSquare(i_StartButtonBoardSquare.BoardSquare, r_GameManager.PlayerTurnToPlay, r_GameManager.IsAnotherJumpIsPossible, r_GameManager.LastMove);

            if (i_StartButtonBoardSquare.BoardSquare.CheckerPiece != null && i_StartButtonBoardSquare.BoardSquare.CheckerPiece.PieceOwner == r_GameManager.PlayerTurnToPlay && r_GameManager.PlayerTurnToPlay.PlayerType == ePlayerType.Human)
            {
                if (legalMovesFromBoardSquare.Count > 0)
                {
                    i_StartButtonBoardSquare.BackgroundImage = global::CheckersGui.Properties.Resources.Blue_Block;
                    markEndBoardSquares(legalMovesFromBoardSquare);
                    m_ButtonStartBoardSquare = i_StartButtonBoardSquare;
                    IsButtonBoardSquareMarks = true;
                }
            }
        }

        private void markEndBoardSquares(List<Move> legalMovesFromBoardSquare)
        {
            int row, col;

            foreach (Move move in legalMovesFromBoardSquare)
            {
                row = move.EndBoardSquare.Row;
                col = move.EndBoardSquare.Col;
                m_ButtonBoardSquare[row, col].BackgroundImage = global::CheckersGui.Properties.Resources.PossibleMove;
            }
        }

        private void removeMarksFromBoardSquares()
        {
            List<Move> legalMovesFromBoardSquare;
            int row, col;

            if (IsButtonBoardSquareMarks)
            {
                m_ButtonStartBoardSquare.BackgroundImage = global::CheckersGui.Properties.Resources.White_Block;
                legalMovesFromBoardSquare = r_GameBoard.GetLegalMovesFromBoardSquare(m_ButtonStartBoardSquare.BoardSquare, r_GameManager.PlayerTurnToPlay, r_GameManager.IsAnotherJumpIsPossible, r_GameManager.LastMove);
                foreach (Move move in legalMovesFromBoardSquare)
                {
                    row = move.EndBoardSquare.Row;
                    col = move.EndBoardSquare.Col;
                    m_ButtonBoardSquare[row, col].BackgroundImage = global::CheckersGui.Properties.Resources.White_Block;
                }   
            }
        }

        public void ShowMessageGameOverWin(Player i_PlayerWon)
        {
            string playerWonMsg = string.Format(@"{0} won! 
Another round?", i_PlayerWon.PlayerName);

            playGameOverSound(i_PlayerWon);
            showGameOverMessageBox(playerWonMsg);
        }

        private void playGameOverSound(Player i_PlayerWon)
        {
            if (i_PlayerWon.PlayerType == ePlayerType.Computer)
            {
                m_SoundGameOverLose.Play();
            }
            else
            {
                m_SoundGameOverWin.Play();
            }
        }

        public void ShowMessageGameOverTie()
        {
            string gameOverTieMsg = string.Format(
    @"Tie! 
Another round?");

            showGameOverMessageBox(gameOverTieMsg);
        }

        private void showGameOverMessageBox(string i_GameOverMsg)
        {
            if (MessageBox.Show(i_GameOverMsg, "Checkers", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                r_GameManager.RestartGame();
            }
            else
            {
                m_IsGameOn = false;
                this.Close();
            }
        }

        public void SwapBoldPlayerName()
        {
            if (m_LabelFirstPlayer.Font.Underline)
            {
                m_LabelFirstPlayer.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                m_LabelSecondPlayer.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }
            else
            {
                m_LabelFirstPlayer.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                m_LabelSecondPlayer.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }
        }

        private bool isAnotherRoundAfterPlayerResign()
        {
            string resignPlayer = r_GameBoard.SecondPlayer.PlayerType == ePlayerType.Computer ? r_GameBoard.FirstPlayer.PlayerName : r_GameManager.PlayerTurnToPlay.PlayerName;
            string msgPlayerResign = string.Format(@"{0} resigned! 
Another round?", resignPlayer);

            if (r_GameBoard.SecondPlayer.PlayerType == ePlayerType.Computer)
            {
                playGameOverSound(r_GameBoard.SecondPlayer);
            }
            else
            {
                playGameOverSound(r_GameManager.PlayerOpponent);
            }

            return MessageBox.Show(msgPlayerResign, "Checkers", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        private void FormCheckers_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_IsGameOn)
            {
                m_IsGameOn = false;
                if (isAnotherRoundAfterPlayerResign())
                {
                    m_IsGameOn = true;
                    r_GameManager.AddScoreToWinnerAfterPlayerResign();
                    r_GameManager.RestartGame();
                    e.Cancel = true;
                }
            }
        }
    }
}
