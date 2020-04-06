using System.Collections.Generic;

using UnityEngine;
using com.mortup.iso.utils;
using com.mortup.iso.world;

namespace com.mortup.iso.pathfinding {

    public class PathCalculator {

        public static List<Vector2Int> FindPath(Level level, Vector2Int start, Vector2Int goal) {
            // Don't calculate unreachable paths.
            if (IsWalkable(level, start) == false || IsWalkable(level, goal) == false) {
                return new List<Vector2Int>();
            }

            // Initial set.
            List<Vector2Int> openSet = new List<Vector2Int>();
            openSet.Add(start);

            // Ready set.
            List<Vector2Int> closedSet = new List<Vector2Int>();

            // For reconstructing the path.
            Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();

            // gScores are the total costs.
            var gScores = new Dictionary<Vector2Int, float>().WithDefaultValue(Mathf.Infinity);
            gScores[start] = 0;

            // fScores are the current best guess.
            // Calculated gScore + cartesianDistance.
            var fScore = new Dictionary<Vector2Int, float>().WithDefaultValue(Mathf.Infinity);
            fScore[start] = Vector2Int.Distance(start, goal);

            Vector2Int current = Vector2Int.zero;
            while (openSet.Count > 0) {
                current = openSet[0];
                foreach (Vector2Int node in openSet) {
                    if (fScore[node] < fScore[current]) {
                        current = node;
                    }
                }

                if (current == goal) {
                    return ReconstructPath(cameFrom, current);
                }

                closedSet.Add(current);
                openSet.Remove(current);

                foreach (Vector2Int neighbor in GetNeighbors(level, current, false)) {
                    float tentativeGScore = gScores[current] + DScore(current, neighbor);
                    if (tentativeGScore < gScores[neighbor]) {
                        cameFrom[neighbor] = current;
                        gScores[neighbor] = tentativeGScore;
                        fScore[neighbor] = gScores[neighbor] + Vector2Int.Distance(neighbor, goal);
                        if (closedSet.Contains(neighbor) == false) {
                            openSet.Add(neighbor);
                        }
                    }
                }
            }

            return new List<Vector2Int>();
        }

        private static List<Vector2Int> ReconstructPath(Dictionary<Vector2Int, Vector2Int> cameFrom, Vector2Int current) {
            List<Vector2Int> totalPath = new List<Vector2Int>();
            totalPath.Add(current);

            while (cameFrom.ContainsKey(current)) {
                current = cameFrom[current];
                totalPath.Insert(0, current);
            }

            return totalPath;
        }

        private static float DScore(Vector2Int from, Vector2Int to) {
            return 1f;
        }

        private static List<Vector2Int> GetNeighbors(Level level, Vector2Int center, bool includeDiagonals) {
            List<Vector2Int> result = new List<Vector2Int>();

            List<Vector2Int> neighbors = new List<Vector2Int>();
            neighbors.Add(center + Vector2Int.up);
            neighbors.Add(center + Vector2Int.down);
            neighbors.Add(center + Vector2Int.left);
            neighbors.Add(center + Vector2Int.right);

            if (includeDiagonals) {
                neighbors.Add(center + Vector2Int.up + Vector2Int.left);
                neighbors.Add(center + Vector2Int.up + Vector2Int.right);
                neighbors.Add(center + Vector2Int.down + Vector2Int.left);
                neighbors.Add(center + Vector2Int.down + Vector2Int.right);
            }

            foreach (Vector2Int neighbor in neighbors) {
                if (IsWalkable(level, neighbor, center)) {
                    result.Add(neighbor);
                }
            }

            return result;
        }

        private static bool IsWalkable(Level level, Vector2Int destination) {
            // Is destination inside boundaries?
            bool insideBoundaries = level.data.IsFloorInBounds(destination);
            if (insideBoundaries == false)
                return false;

            // Is the floor walkable?
            bool isFloorWalkable = true;
            GameObject floorGO = level.GetFloorGameObject(destination);
            WalkableState floorWalkableState = floorGO.GetComponent<WalkableState>();
            if (floorWalkableState == null) {
                isFloorWalkable = level.data.GetFloor(destination.x, destination.y) != (int)FloorIndex.Empty;
            }
            else {
                isFloorWalkable = floorWalkableState.isWalkable;
            }

            if (isFloorWalkable == false)
                return false;

            // Is the item walkable?
            bool isItemWalkable = true;
            GameObject itemGO = level.GetItemGameObject(destination);
            WalkableState itemWalkableState = itemGO.GetComponent<WalkableState>();
            if (itemWalkableState == null) {
                isItemWalkable = level.data.GetItem(destination).x == (int)ItemIndex.Empty;
            }
            else {
                isItemWalkable = itemWalkableState.isWalkable;
            }

            return isItemWalkable;
        }

        private static bool IsWalkable(Level level, Vector2Int destination, Vector2Int origin) {
            bool isTileWalkable = IsWalkable(level, destination);

            if (isTileWalkable == false)
                return false;

            Vector3Int wallBetweenTiles = GetWallBetweenTiles(origin, destination);
            GameObject wallGo = level.GetWallGameObject(wallBetweenTiles);
            WalkableState wallWalkableState = wallGo.GetComponent<WalkableState>();
            if (wallWalkableState == null) {
                Debug.LogFormat("Wall {0},{1},{2} is {3}", wallBetweenTiles.x, wallBetweenTiles.y, wallBetweenTiles.z, level.data.GetWall(wallBetweenTiles) == (int)WallIndex.Empty);
                return level.data.GetWall(wallBetweenTiles) == (int)WallIndex.Empty;
            }
            else {
                return wallWalkableState.isWalkable;
            }
        }

        private static Vector3Int GetWallBetweenTiles(Vector2Int origin, Vector2Int destination) {
            if (Vector2Int.Distance(origin, destination) > 1f) {
                Debug.LogError("Wall can only be found between adjacent tiles.");
                return Vector3Int.zero;
            }

            if (Mathf.Abs(origin.x - destination.x) == 1) {
                return new Vector3Int(Mathf.Max(origin.x, destination.x), origin.y + 1, 0);
            }
            else {
                return new Vector3Int(origin.x, Mathf.Max(origin.y, destination.y), 1);
            }
        }

    }

}