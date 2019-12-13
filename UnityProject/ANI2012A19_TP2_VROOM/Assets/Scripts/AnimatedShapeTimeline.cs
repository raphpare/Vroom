using UnityEngine;
using UnityEngine.Playables;


[RequireComponent(typeof(PlayableDirector))]
[RequireComponent(typeof(BoxCollider))]
public class AnimatedShapeTimeline : MonoBehaviour
{
    private PlayableDirector Timeline;

    private void Start()
    {
        this.Timeline = GetComponent<PlayableDirector>();
    }

    private void OnMouseDown()
    {
        if (this.Timeline.state == PlayState.Playing)
        {
            this.Timeline.Pause();
        }
        else
        {
            this.Timeline.Play();
        }
    }

}
