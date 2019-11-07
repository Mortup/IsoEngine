using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [Header("Debug Settings")]
    [SerializeField] private bool loadFromWeb;

    public FloorObserver floorObserver { get; private set; }
    public Transformer transformer { get; private set; }
    public LevelData data { get; private set; }

    private IRoomSerializer roomSerializer;

    private void Awake() {
        Debug.Log("Loading room " + RoomLoader.roomName + "...");

        if (loadFromWeb) {
            roomSerializer = new WebSerializer();
        }
        else {
            roomSerializer = new LocalSerializer();
        }

        data = roomSerializer.LoadLevel(RoomLoader.roomName);

        transformer = new Transformer(transform);
        floorObserver = new FloorObserver(this);


    }


    private void Update() {
        if (Input.GetKeyDown(KeyCode.S)) {
            roomSerializer.SaveLevel(data);
        }
    }

}
