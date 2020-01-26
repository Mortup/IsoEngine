using UnityEngine;
using UnityEngine.EventSystems;

using Photon.Pun;
using Photon.Realtime;

using com.mortup.iso;

public class CharacterMovement : MonoBehaviourPun, IPunObservable
{
    [SerializeField] private float speed;

    private Level level;
    private Vector2 target;

    private void Start() {
        level = FindObjectOfType<Level>();

        if (photonView.IsMine == true || PhotonNetwork.IsConnected == false) {
            target = transform.position;
        }
    }

    private void Update() {
        if ((target - (Vector2)transform.position).magnitude > 0.05f) {
            transform.position = transform.position + (Vector3)(target - (Vector2)transform.position).normalized * Time.deltaTime * speed;
        }

        if (PhotonNetwork.IsConnected == true && photonView.IsMine == false)
            return;

        // Input handling.
        // Don't move if clicking on UI elements.
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(0)) {
            Vector2Int targetTile = level.transformer.ScreenToTile(Input.mousePosition);
            target = level.transformer.TileToWorld(targetTile);
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
        if (stream.IsWriting) {
            stream.SendNext(target);
        }
        else {
            target = (Vector2)stream.ReceiveNext();
        }
    }
}
