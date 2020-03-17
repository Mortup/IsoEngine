using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

using com.mortup.iso;

public class CursorMode : MonoBehaviour
{

    private enum Mode {
        None = 0,
        Path = 1,
        House = 2,
        Farm = 3,
    }

    [SerializeField] private Level level;
    [SerializeField] private GameObject cursorPrefab;
    [Header ("Sprites")]
    [SerializeField] private Sprite goodTile;
    [SerializeField] private Sprite badTile;
    [Header("Prices")]
    [SerializeField] private int pathMoneyPrice;
    [SerializeField] private int houseFoodPrice;
    [SerializeField] private int farmMoneyPrice;

    private List<GameObject> cursors;
    private Mode currentMode;

    private void Awake() {
        cursors = new List<GameObject>();
    }

    private void Start() {
        currentMode = Mode.None;
    }

    private void Update() {
        DestroyCursors();
        HandleExitCurrentMode();

        if (currentMode == Mode.None)
            return;
        
        Vector2Int mouseCoords = level.transformer.ScreenToTile(Input.mousePosition);
        bool allTilesPlaceables = ShowCursor(mouseCoords);

        if (currentMode == Mode.Farm) {

            allTilesPlaceables = ShowCursor(mouseCoords + new Vector2Int(0, 1)) && allTilesPlaceables;
            allTilesPlaceables = ShowCursor(mouseCoords + new Vector2Int(0, -1)) && allTilesPlaceables;
            allTilesPlaceables = ShowCursor(mouseCoords + new Vector2Int(1, -1)) && allTilesPlaceables;
            allTilesPlaceables = ShowCursor(mouseCoords + new Vector2Int(1, 0)) && allTilesPlaceables;
            allTilesPlaceables = ShowCursor(mouseCoords + new Vector2Int(1, 1)) && allTilesPlaceables;
            allTilesPlaceables = ShowCursor(mouseCoords + new Vector2Int(-1, -1)) && allTilesPlaceables;
            allTilesPlaceables = ShowCursor(mouseCoords + new Vector2Int(-1, 0)) && allTilesPlaceables;
            allTilesPlaceables = ShowCursor(mouseCoords + new Vector2Int(-1, 1)) && allTilesPlaceables;
        }

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
            if (allTilesPlaceables) {
                switch (currentMode) {
                    case Mode.Path:
                        level.data.SetFloor(mouseCoords.x, mouseCoords.y, (int)currentMode);
                        CityBuilderManager.coins -= pathMoneyPrice;
                        break;
                    case Mode.House:
                        level.data.SetFloor(mouseCoords.x, mouseCoords.y, (int)currentMode);
                        CityBuilderManager.food -= houseFoodPrice;
                        break;
                    case Mode.Farm:
                        level.data.SetFloor(mouseCoords.x+1, mouseCoords.y+1, 4);
                        level.data.SetFloor(mouseCoords.x+1, mouseCoords.y, 4);
                        level.data.SetFloor(mouseCoords.x+1, mouseCoords.y-1, 4);
                        level.data.SetFloor(mouseCoords.x-1, mouseCoords.y, 4);
                        level.data.SetFloor(mouseCoords.x-1, mouseCoords.y+1, 4);
                        level.data.SetFloor(mouseCoords.x, mouseCoords.y+1, 4);
                        level.data.SetFloor(mouseCoords.x, mouseCoords.y-1, 4);
                        level.data.SetFloor(mouseCoords.x, mouseCoords.y, 4);

                        level.data.SetFloor(mouseCoords.x-1, mouseCoords.y-1, (int)currentMode);
                        CityBuilderManager.coins -= farmMoneyPrice;
                        break;
                }

                currentMode = Mode.None;
            }
            else {
                // Play wrong sound or something like that.
            }
        }
    }

    public int GetCurrentMode() {
        return (int)currentMode;
    }

    public void SetMode(int newMode) {
        currentMode = (Mode)newMode;
    }

    private bool IsTilePlaceable(int x, int y) {
        // Check if there's enough resources.
        switch(currentMode) {
            case Mode.Path:
                if (CityBuilderManager.coins < pathMoneyPrice)
                    return false;
                break;
            case Mode.House:
                if (CityBuilderManager.food < houseFoodPrice)
                    return false;
                break;
            case Mode.Farm:
                if (CityBuilderManager.coins < farmMoneyPrice)
                    return false;
                break;
        }

        // Check if tile is good.
        if (level.data.IsFloorInBounds(new Vector2Int(x, y)) == false)
            return false;

        int tileData = level.data.GetFloor(x, y);

        if (currentMode == Mode.House) {
            bool nextToPath = false;
            if (level.data.IsFloorInBounds(new Vector2Int(x + 1, y))) {
                nextToPath = nextToPath || level.data.GetFloor(x + 1, y) == 1;
            }
            if (level.data.IsFloorInBounds(new Vector2Int(x - 1, y))) {
                nextToPath = nextToPath || level.data.GetFloor(x - 1, y) == 1;
            }
            if (level.data.IsFloorInBounds(new Vector2Int(x, y+1))) {
                nextToPath = nextToPath || level.data.GetFloor(x, y+1) == 1;
                if (level.data.IsFloorInBounds(new Vector2Int(x, y-1))) {
                    nextToPath = nextToPath || level.data.GetFloor(x, y-1) == 1;
                }
            }

            if (nextToPath == false)
                return false;
        }

        return tileData == 0;
    }

    private void HandleExitCurrentMode() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            currentMode = Mode.None;
        }
    }

    private void DestroyCursors() {
        for (int i = cursors.Count-1; i >= 0; i--) {
            SimplePool.Despawn(cursors[i]);
        }

        cursors = new List<GameObject>();
    }

    private bool ShowCursor(Vector2Int coords) {
        Vector3 position = level.transformer.TileToWorld(coords);
        GameObject cursor = SimplePool.Spawn(cursorPrefab, position, Quaternion.identity);
        cursors.Add(cursor);
        SpriteRenderer spriteRenderer = cursor.GetComponent<SpriteRenderer>();

        bool isPlaceable = IsTilePlaceable(coords.x, coords.y);
        spriteRenderer.sprite = isPlaceable ? goodTile : badTile;
        spriteRenderer.sortingOrder = level.transformer.TileSortingOrder(coords.x, coords.y) + 2;

        return isPlaceable;
    }
}
