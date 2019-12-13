using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ColorButton : MonoBehaviour
{
    public int ColorId;
    public GameObject Color;
    public GameObject ColorButtonGroup;

    private bool _isActive;

    public void ActiveMaterialChange(int colorId)
    {
        this._isActive = this.ColorId == colorId;
        this._drawColor();
    }

    public void OnClick()
    {
        this.ColorButtonGroup.GetComponent<ColorButtonGroup>().UpdateCarColor(this.ColorId);
    }

    private void Start()
    {
        this._drawColor();
    }    

    private void _drawColor()
    {
        this.Color.GetComponent<RectTransform>().sizeDelta = this._isActive ? new Vector2(67.5f, 67.5f) : new Vector2(50, 50);
    }
}
