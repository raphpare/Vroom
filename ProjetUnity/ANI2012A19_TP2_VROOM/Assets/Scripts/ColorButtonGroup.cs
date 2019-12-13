using System.Collections.Generic;
using UnityEngine;

public class ColorButtonGroup : MonoBehaviour
{
    public Car Car;
    public List<ColorButton> ColorButton;

    public void UpdateCarColor(int colorId)
    {
        this._updateColosButtons(colorId);

        this.Car.SetBodyColor(colorId);
    }

    public void ResetToInitalValue()
    {
        this._updateColosButtons(Car.BodyColorId);
    }

    private void Start()
    {
        this._updateColosButtons(Car.BodyColorId);
    }

    private void _updateColosButtons(int colorId)
    {
        foreach (ColorButton colorButton in this.ColorButton)
        {
            colorButton.ActiveMaterialChange(colorId);
        }
    }
}
