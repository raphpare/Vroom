using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class IconeSceneFullScreen : MonoBehaviour
{
    public Sprite ImageLight;
    public Sprite ImageDark;

    void Start()
    {
        this.gameObject.GetComponent<Image>().sprite = Atmosphere.DayMode ? this.ImageDark: this.ImageLight;
    }
}
