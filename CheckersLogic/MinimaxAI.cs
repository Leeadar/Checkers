using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckersLogic
{
    public class MinimaxAI
    {
        private const int k_DepthTree = 4;

        public static Move GetBestMove(GameBoard i_GameBoard, Player i_Player, bool i_IsLastMoveJump = false, Move i_LastMove = null)
        {
            Move o_BestMove;
            Player opponentPlayer = i_Player == i_GameBoard.FirstPlayer ? i_GameBoard.SecondPlayer : i_GameBoard.FirstPlayer;
            float io_BoardEvaluate;

            minimax(i_GameBoard, i_Player, opponentPlayer, k_DepthTree, true, out io_BoardEvaluate, out o_BestMove, i_Player, i_IsLastMoveJump, i_LastMove);

            return createMoveOnOtherBoard(o_BestMove, i_GameBoard);
        }

        private static void minimax(GameBoard i_GameBoard, Player i_PlayerTurnToPlay, Player i_PlayerOpponent, int i_DepthTree, bool i_MaxPlayerValue, out float io_BoardEvaluate, out Move o_BestMove, Player i_PlayerToFindBestMove, bool i_IsLastMoveJump = false, Move i_LastMove = null)
        {
            float maxBoardEvaluate = float.NegativeInfinity;
            float minBoardEvaluate = float.PositiveInfinity;
            bool playerStatus = false;
            Move copyMove, bestMove = null;
            List<Move> allLegalMoves = i_GameBoard.GetAllLegalMoves(i_PlayerTurnToPlay, i_IsLastMoveJump, i_LastMove);
            GameBoard copyBoard;

            if (i_DepthTree == 0 || i_GameBoard.CheckIfEndGame(i_PlayerTurnToPlay))
            {
                io_BoardEvaluate = evaluateBoardPosition(i_GameBoard, i_PlayerToFindBestMove);
                o_BestMove = null;
            }
            else
            {
                if (!i_MaxPlayerValue)
                {
                    playerStatus = true;
                }

                foreach (Move move in allLegalMoves)
                {
                    copyBoard = i_GameBoard.Clone();
                    copyMove = createMoveOnOtherBoard(move, copyBoard);
                    if (i_GameBoard.CheckIfLegalStep(move))
                    {
                        copyBoard.ExecuteMove(copyMove);
                        minimax(copyBoard, i_PlayerOpponent, i_PlayerTurnToPlay, i_DepthTree - 1, playerStatus, out io_BoardEvaluate, out o_BestMove, i_PlayerToFindBestMove);
                    }
                    else
                    {
                        copyBoard.ExecuteMoveAndCapturePiece(copyMove);
                        if (copyBoard.CheckIfAnotherJumpIsPossible(copyMove.EndBoardSquare))
                        {
                            minimax(copyBoard, i_PlayerTurnToPlay, i_PlayerOpponent, i_DepthTree - 1, !playerStatus, out io_BoardEvaluate, out o_BestMove, i_PlayerToFindBestMove, true, copyMove);
                        }
                        else
                        {
                            minimax(copyBoard, i_PlayerOpponent, i_PlayerTurnToPlay, i_DepthTree - 1, playerStatus, out io_BoardEvaluate, out o_BestMove, i_PlayerToFindBestMove);
                        }
                    }

                    bestMove = getBestMoveOptions(i_MaxPlayerValue, ref maxBoardEvaluate, ref minBoardEvaluate, io_BoardEvaluate, move, bestMove);
                }

                o_BestMove = bestMove;
                if (i_MaxPlayerValue)
                {
                    io_BoardEvaluate = maxBoardEvaluate;
                }
                else
                {
                    io_BoardEvaluate = minBoardEvaluate;
                }
            }
        }

        private static Move getBestMoveOptions(bool i_MaxValue, ref float o_MaxPlayerValue, ref float o_MinPlayerValue, float i_BoardEvaluate, Move i_Move, Move i_BestMove)
        {
            if (i_MaxValue)
            {
                o_MaxPlayerValue = Math.Max(o_MaxPlayerValue, i_BoardEvaluate);
                if (o_MaxPlayerValue == i_BoardEvaluate)
                {
                    i_BestMove = i_Move;
                }
            }
            else
            {
                o_MinPlayerValue = Math.Min(o_MinPlayerValue, i_BoardEvaluate);
                if (o_MinPlayerValue == i_BoardEvaluate)
                {
                    i_BestMove = i_Move;
                }
            }

            return i_BestMove;
        }

        private static float evaluateBoardPosition(GameBoard i_GameBoard, Player i_Player)
        {
            Player opponentPlayer = i_Player == i_GameBoard.FirstPlayer ? i_GameBoard.SecondPlayer : i_GameBoard.FirstPlayer;
            float playerScore = 0;
            float opponentScore = 0;

            foreach (BoardSquare boardSquare in i_GameBoard.Board)
            {
                if (boardSquare.CheckerPiece != null && boardSquare.CheckerPiece.PieceOwner == i_Player)
                {
                    playerScore += getScoreFromBoardSquare(i_GameBoard, boardSquare);
                }
                else if (boardSquare.CheckerPiece != null && boardSquare.CheckerPiece.PieceOwner == opponentPlayer)
                {
                    opponentScore += getScoreFromBoardSquare(i_GameBoard, boardSquare);
                }
            }

            return playerScore - opponentScore;
        }

        private static float getScoreFromBoardSquare(GameBoard i_GameBoard, BoardSquare i_BoardSquare)
        {
            float playerScore = 0;

            if (i_BoardSquare.CheckerPiece.IsPieceKing)
            {
                playerScore += 5 + i_GameBoard.Board.GetLength(0) + 2;
            }
            else
            {
                if (i_BoardSquare.CheckerPiece.PieceOwner.Direction == eGameDirection.Down)
                {
                    playerScore += 5 + i_BoardSquare.Row;
                }
                else
                {
                    playerScore += 5 + (i_GameBoard.Board.GetLength(0) - i_BoardSquare.Row - 1);
                }
            }

            return playerScore;
        }

        private static Move createMoveOnOtherBoard(Move i_Move, GameBoard i_GameBoard)
        {
            return new Move(i_GameBoard.Board[i_Move.StartBoardSquare.Row, i_Move.StartBoardSquare.Col], i_GameBoard.Board[i_Move.EndBoardSquare.Row, i_Move.EndBoardSquare.Col], i_Move.Player);
        }
    }
}
