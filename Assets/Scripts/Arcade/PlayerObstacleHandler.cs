using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerObstacleHandler : MonoBehaviour
{
    [Header("Effects")]
    [SerializeField] private GameObject immortalityParticles;
    [SerializeField] private GameObject crashParticles;
    [SerializeField] private GameObject particlePoint;

    private BuffManager buffManager;
    private ArcadePlayerMovement movement;
    private CameraShakeEffect cameraShake;
    void Start()
    {
        cameraShake = Camera.main.GetComponent<CameraShakeEffect>();
        buffManager = GetComponent<BuffManager>();
        movement = GetComponent<ArcadePlayerMovement>();
        if (movement == null || buffManager == null || cameraShake == null)
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
        cameraShake.TriggerShake();
        if (buffManager.IsBuffActive(BuffType.Immortality))
        {
            HandleImmortalCollision();
            Destroy(other.gameObject);
        }
        else
        {
            StartCoroutine( HandleCrashCollision());
        }
    }


    private void HandleImmortalCollision()
    {
        Instantiate(immortalityParticles, particlePoint.transform);
       
    }

    private IEnumerator HandleCrashCollision()
    {
        MainManager.Instance.SaveCoinsInLevel();

       
        Instantiate(crashParticles, particlePoint.transform);
        movement.DestroyCar();
        ArcadeManager.Instance.SetZeroSpeed();
        yield return new WaitForSeconds(0.2f);
        UIArcadeManager.Instance.Windows.ShowWindow<UIAdvWindow>();
       
    }



}
