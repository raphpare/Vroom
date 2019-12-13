using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class MainCameraTimeline : MonoBehaviour
{
    static protected bool IsInStartMethodeForFirtTime = true;

    private PlayableDirector timeline;
    
    private void Start()
    {
        this.timeline = GetComponent<PlayableDirector>();
        if (IsInStartMethodeForFirtTime)
        {
            this.timeline.time = 0;
            this.timeline.Play();
            IsInStartMethodeForFirtTime = false;
        } else
        {
            this.timeline.Play();
            this.timeline.time = this.timeline.duration;
        }
    }
}
