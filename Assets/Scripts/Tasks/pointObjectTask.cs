using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointObjectTask : MonoBehaviour
{
    private ParticleSystem[] emitters;

    void Start()
    {
        emitters = GetComponentsInChildren<ParticleSystem>();
    }

    public void OnPlayTask()
    {
        foreach (ParticleSystem p in emitters)
        {
            p.Play();
        }
    }
}