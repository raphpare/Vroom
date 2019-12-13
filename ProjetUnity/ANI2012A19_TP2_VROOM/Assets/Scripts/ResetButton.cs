using UnityEngine;

public class ResetButton : MonoBehaviour
{
    public Car Car;
    public CarDisplayDropdown CarDisplayDropdown;
    public ColorButtonGroup ColorButtonGroup;
    public DayNightButton DayNightButton;

    public void ResetGame()
    {
        this.Car.ResetToInitalValue();
        this.CarDisplayDropdown.ResetToInitalValue();
        this.ColorButtonGroup.ResetToInitalValue();
        this.DayNightButton.SetDayLight(true);
    }
}
