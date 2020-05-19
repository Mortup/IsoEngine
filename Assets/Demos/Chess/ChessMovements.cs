using System.Collections.Generic;
using UnityEngine;

namespace com.mortup.iso.demo.chess {

    public static class ChessMovements {

        public static List<Vector2Int> GetMovementOptions(Level level, ChessIndex pieceType, Vector2Int position) {         
            switch (pieceType) {
                case ChessIndex.BLACK_KING:
                case ChessIndex.WHITE_KING:
                    return GetKingMovements(level, position);
                case ChessIndex.BLACK_QUEEN:
                case ChessIndex.WHITE_QUEEN:
                    return GetQueenMovements(level, position);
                case ChessIndex.BLACK_TOWER:
                case ChessIndex.WHITE_TOWER:
                    return GetTowerMovements(level, position);
                case ChessIndex.BLACK_KNIGHT:
                case ChessIndex.WHITE_KNIGHT:
                    return GetKnightMovements(level, position);
                case ChessIndex.BLACK_BISHOP:
                case ChessIndex.WHITE_BISHOP:
                    return GetBishopMovements(level, position);
                case ChessIndex.BLACK_PAWN:
                case ChessIndex.WHITE_PAWN:
                    return GetPawnMovements(level, position);
            }

            return null;
        }

        private static List<Vector2Int> GetPawnMovements(Level level, Vector2Int pawnPosition) {
            List<Vector2Int> result = new List<Vector2Int>();

            ChessIndex pawnType = (ChessIndex)level.data.GetItem(pawnPosition).x;
            Vector2Int destination = pawnType.IsWhite() ? new Vector2Int(0, -1) : new Vector2Int(0, 1);
            Vector2Int extraDestination = pawnType.IsWhite() ? new Vector2Int(0, -2) : new Vector2Int(0, 2);

            destination += pawnPosition;
            extraDestination += pawnPosition;

            if (level.data.IsItemInBounds(destination)) {
                int destinationType = level.data.GetItem(destination).x;
                if ((ChessIndex)destinationType == ChessIndex.EMPTY) {
                    result.Add(destination);

                    if (level.data.IsItemInBounds(extraDestination) && (pawnType.IsWhite() && pawnPosition.y == 6) || (pawnType.IsBlack() && pawnPosition.y == 1)) {
                        int extraDestinationType = level.data.GetItem(extraDestination).x;
                        if (CanMove((int)pawnType, extraDestinationType)) {
                            result.Add(extraDestination);
                        }
                    }
                }
            }

            Vector2Int attackOne = pawnType.IsWhite() ? new Vector2Int(1, -1) : new Vector2Int(1, 1);
            Vector2Int attackTwo = pawnType.IsWhite() ? new Vector2Int(-1, -1) : new Vector2Int(-1, 1);
            attackOne += pawnPosition;
            attackTwo += pawnPosition;
            if (level.data.IsItemInBounds(attackOne)) {
                int destinationType = level.data.GetItem(attackOne).x;
                if (CanEat((int)pawnType, destinationType)) {
                    result.Add(attackOne);
                }
            }

            if (level.data.IsItemInBounds(attackTwo)) {
                int destinationType = level.data.GetItem(attackTwo).x;
                if (CanEat((int)pawnType, destinationType)) {
                    result.Add(attackTwo);
                }
            }

            return result;
        }

        private static List<Vector2Int> GetTowerMovements(Level level, Vector2Int towerPosition) {
            List<Vector2Int> result = TraceMovementRay(level, towerPosition, new Vector2Int(1, 0));
            result.AddRange(TraceMovementRay(level, towerPosition, new Vector2Int(-1, 0)));
            result.AddRange(TraceMovementRay(level, towerPosition, new Vector2Int(0, 1)));
            result.AddRange(TraceMovementRay(level, towerPosition, new Vector2Int(0, -1)));
            return result;
        }

        private static List<Vector2Int> GetKnightMovements(Level level, Vector2Int knightPosition) {
            List<Vector2Int> localTargets = new List<Vector2Int>();
            localTargets.Add(new Vector2Int(2, 1));
            localTargets.Add(new Vector2Int(2, -1));
            localTargets.Add(new Vector2Int(-2, 1));
            localTargets.Add(new Vector2Int(-2, -1));
            localTargets.Add(new Vector2Int(1, 2));
            localTargets.Add(new Vector2Int(-1, 2));
            localTargets.Add(new Vector2Int(1, -2));
            localTargets.Add(new Vector2Int(-1, -2));

            int knightType = level.data.GetItem(knightPosition).x;
            List<Vector2Int> result = new List<Vector2Int>();
            foreach (Vector2Int local in localTargets) {
                Vector2Int world = local + knightPosition;

                if (level.data.IsItemInBounds(world)) {
                    int destinationType = level.data.GetItem(world).x;

                    if (CanMove(knightType, destinationType)) {
                        result.Add(world);
                    }
                }
            }

            return result;
        }

        private static List<Vector2Int> GetBishopMovements(Level level, Vector2Int bishopPosition) {
            List<Vector2Int> result = TraceMovementRay(level, bishopPosition, new Vector2Int(1, 1));
            result.AddRange(TraceMovementRay(level, bishopPosition, new Vector2Int(-1, -1)));
            result.AddRange(TraceMovementRay(level, bishopPosition, new Vector2Int(-1, 1)));
            result.AddRange(TraceMovementRay(level, bishopPosition, new Vector2Int(1, -1)));
            return result;
        }

        private static List<Vector2Int> GetQueenMovements(Level level, Vector2Int queenPosition) {
            List<Vector2Int> result = TraceMovementRay(level, queenPosition, new Vector2Int(1, 1));
            result.AddRange(TraceMovementRay(level, queenPosition, new Vector2Int(-1, -1)));
            result.AddRange(TraceMovementRay(level, queenPosition, new Vector2Int(-1, 1)));
            result.AddRange(TraceMovementRay(level, queenPosition, new Vector2Int(1, -1)));
            result.AddRange(TraceMovementRay(level, queenPosition, new Vector2Int(1, 0)));
            result.AddRange(TraceMovementRay(level, queenPosition, new Vector2Int(-1, 0)));
            result.AddRange(TraceMovementRay(level, queenPosition, new Vector2Int(0, 1)));
            result.AddRange(TraceMovementRay(level, queenPosition, new Vector2Int(0, -1)));
            return result;
        }

        private static List<Vector2Int> GetKingMovements(Level level, Vector2Int kingPosition) {
            List<Vector2Int> localMovementOptions = new List<Vector2Int>();
            localMovementOptions.Add(new Vector2Int(1, -1));
            localMovementOptions.Add(new Vector2Int(1, 0));
            localMovementOptions.Add(new Vector2Int(1, 1));
            localMovementOptions.Add(new Vector2Int(0, -1));
            localMovementOptions.Add(new Vector2Int(0, 1));
            localMovementOptions.Add(new Vector2Int(-1, -1));
            localMovementOptions.Add(new Vector2Int(-1, 0));
            localMovementOptions.Add(new Vector2Int(-1, 1));

            List<Vector2Int> result = new List<Vector2Int>();
            foreach (Vector2Int localOption in localMovementOptions) {
                Vector2Int globalPosition = localOption + kingPosition;

                int kingType = level.data.GetItem(kingPosition).x;
                if (level.data.IsItemInBounds(globalPosition)) {
                    int targetType = level.data.GetItem(globalPosition).x;
                    if (CanMove(kingType, targetType)) {
                        result.Add(globalPosition);
                    }
                }
            }

            return result;
        }

        private static List<Vector2Int> TraceMovementRay(Level level, Vector2Int originalPosition, Vector2Int direction) {
            int towerType = level.data.GetItem(originalPosition).x;
            List<Vector2Int> result = new List<Vector2Int>();

            for (int i = 1; i < 9; i++) {
                Vector2Int target = originalPosition + direction * i;
                if (level.data.IsItemInBounds(target) == false)
                    break;

                int destinationType = level.data.GetItem(target.x, target.y).x;
                if (CanMove(towerType, destinationType) == false)
                    break;

                result.Add(target);

                if (ChessIndex.EMPTY != (ChessIndex)destinationType)
                    break;
            }

            return result;
        }

        private static bool CanMove(int pieceType, int destinationType) {
            return CanMove((ChessIndex)pieceType, (ChessIndex)destinationType);
        }

        private static bool CanMove(ChessIndex pieceType, ChessIndex destinationType) {
            if (pieceType.IsWhite() && !destinationType.IsWhite())
                return true;

            if (pieceType.IsBlack() && !destinationType.IsBlack())
                return true;

            return false;
        }

        private static bool CanEat(int pieceType, int destinationType) {
            return CanEat((ChessIndex)pieceType, (ChessIndex)destinationType);
        }

        private static bool CanEat(ChessIndex pieceType, ChessIndex destinationType) {
            if (pieceType.IsWhite() && destinationType.IsBlack())
                return true;

            if (pieceType.IsBlack() && destinationType.IsWhite())
                return true;

            return false;
        }
    }

}