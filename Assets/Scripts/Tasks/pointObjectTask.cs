using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class pointObjectTask : MonoBehaviour, ITask
{
    private ParticleSystem[] emitters;

    void Start()
    {
        emitters = GetComponentsInChildren<ParticleSystem>();
    }

    public void SetupTask(ARPlane currentPlane)
    {
        Debug.Log("Setup");
    }

    public void PlayTask()
    {
        foreach (ParticleSystem p in emitters)
        {
            p.Play();
        }
    }
    public void Clean()
    {
        Debug.Log("Cleaning");
    }
}