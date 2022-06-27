using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckersLogic
{
    public class BoardSquare
    {
        public event Action<BoardSquare> ChangedPieceEventHandler;

        private readonly int r_Row;
        private readonly int r_Col;
        private CheckerPiece m_CheckerPiece = null;

        public BoardSquare(int i_Row, int i_Col)
        {
            r_Row = i_Row;
            r_Col = i_Col;
        }

        public CheckerPiece CheckerPiece
        {
            get
            {
                return m_CheckerPiece;
            }

            set
            {
                m_CheckerPiece = value;
                OnChangedPiece();
            }
        }

        public int Row
        {
            get
            {
                return r_Row;
            }
        }

        public int Col
        {
            get
            {
                return r_Col;
            }
        }

        protected virtual void OnChangedPiece()
        {
            if (ChangedPieceEventHandler != null)
            {
                ChangedPieceEventHandler.Invoke(this);
            }
        }
    }
}
