using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    [SerializeField] private GameObject particlesImortalityPrefab;
    [SerializeField]  private GameObject particlesCrashPrefab;

    public GameObject GetParticlesImortalityPrefab()
    {
        return particlesImortalityPrefab;
    }
    public GameObject GetParticlesCrashPrefab()
    {
        return particlesCrashPrefab;
    }


}
