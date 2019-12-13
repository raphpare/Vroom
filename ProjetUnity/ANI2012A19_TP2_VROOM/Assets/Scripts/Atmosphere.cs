using UnityEngine;

public class Atmosphere : MonoBehaviour
{
    public Light MainLight;
    public MeshRenderer SkyHorizonMesh;
    public Material DayHorizon;
    public Material NightHorizon;
    public MeshRenderer WaterMesh;
    public Material DayWater;
    public Material NightWater;

    static public bool DayMode = true;


    public void ToggleDayNight()
    {
        this.SetDayMode(!DayMode);
    }

    public void SetDayMode(bool dayMode)
    {
       
        DayMode = dayMode;

        if (DayMode)
        {
            RenderSettings.ambientIntensity = 1.0f;
            RenderSettings.fogColor = new Color(0.682f, 0.839f, 0.984f);
            RenderSettings.fogDensity = 0.003f;
            this.MainLight.intensity = 1.0f;
            this.SkyHorizonMesh.material = this.DayHorizon;
            this.WaterMesh.material = this.DayWater;
        }
        else
        {
            RenderSettings.ambientIntensity = 0.2f;
            RenderSettings.fogColor = new Color(0.0196f, 0.1176f, 0.2f);
            RenderSettings.fogDensity = 0.008f;
            this.MainLight.intensity = 0.2f;
            this.SkyHorizonMesh.material = this.NightHorizon;
            this.WaterMesh.material = this.NightWater;
        }
    }

    private void Start()
    {
        this.SetDayMode(DayMode);
    }
}
