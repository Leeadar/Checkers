using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using CheckersLogic;

namespace CheckersGui
{
    public class ButtonBoardSquare : Button
    {
        private BoardSquare m_BoardSquare;

        public ButtonBoardSquare(BoardSquare i_BoardSquare)
        {
            m_BoardSquare = i_BoardSquare;
            initializeButtonBoardSquare();
        }

        private void initializeButtonBoardSquare()
        {
            this.SetStyle(ControlStyles.Selectable, false);
            this.Name = m_BoardSquare.Row + "," + m_BoardSquare.Col;
            if ((m_BoardSquare.Row % 2 == 0 && m_BoardSquare.Col % 2 == 0) || (m_BoardSquare.Row % 2 == 1 && m_BoardSquare.Col % 2 == 1))
            {
                this.BackColor = System.Drawing.Color.Transparent;
                this.BackgroundImage = global::CheckersGui.Properties.Resources.Black_Block;
                this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                this.Enabled = false;
            }
            else
            {
                m_BoardSquare.ChangedPieceEventHandler += this.boardSquare_ChangedPiece;
                this.BackColor = System.Drawing.Color.Transparent;
                this.BackgroundImage = global::CheckersGui.Properties.Resources.White_Block;
                this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                updateCheckerPieceOnBoardSquare();
            }
        }

        public BoardSquare BoardSquare
        {
            get
            {
                return m_BoardSquare;
            }
        }

        private void boardSquare_ChangedPiece(BoardSquare i_BoardSquare)
        {
            if (m_BoardSquare == i_BoardSquare)
            {
                updateCheckerPieceOnBoardSquare();
            }
        }

        private void updateCheckerPieceOnBoardSquare()
        {
            if (m_BoardSquare.CheckerPiece != null)
            {
                if (m_BoardSquare.CheckerPiece.PieceOwner.Direction == eGameDirection.Down)
                {
                    if (m_BoardSquare.CheckerPiece.IsPieceKing)
                    {
                        this.Image = global::CheckersGui.Properties.Resources.BlackCheckerKing;
                    }
                    else
                    {
                        this.Image = global::CheckersGui.Properties.Resources.BlackCheckerPiece;
                    }
                }
                else
                {
                    if (m_BoardSquare.CheckerPiece.IsPieceKing)
                    {
                        this.Image = global::CheckersGui.Properties.Resources.WhiteCheckerKing;
                    }
                    else
                    {
                        this.Image = global::CheckersGui.Properties.Resources.WhiteCheckerPiece;
                    }
                }
            }
            else
            {
                this.Image = null;
            }
        }

        protected override bool ShowFocusCues
        {
            get
            {
                return false;
            }
        }
    }
}
