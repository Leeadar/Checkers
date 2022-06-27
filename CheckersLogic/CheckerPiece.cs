using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckersLogic
{
    public class CheckerPiece
    {
        private readonly Player m_PieceOwner;
        private bool m_IsPieceKing;

        public CheckerPiece(Player i_PieceOwner)
        {
            m_PieceOwner = i_PieceOwner;
            m_IsPieceKing = false;
        }

        public Player PieceOwner
        {
            get
            {
                return m_PieceOwner;
            }
        }

        public bool IsPieceKing
        {
            get
            {
                return m_IsPieceKing;
            }

            set
            {
                m_IsPieceKing = value;
            }
        }
    }
}
