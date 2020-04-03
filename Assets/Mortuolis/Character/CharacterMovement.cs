using UnityEngine;
using UnityEngine.EventSystems;

using Photon.Pun;
using Photon.Realtime;

using com.mortup.iso;
using com.mortup.iso.pathfinding;
using com.mortup.iso.world;
using com.mortup.city.gamemodes;

public class CharacterMovement : MonoBehaviourPun
{
    [SerializeField] private float speed;

    private Level level;
    private Vector2Int[] currentPath;
    private int currentPathPointIndex;

    private void Awake() {
        level = FindObjectOfType<Level>();

        currentPath = new Vector2Int[0];
        currentPathPointIndex = 0;
    }

    private void Update() {
        HandleMovement();
        HandleNewTarget();
    }

    private void HandleMovement() {
        if (currentPath.Length == 0)
            return;

        Vector2 currentTarget = level.transformer.TileToWorld(currentPath[currentPathPointIndex]);

        bool isCloseToCurrentTarget = (currentTarget - (Vector2)transform.position).magnitude < 0.05f;

        if (isCloseToCurrentTarget) {
            UpdateCurrentTarget();
        }
        else {
            transform.position = transform.position + (Vector3)(currentTarget - (Vector2)transform.position).normalized * Time.deltaTime * speed;
        }
    }

    private void HandleNewTarget() {
        if (PhotonNetwork.IsConnected == true && photonView.IsMine == false)
            return;

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (FindObjectOfType<LivingMode>() != null && FindObjectOfType<LivingMode>().enabled == false) {
            return;
        }

        if (Input.GetMouseButtonDown(0)) {
            Vector2Int startCoords = level.transformer.WorldToTile(transform.position + Vector3.right * 0.5f);
            Vector2Int targetTile = level.transformer.ScreenToTile(Input.mousePosition);
            Vector2Int[] newPath = PathCalculator.FindPath(level, startCoords, targetTile).ToArray();
            photonView.RPC("SetNewPath", RpcTarget.AllViaServer, splitArray(newPath, true), splitArray(newPath, false));
        }
    }

    private void UpdateCurrentTarget() {
        if (currentPath.Length == 0)
            return;

        if (currentPathPointIndex < currentPath.Length - 1) {
            currentPathPointIndex++;
            Vector2Int targetCoords = currentPath[currentPathPointIndex];
        }
    }

    public void RequestInitialInfo(Player requester) {
        Debug.LogFormat("Requesting initial info.");
        photonView.RPC("SetInitialInfo", requester, (Vector2)transform.position, currentPathPointIndex, splitArray(currentPath, true), splitArray(currentPath, false));
    }

    [PunRPC]
    private void SetNewPath(int[] pathX, int[] pathY) {
        currentPathPointIndex = 0;
        currentPath = joinArray(pathX, pathY);
    }

    [PunRPC]
    private void SetInitialInfo(Vector2 position, int pathPos, int[] pathX, int[] pathY) {
        Debug.LogFormat("Received position {0}", position);
        transform.position = position;
        currentPathPointIndex = pathPos;
        currentPath = joinArray(pathX, pathY);

        Debug.Log("Setting path length: " + currentPath.Length);
    }

    private int[] splitArray(Vector2Int[] arr, bool sideX) {
        int[] result = new int[arr.Length];

        for (int i = 0; i < result.Length; i++) {
            result[i] = sideX ? arr[i].x : arr[i].y;
        }

        return result;
    }

    private Vector2Int[] joinArray(int[] x, int[] y) {
        if (x.Length != y.Length) {
            Debug.LogError("Cannot join arrays of different sizes.");
        }

        Vector2Int[] result = new Vector2Int[x.Length];
        for (int i = 0; i < result.Length; i++) {
            result[i] = new Vector2Int(x[i], y[i]);
        }

        return result;
    }
}
