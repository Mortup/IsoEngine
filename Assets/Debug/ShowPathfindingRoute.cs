using System.Collections.Generic;
using UnityEngine;

using com.mortup.iso.pathfinding;
using com.mortup.iso.world;

namespace com.mortup.iso.debug {

    public class ShowPathfindingRoute : MonoBehaviour {

        [SerializeField] Level level;

        Transform startingPoint;

        private void Update() {
            GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
            if (playerGO == null)
                return;
            startingPoint = playerGO.transform;

            
            Vector2Int goalCoords = level.transformer.ScreenToTile(Input.mousePosition);
            Vector2Int startCoords = level.transformer.WorldToTile(startingPoint.transform.position);

            List<Vector2Int> route = PathCalculator.FindPath(level, startCoords, goalCoords);

            DrawRoute(route);
        }

        private void DrawRoute(List<Vector2Int> route) {
            for (int i = 0; i < route.Count; i++) {
                Vector2Int point = route[i];
                DrawCross(point, Color.red);
            }

        }

        private void DrawCross(Vector2Int coords, Color color) {
            Vector3 center = level.transformer.TileToWorld(coords);
            center += Vector3.right * 0.5f;

            Debug.DrawLine(center - new Vector3(-0.25f, 0.25f), center - new Vector3(0.25f, -0.25f), color);
            Debug.DrawLine(center - new Vector3(0.25f, 0.25f), center - new Vector3(-0.25f, -0.25f), color);
        }

    }

}