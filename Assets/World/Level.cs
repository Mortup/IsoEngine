using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.mortup.iso.serialization;

public class Level : MonoBehaviour
{
    public static Level main { get; private set; } // TODO: Delete this

    public FloorObserver floorObserver { get; private set; }
    public Transformer transformer { get; private set; }
    public LevelData data { get; private set; }

    private IRoomSerializer roomSerializer;

    private void Awake() {
        transformer = new Transformer(transform);

        roomSerializer = GetComponent<IRoomSerializer>();
        if (roomSerializer == null) {
            roomSerializer = gameObject.AddComponent<NullRoomSerializer>();
        }

        data = roomSerializer.LoadLevel("2");

        floorObserver = new FloorObserver(this);


        if (main == null) {
            main = this;
        }
        else {
            Debug.LogError("Multiple levels, should do something about it.");
        }
    }


    private void Update() {
        if (Input.GetKeyDown(KeyCode.S)) {
            roomSerializer.SaveLevel(data);
        }
    }

}
