using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;

    private Vector2 cursorHotSpot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        cursorHotSpot = new Vector2(cursorTexture.width / 2, cursorTexture.height);

        Cursor.SetCursor(cursorTexture, cursorHotSpot, CursorMode.Auto);
    }

}
