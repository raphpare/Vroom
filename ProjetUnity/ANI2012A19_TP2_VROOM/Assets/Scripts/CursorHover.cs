using UnityEngine;

public class CursorHover : MonoBehaviour
{
    public Texture2D Cusor;

    private CursorMode curModeMouse = CursorMode.Auto;
    private Vector2 hotSpotMouse = Vector2.zero;

    public void OnMouseEnter()
    {
        Cursor.SetCursor(this.Cusor, hotSpotMouse, curModeMouse);
    }

    public void OnMouseExit()
    {
        Cursor.SetCursor(null, hotSpotMouse, curModeMouse);
    }
}
