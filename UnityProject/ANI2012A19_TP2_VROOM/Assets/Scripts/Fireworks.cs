using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Fireworks : MonoBehaviour
{
    private ParticleSystem FireworksSysteme;

    private void Start()
    {
        this.FireworksSysteme = GetComponent<ParticleSystem>();
    }

    public void Play()
    {
        if (this.FireworksSysteme.isPlaying)
        {
            this.FireworksSysteme.Clear();
        }
        this.FireworksSysteme.Play();
    }
}
