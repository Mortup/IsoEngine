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

                foreach (Vector2Int neighbor in GetNeighbors(level, current, true)) {
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
                if (IsWalkable(level, neighbor)) {
                    result.Add(neighbor);
                }
            }

            return result;
        }

        private static bool IsWalkable(Level level, Vector2Int coords) {
            bool insideBoundaries = level.data.IsFloorInBounds(coords);

            if (insideBoundaries == false)
                return false;

            bool notEmpty = level.data.GetFloor(coords.x, coords.y) != (int)FloorIndex.Empty;
            return notEmpty;
        }

    }

}