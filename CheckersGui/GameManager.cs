using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using CheckersLogic;

namespace CheckersGui
{
    public class GameManager
    {
        private GameBoard m_GameBoard;
        private FormGameSettings m_FormGameSettings;
        private FormCheckers m_FormCheckers;

        public GameManager()
        {
            runFormGameSettings();
            if (m_FormGameSettings.DialogResult == DialogResult.OK)
            {
                runFormCheckers();
            }
        }

        public void RestartGame()
        {
            m_GameBoard.SetBoardToStartingPosition();
            PlayerTurnToPlay = m_GameBoard.FirstPlayer;
            PlayerOpponent = m_GameBoard.SecondPlayer;
            m_FormCheckers.ResetPlayerLabelMark();
            m_FormCheckers.ResetButtonBoardSquareMark();
        }

        private void runFormGameSettings()
        {
            m_FormGameSettings = new FormGameSettings();
            m_FormGameSettings.ShowDialog();
        }

        private void runFormCheckers()
        {
            m_GameBoard = getBoardAccordingToGameSettingsForm(m_FormGameSettings);
            m_GameBoard.SetBoardToStartingPosition();
            PlayerTurnToPlay = m_GameBoard.FirstPlayer;
            PlayerOpponent = m_GameBoard.SecondPlayer;
            m_FormCheckers = new FormCheckers(this, m_GameBoard);
            m_FormCheckers.ShowDialog();
        }

        private GameBoard getBoardAccordingToGameSettingsForm(FormGameSettings i_FormGameSettings)
        {
            Player firstPlayer = new Player(ePlayerType.Human, eGameDirection.Up, i_FormGameSettings.FirstPlayerName);
            Player secondPlayer = new Player(i_FormGameSettings.SecondPlayerType, eGameDirection.Down, i_FormGameSettings.SecondPlayerName);
            int boardSize = i_FormGameSettings.BoardSize;

            return new GameBoard(boardSize, firstPlayer, secondPlayer);
        }

        public Player PlayerTurnToPlay { get; set; }

        public Player PlayerOpponent { get; set; }

        public bool IsAnotherJumpIsPossible { get; set; }

        public Move LastMove { get; set; }

        private void executeMove(Move i_Move)
        {
            if (m_GameBoard.CheckIfLegalStep(i_Move))
            {
                m_FormCheckers.PlayStepSound();
                m_GameBoard.ExecuteMove(i_Move);
                IsAnotherJumpIsPossible = false;
            }
            else if (m_GameBoard.CheckIfLegalJump(i_Move))
            {
                m_FormCheckers.PlayJumpSound();
                m_GameBoard.ExecuteMoveAndCapturePiece(i_Move);
                if (m_GameBoard.CheckIfAnotherJumpIsPossible(i_Move.EndBoardSquare))
                {
                    IsAnotherJumpIsPossible = true;
                    LastMove = i_Move;
                }
                else
                {
                    IsAnotherJumpIsPossible = false;
                }
            }
        }

        public void ManageTurn(Move i_Move)
        {
            executeMove(i_Move);
            if (!IsAnotherJumpIsPossible)
            {
                swapTurns();
                if (m_GameBoard.CheckIfEndGame(PlayerTurnToPlay))
                {
                    calculateResultsAndAddScoreToWinner();
                }
            }

            if (PlayerTurnToPlay.PlayerType == ePlayerType.Computer)
            {
                m_FormCheckers.StartTimerAndPlayComputerMove();
            }
        }

        public void PlayComputerMove()
        {
            ManageTurn(MinimaxAI.GetBestMove(m_GameBoard, PlayerTurnToPlay, IsAnotherJumpIsPossible, LastMove));
        }

        public bool CheckIfMoveIsLegal(BoardSquare i_StartBoardSquare, BoardSquare i_EndBoardSquare, out Move o_MoveToExecute)
        {
            List<Move> legalMovesFromBoardSquare = m_GameBoard.GetLegalMovesFromBoardSquare(i_StartBoardSquare, PlayerTurnToPlay, IsAnotherJumpIsPossible, LastMove);
            bool isLegalMove = false;

            o_MoveToExecute = null;
            foreach (Move move in legalMovesFromBoardSquare)
            {
                if (i_EndBoardSquare == move.EndBoardSquare)
                {
                    isLegalMove = true;
                    o_MoveToExecute = move;
                }
            }

            return isLegalMove;
        }

        private void swapTurns()
        {
            Player tempPlayerForSwap = PlayerTurnToPlay;

            m_FormCheckers.SwapBoldPlayerName();
            PlayerTurnToPlay = PlayerOpponent;
            PlayerOpponent = tempPlayerForSwap;
        }

        public void AddScoreToWinnerAfterPlayerResign()
        {
            if (m_GameBoard.SecondPlayer.PlayerType == ePlayerType.Computer)
            {
                m_GameBoard.SecondPlayer.AddScoreToPlayer(m_GameBoard.GetScoreForWinner());
            }
            else
            {
                PlayerOpponent.AddScoreToPlayer(m_GameBoard.GetScoreForWinner());
            }
        }

        private void calculateResultsAndAddScoreToWinner()
        {
            if (!m_GameBoard.CheckIfTie())
            {
                if (m_GameBoard.CheckIfPlayerWon(m_GameBoard.FirstPlayer) || m_GameBoard.CheckIfPlayerLost(m_GameBoard.SecondPlayer))
                {
                    m_GameBoard.FirstPlayer.AddScoreToPlayer(m_GameBoard.GetScoreForWinner());
                    m_FormCheckers.ShowMessageGameOverWin(m_GameBoard.FirstPlayer);
                }
                else
                {
                    m_GameBoard.SecondPlayer.AddScoreToPlayer(m_GameBoard.GetScoreForWinner());
                    m_FormCheckers.ShowMessageGameOverWin(m_GameBoard.SecondPlayer);
                }
            }
            else
            {
                m_FormCheckers.ShowMessageGameOverTie();
            }
        }
    }
}
