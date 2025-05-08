using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerObstacleHandler : MonoBehaviour
{
    public event System.Action OnCrash; // score, multiplier

    [Header("Effects")]
    [SerializeField] private ParticleSystem immortalityParticles;
    [SerializeField] private ParticleSystem crashParticles;

    [Header("References")]
    [SerializeField] private SmoothScore smoothScore;

    private BuffManager buffManager;
    private ArcadePlayerMovement movement;
    private 
    void Start()
    {
        buffManager = GetComponent<BuffManager>();
        movement = GetComponent<ArcadePlayerMovement>();
        if (movement == null || buffManager == null)
        {
            throw new System.NullReferenceException("PlayerObstacleHandler: Missing critical components!");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Obstacle"))
        {
            return;
        }

        if (buffManager.GetImmortality())
        {
            HandleImmortalCollision();
            Destroy(other.gameObject);
        }
        else
        {
            HandleCrashCollision();
        }
    }
  

    private void HandleImmortalCollision()
    {
        immortalityParticles.Play();
    }

    private void HandleCrashCollision()
    {
        MainManager.Instance.SaveCoinsInLevel();

        crashParticles.Play();

        movement.DestroyCar();
        ArcadeManager.Instance.SetZeroSpeed();
        OnCrash.Invoke();
    }

  
    
}
