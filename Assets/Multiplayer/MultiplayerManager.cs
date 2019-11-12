using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class MultiplayerManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playerPrefab;

    private CharacterMovement localPlayer;

    private readonly byte RequestPlayerPosition = 1;

    private void Start() {
        JoinRoom();
    }

    public override void OnJoinedRoom() {
        Debug.Log("Joined room!");
        localPlayer = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0f, 0f, 0f), Quaternion.identity, 0).GetComponent<CharacterMovement>();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) {
        Debug.LogFormat("{0} entered the room. Requesting position.", newPlayer.NickName);
        localPlayer.RequestPosition(newPlayer);
    }

    public override void OnJoinRoomFailed(short returnCode, string message) {
        Debug.LogErrorFormat("Couldn't join room with reason {0}", message);
    }

    private void JoinRoom() {
        PhotonNetwork.JoinOrCreateRoom("DebugRoom", new RoomOptions(), new TypedLobby());
    }
}
