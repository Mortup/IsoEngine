using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using com.mortup.iso;

namespace com.mortup.city.multiplayer {

    public class MultiplayerManager : MonoBehaviourPunCallbacks {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Level level;

        private CharacterMovement localPlayer;

        private readonly byte RequestPlayerPosition = 1;

        private void Start() {
            if (PhotonNetwork.IsConnectedAndReady) {
                JoinRoom();
            }
        }

        public override void OnConnectedToMaster() {
            if (PhotonNetwork.InRoom == false) {
                JoinRoom();
            }
        }

        public override void OnJoinedRoom() {
            Debug.Log("Joined room!");
            level.LoadLevel();
            localPlayer = PhotonNetwork.Instantiate(playerPrefab.name, level.transformer.TileToWorld(0,0), Quaternion.identity, 0).GetComponent<CharacterMovement>();
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

}