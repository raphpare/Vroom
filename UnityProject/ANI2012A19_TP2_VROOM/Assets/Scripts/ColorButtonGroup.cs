using System.Collections.Generic;
using UnityEngine;

public class ColorButtonGroup : MonoBehaviour
{
    public Car Car;
    public List<ColorButton> ColorButton;

    public void UpdateColorsButtons(int colorId)
    {
        foreach (ColorButton colorButton in this.ColorButton)
        {
            colorButton.ActiveMaterialChange(colorId);
        }
    }

    public void UpdateCarColor(int colorId)
    {
        this.UpdateColorsButtons(colorId);

        this.Car.SetBodyColor(colorId);
    }

    public void ResetToInitalValue()
    {
        this.UpdateColorsButtons(Car.BodyColorId);
    }
}
