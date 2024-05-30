using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanParticleSystem : MonoBehaviour
{
    private ParticleSystem particleSystem;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }
    public void OnOff()
    {
        if (particleSystem.isPlaying)
        {
            particleSystem.Stop();
        }
        else
        {
            particleSystem.Play();
        }
    }
}
