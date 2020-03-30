using UnityEngine;
using TMPro;
using Photon.Pun;

namespace com.mortup.city.multiplayer {

    public class CharacterNickname : MonoBehaviourPun {

        [SerializeField] private TMP_Text text;

        void Start() {
            text.SetText(photonView.Owner.NickName);
        }

    }

}