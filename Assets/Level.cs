using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [Header("Debug Settings")]
    [SerializeField] private bool loadFromWeb;

    public FloorObervser floorObserver { get; private set; }
    public Transformer transformer { get; private set; }
    public LevelData data { get; private set; }

    private void Awake() {
        Debug.Log("Loading room " + RoomLoader.roomName + "...");

        IRoomSerializer roomSerializer;
        if (loadFromWeb) {
            roomSerializer = new WebSerializer();
        }
        else {
            roomSerializer = new LocalSerializer();
        }

        data = roomSerializer.LoadLevel("1");

        transformer = new Transformer(transform);
        floorObserver = new FloorObervser(this);


    }
    
}
