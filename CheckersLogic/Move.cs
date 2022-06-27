using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckersLogic
{
    public class Move
    {
        private readonly BoardSquare m_StartBoardSquare;
        private readonly BoardSquare m_EndBoardSquare;
        private readonly Player m_PlayerExecuter;

        public Move(BoardSquare i_StartBoardSquare, BoardSquare i_EndBoardSquare, Player i_PlayerExecuter)
        {
            m_StartBoardSquare = i_StartBoardSquare;
            m_EndBoardSquare = i_EndBoardSquare;
            m_PlayerExecuter = i_PlayerExecuter;
        }

        public BoardSquare StartBoardSquare
        {
            get
            {
                return m_StartBoardSquare;
            }
        }

        public BoardSquare EndBoardSquare
        {
            get
            {
                return m_EndBoardSquare;
            }
        }

        public Player Player
        {
            get
            {
                return m_PlayerExecuter;
            }
        }

        public enum eMoveType
        {
            Step,
            Jump
        }
    }
}
