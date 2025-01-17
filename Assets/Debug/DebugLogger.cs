﻿using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;

using com.mortup.iso;

public class DebugLogger : MonoBehaviour
{
    [SerializeField] private Level level;

    [Header("Options")]
    [SerializeField] private bool showNickname;
    [SerializeField] private bool showMouseScreen;
    [SerializeField] private bool showMouseWorld;
    [SerializeField] private bool showMouseLocal;
    [SerializeField] private bool showMouseCoordinates;
    [SerializeField] private bool showOrientation;
    [SerializeField] private bool showMouseWallCoordinates;
    [SerializeField] private bool showVertexCoordinates;

    private Text text;

    private void Awake() {
        text = GetComponent<Text>();
    }

    private void OnGUI() {

        string output = "";

        if (showNickname) {
            output += string.Format("Welcome back, {0}!\n", PhotonNetwork.NickName);
        }

        if (showMouseScreen) {
            output += string.Format("Mouse Screen: {0}, {1}\n", Input.mousePosition.x, Input.mousePosition.y);
        }

        if (showMouseWorld) {
            Vector2 world = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            output += string.Format("Mouse World: {0}, {1}\n", world.x, world.y);
        }

        if (showMouseLocal) {
            Vector2 world = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 local = level.transform.InverseTransformPoint(world);
            output += string.Format("Mouse Local: {0}, {1}\n", local.x, local.y);
        }

        if (showMouseCoordinates) {
            Vector2Int mouseCoords = level.transformer.ScreenToTile(Input.mousePosition);
            output += string.Format("Mouse Coords: {0}, {1}\n", mouseCoords.x, mouseCoords.y);
        }

        if (showOrientation) {
            output += string.Format("Orientation: {0}\n", level.transformer.GetOrientation());
        }

        if (showMouseWallCoordinates) {
            Vector3Int wallCoords = level.transformer.ScreenToWall(Input.mousePosition);
            output += string.Format("Wall Coords: {0}, {1}, {2}\n", wallCoords.x, wallCoords.y, wallCoords.z);
        }
        if (showVertexCoordinates) {
            Vector2Int vertexCoords = level.transformer.ScreenToVertex(Input.mousePosition);
            output += string.Format("Vertex Coords: {0}, {1}\n", vertexCoords.x, vertexCoords.y);
        }

        text.text = output;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            Debug.Log("Rotating from DebugLogger");
            level.transformer.RotateClockwise();
        }
    }
}
