using UnityEngine;
using UnityEngine.EventSystems;

using Photon.Pun;
using Photon.Realtime;

using com.mortup.iso;
using com.mortup.iso.pathfinding;
using com.mortup.iso.world;
using com.mortup.city.gamemodes;

public class CharacterMovement : MonoBehaviourPun, IPunObservable
{
    [SerializeField] private float speed;

    private Level level;
    private Vector2Int[] currentPath;
    private int currentPathPointIndex;

    private void Start() {
        level = FindObjectOfType<Level>();

        if (photonView.IsMine == true || PhotonNetwork.IsConnected == false) {
            currentPath = new Vector2Int[0];
            currentPathPointIndex = 0;
        }
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
            currentPath = PathCalculator.FindPath(level, startCoords, targetTile).ToArray();
            currentPathPointIndex = 0;
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

    public void RequestPosition(Player requester) {
        Debug.LogFormat("Requesting position. Sending {0}", (Vector2)transform.position);
        photonView.RPC("SetPosition", requester, (Vector2)transform.position);
    }

    [PunRPC]
    private void SetPosition(Vector2 position) {
        Debug.LogFormat("Received position {0}", position);
        transform.position = position;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        /**
        if (stream.IsWriting) {
            stream.SendNext(currentTarget);
        }
        else {
            currentTarget = (Vector2)stream.ReceiveNext();
        }
    **/
    }
}
