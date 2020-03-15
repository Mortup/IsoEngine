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
    [SerializeField] private Sprite goodTile;
    [SerializeField] private Sprite badTile;

    private SpriteRenderer spriteRenderer;
    private Mode currentMode;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        currentMode = Mode.None;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1)) {
            currentMode = Mode.None;
        }

        spriteRenderer.color = currentMode == Mode.None ? Color.clear : Color.white;
        
        Vector2Int tileCoords = level.transformer.ScreenToTile(Input.mousePosition);
        transform.position = level.transformer.MouseTileRounded();
        spriteRenderer.sortingOrder = level.transformer.TileSortingOrder(tileCoords.x, tileCoords.y) + 2;

        spriteRenderer.sprite = IsTilePlaceable(tileCoords.x, tileCoords.y) ? goodTile : badTile;

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
            level.data.SetFloor(tileCoords.x, tileCoords.y, (int)currentMode);

            currentMode = Mode.None;
        }
    }

    public int GetCurrentMode() {
        return (int)currentMode;
    }

    public void SetMode(int newMode) {
        currentMode = (Mode)newMode;
    }

    private bool IsTilePlaceable(int x, int y) {
        if (level.data.IsFloorInBounds(new Vector2Int(x, y)) == false)
            return false;

        int tileData = level.data.GetFloor(x, y);

        if (currentMode == Mode.Path) {
            return tileData == 0;
        }

        return true;
    }
}
