using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DayNightButton : MonoBehaviour
{
    static public bool DayMode = true;
    public Car Car;
    public Atmosphere Atmosphere;
    public Sprite DayIcon;
    public Sprite NightIcon;
    public Material Podium;

    public void ToggleDayNight()
    {
        this.SetDayLight(!DayMode);
    }

    public void SetDayLight(bool dayMode)
    {
        DayMode = dayMode;

        Image imageBouton = this.gameObject.GetComponent<Image>();

        if (DayMode)
        {
            imageBouton.sprite = this.NightIcon;
            this.Podium.SetFloat("_GlossMapScale", 0.5f);
        }
        else
        {
            imageBouton.sprite = this.DayIcon;
            this.Podium.SetFloat("_GlossMapScale",0);
        }

        this.Car.SetLights(!DayMode);
        this.Atmosphere.SetDayMode(DayMode);
    }

    private void Start()
    {
        this.SetDayLight(DayMode);
    }
}