using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckersLogic
{
    public class GameBoard
    {
        private readonly Player m_FirstPlayer;
        private readonly Player m_SecondPlayer;
        private BoardSquare[,] m_Board = null;

        public GameBoard(int i_BoardSize, Player i_FirstPlayer, Player i_SecondPlayer)
        {
            m_FirstPlayer = i_FirstPlayer;
            m_SecondPlayer = i_SecondPlayer;
            m_Board = new BoardSquare[i_BoardSize, i_BoardSize];
            BoardSize = i_BoardSize;
            createEmptyBoard();
        }

        public int BoardSize { get; }

        public Player FirstPlayer
        {
            get
            {
                return m_FirstPlayer;
            }
        }

        public Player SecondPlayer
        {
            get
            {
                return m_SecondPlayer;
            }
        }

        public BoardSquare[,] Board
        {
            get
            {
                return m_Board;
            }

            set
            {
                m_Board = value;
            }
        }

        public GameBoard Clone()
        {
            GameBoard boardCopy = new GameBoard(Board.GetLength(0), FirstPlayer, SecondPlayer);

            for (int i = 0; i < boardCopy.Board.GetLength(0); i++)
            {
                for (int j = 0; j < boardCopy.Board.GetLength(1); j++)
                {
                    boardCopy.Board[i, j] = new BoardSquare(i, j);
                    if (Board[i, j].CheckerPiece != null)
                    {
                        boardCopy.Board[i, j].CheckerPiece = new CheckerPiece(Board[i, j].CheckerPiece.PieceOwner);
                        boardCopy.Board[i, j].CheckerPiece.IsPieceKing = Board[i, j].CheckerPiece.IsPieceKing;
                    }
                }
            }

            return boardCopy;
        }

        public void SetBoardToStartingPosition()
        {
            int secondPlayerInitRows = (m_Board.GetLength(0) / 2) - 1;
            int firstPlayerInitRows = secondPlayerInitRows + 2;
            int rowNum = 0;

            setEmptyBoard();
            for (; rowNum < secondPlayerInitRows; rowNum++)
            {
                initRowInBoard(rowNum, m_SecondPlayer);
            }

            for (rowNum = firstPlayerInitRows; rowNum < m_Board.GetLength(0); rowNum++)
            {
                initRowInBoard(rowNum, m_FirstPlayer);
            }
        }

        private void initRowInBoard(int i_RowNum, Player i_PieceOwner)
        {
            int j = i_RowNum % 2 == 0 ? 1 : 0;

            for (; j < m_Board.GetLength(1); j += 2)
            {
                m_Board[i_RowNum, j].CheckerPiece = new CheckerPiece(i_PieceOwner);
            }
        }

        private void createEmptyBoard()
        {
            for (int i = 0; i < m_Board.GetLength(0); i++)
            {
                for (int j = 0; j < m_Board.GetLength(1); j++)
                {
                    m_Board[i, j] = new BoardSquare(i, j);
                }
            }
        }

        private void setEmptyBoard()
        {
            foreach (BoardSquare boardSquare in Board)
            {
                boardSquare.CheckerPiece = null;
            }
        }

        public bool CheckIfLegalMove(Move i_Move)
        {
            bool isLegalMove = false;

            if (checkIfPieceBelongToPlayer(i_Move) && checkIfDestinationSquareEmpty(i_Move))
            {
                if (CheckIfPlayerHasLegalJumpOption(i_Move.StartBoardSquare.CheckerPiece.PieceOwner))
                {
                    if (CheckIfLegalJump(i_Move))
                    {
                        isLegalMove = true;
                    }
                }
                else if (CheckIfLegalStep(i_Move))
                {
                    isLegalMove = true;
                }
            }

            return isLegalMove;
        }

        public bool CheckIfLegalStep(Move i_Move)
        {
            bool isLegalStep;

            if (i_Move.StartBoardSquare.CheckerPiece.IsPieceKing)
            {
                isLegalStep = checkIfLegalStepUpOrDown(eGameDirection.Up, i_Move) || checkIfLegalStepUpOrDown(eGameDirection.Down, i_Move);
            }
            else
            {
                isLegalStep = checkIfLegalStepUpOrDown(i_Move.Player.Direction, i_Move);
            }

            return isLegalStep;
        }

        private bool checkIfPieceBelongToPlayer(Move i_Move)
        {
            return i_Move.StartBoardSquare.CheckerPiece != null && i_Move.StartBoardSquare.CheckerPiece.PieceOwner == i_Move.Player;
        }

        private bool checkIfDestinationSquareEmpty(Move i_Move)
        {
            return i_Move.EndBoardSquare.CheckerPiece == null;
        }

        private bool checkIfLegalStepUpOrDown(eGameDirection i_Direction, Move i_Move)
        {
            bool isFollowingRows = false;
            bool isDiagonalSquares;

            if (i_Direction == eGameDirection.Up)
            {
                if (i_Move.StartBoardSquare.Row > 0)
                {
                    isFollowingRows = i_Move.StartBoardSquare.Row - 1 == i_Move.EndBoardSquare.Row;
                }
            }
            else if (i_Direction == eGameDirection.Down)
            {
                if (i_Move.StartBoardSquare.Row != m_Board.GetLength(0) - 1)
                {
                    isFollowingRows = i_Move.StartBoardSquare.Row + 1 == i_Move.EndBoardSquare.Row;
                }
            }

            isDiagonalSquares = checkIfDiagonalSquares(i_Move);

            return isFollowingRows && isDiagonalSquares;
        }

        private bool checkIfDiagonalSquares(Move i_Move)
        {
            bool diagonalSquares = false;

            if (i_Move.StartBoardSquare.Col > 0)
            {
                diagonalSquares = i_Move.StartBoardSquare.Col - 1 == i_Move.EndBoardSquare.Col;
            }

            if (!diagonalSquares && i_Move.StartBoardSquare.Col < m_Board.GetLength(1) - 1)
            {
                diagonalSquares = i_Move.StartBoardSquare.Col + 1 == i_Move.EndBoardSquare.Col;
            }

            return diagonalSquares;
        }

        public bool CheckIfLegalJump(Move i_Move)
        {
            bool isLegalJump;

            if (i_Move.StartBoardSquare.CheckerPiece.IsPieceKing)
            {
                isLegalJump = checkIfLegalJumpUpOrDown(eGameDirection.Up, i_Move) || checkIfLegalJumpUpOrDown(eGameDirection.Down, i_Move);
            }
            else
            {
                isLegalJump = checkIfLegalJumpUpOrDown(i_Move.Player.Direction, i_Move);
            }

            return isLegalJump;
        }

        private bool checkIfLegalJumpUpOrDown(eGameDirection i_Direction, Move i_Move)
        {
            bool isRowsDifferenceOne = false;
            bool isOpponentPieceCapture = false;
            CheckerPiece PieceToCaptue = null;
            int direction = 1;

            if (i_Direction == eGameDirection.Up)
            {
                direction = -1;
                if (i_Move.StartBoardSquare.Row > 1)
                {
                    isRowsDifferenceOne = i_Move.StartBoardSquare.Row - 2 == i_Move.EndBoardSquare.Row;
                }
            }
            else if (i_Move.StartBoardSquare.Row < m_Board.GetLength(0) - 2)
            {
                isRowsDifferenceOne = i_Move.StartBoardSquare.Row + 2 == i_Move.EndBoardSquare.Row;
            }

            if (isRowsDifferenceOne && i_Move.StartBoardSquare.Col > 1 && i_Move.StartBoardSquare.Col - 2 == i_Move.EndBoardSquare.Col)
            {
                PieceToCaptue = m_Board[i_Move.StartBoardSquare.Row + direction, i_Move.StartBoardSquare.Col - 1].CheckerPiece;
            }
            else if (isRowsDifferenceOne && i_Move.StartBoardSquare.Col < m_Board.GetLength(1) - 2 && i_Move.StartBoardSquare.Col + 2 == i_Move.EndBoardSquare.Col)
            {
                PieceToCaptue = m_Board[i_Move.StartBoardSquare.Row + direction, i_Move.StartBoardSquare.Col + 1].CheckerPiece;
            }

            if (PieceToCaptue != null)
            {
                isOpponentPieceCapture = PieceToCaptue != null && PieceToCaptue.PieceOwner != i_Move.Player;
            }

            return isOpponentPieceCapture;
        }

        public void CapturePiece(BoardSquare i_BoardSquare)
        {
            i_BoardSquare.CheckerPiece = null;
        }

        private BoardSquare getBoardSquareCapturedPiece(Move i_Move)
        {
            int startRow = i_Move.StartBoardSquare.Row;
            int destRow = i_Move.EndBoardSquare.Row;
            int startCol = i_Move.StartBoardSquare.Col;
            int destCol = i_Move.EndBoardSquare.Col;
            int capturePieceRow = startRow > destRow ? startRow - 1 : startRow + 1;
            int capturePieceCol = startCol > destCol ? startCol - 1 : startCol + 1;

            return Board[capturePieceRow, capturePieceCol];
        }

        public bool CheckIfAnotherJumpIsPossible(BoardSquare i_StartBoardSquare)
        {
            Player playerExecuter = i_StartBoardSquare.CheckerPiece.PieceOwner;
            bool isStepOptionWithPiece = false;
            int borderOfBoard = 2;

            if (i_StartBoardSquare.Row > borderOfBoard - 1)
            {
                if (i_StartBoardSquare.Col > borderOfBoard - 1)
                {
                    isStepOptionWithPiece = isStepOptionWithPiece || createMoveAndCheckIfLegalStepOrJump(i_StartBoardSquare, i_StartBoardSquare.Row - borderOfBoard, i_StartBoardSquare.Col - borderOfBoard, playerExecuter);
                }

                if (i_StartBoardSquare.Col < Board.GetLength(1) - borderOfBoard)
                {
                    isStepOptionWithPiece = isStepOptionWithPiece || createMoveAndCheckIfLegalStepOrJump(i_StartBoardSquare, i_StartBoardSquare.Row - borderOfBoard, i_StartBoardSquare.Col + borderOfBoard, playerExecuter);
                }
            }

            if (i_StartBoardSquare.Row < Board.GetLength(0) - borderOfBoard)
            {
                if (i_StartBoardSquare.Col > borderOfBoard - 1)
                {
                    isStepOptionWithPiece = isStepOptionWithPiece || createMoveAndCheckIfLegalStepOrJump(i_StartBoardSquare, i_StartBoardSquare.Row + borderOfBoard, i_StartBoardSquare.Col - borderOfBoard, playerExecuter);
                }

                if (i_StartBoardSquare.Col < Board.GetLength(1) - borderOfBoard)
                {
                    isStepOptionWithPiece = isStepOptionWithPiece || createMoveAndCheckIfLegalStepOrJump(i_StartBoardSquare, i_StartBoardSquare.Row + borderOfBoard, i_StartBoardSquare.Col + borderOfBoard, playerExecuter);
                }
            }

            return isStepOptionWithPiece;
        }

        private bool createMoveAndCheckIfLegalStepOrJump(BoardSquare i_StartBoardSquare, int i_EndRow, int i_EndCol, Player i_PlayerExecuter)
        {
            BoardSquare endBoardSquare = Board[i_EndRow, i_EndCol];
            Move move = new Move(i_StartBoardSquare, endBoardSquare, i_PlayerExecuter);
            bool isValidMove = checkIfPieceBelongToPlayer(move) && checkIfDestinationSquareEmpty(move);

            return isValidMove && CheckIfLegalJump(move);
        }

        public bool CheckIfPlayerHasNoMoves(Player i_Player)
        {
            return GetAllLegalMoves(i_Player).Count == 0;
        }

        public bool CheckIfPlayerHasLegalJumpOption(Player i_Player)
        {
            bool playerHasLegalJump = false;

            foreach (BoardSquare boardSquare in Board)
            {
                if (!playerHasLegalJump && boardSquare.CheckerPiece != null && boardSquare.CheckerPiece.PieceOwner == i_Player)
                {
                    if (CheckIfAnotherJumpIsPossible(boardSquare))
                    {
                        playerHasLegalJump = true;
                    }
                }
            }

            return playerHasLegalJump;
        }

        public bool CheckIfPlayerWon(Player i_Player)
        {
            Player opponentPlayer = i_Player == FirstPlayer ? SecondPlayer : FirstPlayer;

            return !CheckIfPlayerHasPieces(opponentPlayer);
        }

        public bool CheckIfPlayerLost(Player i_Player)
        {
            Player opponentPlayer = i_Player == FirstPlayer ? SecondPlayer : FirstPlayer;

            return CheckIfPlayerHasNoMoves(i_Player) && !CheckIfPlayerHasNoMoves(opponentPlayer);
        }

        public bool CheckIfTie()
        {
            return CheckIfPlayerHasNoMoves(FirstPlayer) && CheckIfPlayerHasNoMoves(SecondPlayer);
        }

        public bool CheckIfEndGame(Player i_PlayerTurnToPlay)
        {
            return CheckIfPlayerWon(i_PlayerTurnToPlay) || CheckIfPlayerLost(i_PlayerTurnToPlay) || CheckIfTie();
        }

        public bool CheckIfPlayerHasPieces(Player i_Player)
        {
            bool isPlayerHasNoPieces = false;

            foreach (BoardSquare boardSquare in Board)
            {
                if (boardSquare.CheckerPiece != null && boardSquare.CheckerPiece.PieceOwner == i_Player)
                {
                    if (boardSquare.CheckerPiece != null && boardSquare.CheckerPiece.PieceOwner == i_Player)
                    {
                        isPlayerHasNoPieces = true;
                    }
                }
            }

            return isPlayerHasNoPieces;
        }

        public void ExecuteMove(Move i_Move)
        {
            CheckerPiece checkerPiece = i_Move.StartBoardSquare.CheckerPiece;

            makePieceToAKingIfLastRow(i_Move.EndBoardSquare, checkerPiece);
            i_Move.EndBoardSquare.CheckerPiece = checkerPiece;
            i_Move.StartBoardSquare.CheckerPiece = null;
        }

        public void ExecuteMoveAndCapturePiece(Move i_Move)
        {
            BoardSquare boardSquareCapturePiece;

            boardSquareCapturePiece = getBoardSquareCapturedPiece(i_Move);
            ExecuteMove(i_Move);
            CapturePiece(boardSquareCapturePiece);
        }

        private void makePieceToAKingIfLastRow(BoardSquare i_BoardSquare, CheckerPiece i_CheckerPiece)
        {
            if (i_CheckerPiece.PieceOwner.Direction == eGameDirection.Down)
            {
                if (i_BoardSquare.Row == Board.GetLength(0) - 1)
                {
                    i_CheckerPiece.IsPieceKing = true;
                }
            }
            else if (i_CheckerPiece.PieceOwner.Direction == eGameDirection.Up)
            {
                if (i_BoardSquare.Row == 0)
                {
                    i_CheckerPiece.IsPieceKing = true;
                }
            }
        }

        public int GetScoreForWinner()
        {
            int scoreForWinner;
            int firstPlayerScore = 0;
            int secondPlayerScore = 0;

            foreach (BoardSquare boardSquare in Board)
            {
                if (boardSquare.CheckerPiece == null)
                {
                    continue;
                }
                else if (boardSquare.CheckerPiece.PieceOwner == FirstPlayer)
                {
                    if (boardSquare.CheckerPiece.IsPieceKing)
                    {
                        firstPlayerScore += 4;
                    }
                    else
                    {
                        firstPlayerScore += 1;
                    }
                }
                else
                {
                    if (boardSquare.CheckerPiece.IsPieceKing)
                    {
                        secondPlayerScore += 4;
                    }
                    else
                    {
                        secondPlayerScore += 1;
                    }
                }
            }

            if (firstPlayerScore == secondPlayerScore)
            {
                scoreForWinner = 1;
            }
            else
            {
                scoreForWinner = Math.Abs(firstPlayerScore - secondPlayerScore);
            }
            
            return scoreForWinner;
        }

        public List<Move> GetLegalMovesFromBoardSquare(BoardSquare i_StartBoardSquare, Player i_Player, bool i_IsLastMoveJump = false, Move i_LastMove = null)
        {
            List<Move> allLegalMoves = GetAllLegalMoves(i_Player, i_IsLastMoveJump, i_LastMove);
            List<Move> legalMovesFromBoardSquare = new List<Move>();

            foreach (Move move in allLegalMoves)
            {
                if (move.StartBoardSquare == i_StartBoardSquare)
                {
                    legalMovesFromBoardSquare.Add(move);
                }
            }

            return legalMovesFromBoardSquare;
        }

        public List<Move> GetAllLegalMoves(Player i_Player, bool i_IsLastMoveJump = false, Move i_LastMove = null)
        {
            List<Move> allLegalMoves = new List<Move>();

            if (i_IsLastMoveJump)
            {
                addLegalStepOrJumpToList(Move.eMoveType.Jump, i_LastMove.EndBoardSquare, allLegalMoves);
            }
            else
            {
                foreach (BoardSquare boardSquare in Board)
                {
                    if (boardSquare.CheckerPiece != null && boardSquare.CheckerPiece.PieceOwner == i_Player)
                    {
                        addLegalStepOrJumpToList(Move.eMoveType.Jump, boardSquare, allLegalMoves);
                    }
                }

                if (allLegalMoves.Count == 0)
                {
                    foreach (BoardSquare boardSquare in Board)
                    {
                        if (boardSquare.CheckerPiece != null && boardSquare.CheckerPiece.PieceOwner == i_Player)
                        {
                            addLegalStepOrJumpToList(Move.eMoveType.Step, boardSquare, allLegalMoves);
                        }
                    }
                }
            }

            return allLegalMoves;
        }

        private void createMoveAndAddToListIfLegalStepOrJump(Move.eMoveType i_MoveType, BoardSquare i_StartBoardSquare, BoardSquare i_EndBoardSquare, Player i_PlayerExecuter, List<Move> i_AllLegalMoves)
        {
            Move move = new Move(i_StartBoardSquare, i_EndBoardSquare, i_PlayerExecuter);

            if (checkIfPieceBelongToPlayer(move) && checkIfDestinationSquareEmpty(move))
            {
                if (i_MoveType == Move.eMoveType.Jump)
                {
                    if (CheckIfLegalJump(move))
                    {
                        i_AllLegalMoves.Add(move);
                    }
                }
                else if (CheckIfLegalStep(move))
                {
                    i_AllLegalMoves.Add(move);
                }
            }
        }

        private void addLegalStepOrJumpToList(Move.eMoveType i_MoveType, BoardSquare i_StartBoardSquare, List<Move> i_AllLegalMoves)
        {
            Player playerExecuter = i_StartBoardSquare.CheckerPiece.PieceOwner;
            BoardSquare endBoardSquare;
            int borderOfBoard = 1;

            if (i_MoveType == Move.eMoveType.Jump)
            {
                borderOfBoard = 2;
            }

            if (i_StartBoardSquare.Row > borderOfBoard - 1)
            {
                if (i_StartBoardSquare.Col > borderOfBoard - 1)
                {
                    endBoardSquare = Board[i_StartBoardSquare.Row - borderOfBoard, i_StartBoardSquare.Col - borderOfBoard];
                    createMoveAndAddToListIfLegalStepOrJump(i_MoveType, i_StartBoardSquare, endBoardSquare, playerExecuter, i_AllLegalMoves);
                }

                if (i_StartBoardSquare.Col < Board.GetLength(1) - borderOfBoard)
                {
                    endBoardSquare = Board[i_StartBoardSquare.Row - borderOfBoard, i_StartBoardSquare.Col + borderOfBoard];
                    createMoveAndAddToListIfLegalStepOrJump(i_MoveType, i_StartBoardSquare, endBoardSquare, playerExecuter, i_AllLegalMoves);
                }
            }

            if (i_StartBoardSquare.Row < Board.GetLength(0) - borderOfBoard)
            {
                if (i_StartBoardSquare.Col > borderOfBoard - 1)
                {
                    endBoardSquare = Board[i_StartBoardSquare.Row + borderOfBoard, i_StartBoardSquare.Col - borderOfBoard];
                    createMoveAndAddToListIfLegalStepOrJump(i_MoveType, i_StartBoardSquare, endBoardSquare, playerExecuter, i_AllLegalMoves);
                }

                if (i_StartBoardSquare.Col < Board.GetLength(1) - borderOfBoard)
                {
                    endBoardSquare = Board[i_StartBoardSquare.Row + borderOfBoard, i_StartBoardSquare.Col + borderOfBoard];
                    createMoveAndAddToListIfLegalStepOrJump(i_MoveType, i_StartBoardSquare, endBoardSquare, playerExecuter, i_AllLegalMoves);
                }
            }
        }
    }
}
