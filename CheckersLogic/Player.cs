using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckersLogic
{
    public class Player
    {
        public event Action<Player> ChangedScoreEventHandler;

        private readonly ePlayerType r_PlayerType;
        private readonly string r_PlayerName;
        private readonly eGameDirection r_Direction;
        private int m_Score;

        public Player(ePlayerType i_PlayerType, eGameDirection i_DirectionInGame, string i_PlayerName)
        {
            r_PlayerName = i_PlayerName;
            r_PlayerType = i_PlayerType;
            r_Direction = i_DirectionInGame;
            m_Score = 0;
        }

        public eGameDirection Direction
        {
            get
            {
                return r_Direction;
            }
        }

        public int Score
        {
            get
            {
                return m_Score;
            }
        }

        public string PlayerName
        {
            get
            {
                return r_PlayerName;
            }
        }

        public ePlayerType PlayerType
        {
            get
            {
                return r_PlayerType;
            }
        }

        public void AddScoreToPlayer(int i_Score)
        {
            m_Score += i_Score;
            OnChangedScore();
        }

        protected virtual void OnChangedScore()
        {
            if (ChangedScoreEventHandler != null)
            {
                ChangedScoreEventHandler.Invoke(this);
            }
        }
    }
}
