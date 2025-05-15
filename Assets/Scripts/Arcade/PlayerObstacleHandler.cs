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

    [Header("Referencies")]
    [SerializeField] private MainManager mainManager;
    [SerializeField] private ArcadeManager arcadeManager;

    [Header("Referencies")]
    [SerializeField] private float crashDelay = 0.2f;


    private BuffManager buffManager;
    private ArcadePlayerMovement movement;
    private CameraShakeEffect cameraShake;

    private void Awake()
    {
        ComponentValidator.CheckAndLog(immortalityParticles, nameof(immortalityParticles), this);
        ComponentValidator.CheckAndLog(crashParticles, nameof(crashParticles), this);
        ComponentValidator.CheckAndLog(particlePoint, nameof(particlePoint), this);
        ComponentValidator.CheckAndLog(mainManager, nameof(mainManager), this);
        ComponentValidator.CheckAndLog(arcadeManager, nameof(arcadeManager), this);
     
    }
    void Start()
    {
       

        cameraShake = Camera.main.GetComponent<CameraShakeEffect>();
        buffManager = GetComponent<BuffManager>();
        movement = GetComponent<ArcadePlayerMovement>();

        ComponentValidator.CheckAndLog(cameraShake, nameof(cameraShake), this);
        ComponentValidator.CheckAndLog(buffManager, nameof(buffManager), this);
        ComponentValidator.CheckAndLog(movement, nameof(movement), this);

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
        mainManager.SaveCoinsInLevel();

        Instantiate(crashParticles, particlePoint.transform);
        movement.DestroyCar();
        arcadeManager.StopRoadMovement();
        yield return new WaitForSeconds(crashDelay);

        ComponentValidator.CheckAndLog(UIArcadeManager.Instance.Windows, nameof(UIArcadeManager.Instance.Windows), this);
        UIArcadeManager.Instance.Windows.ShowWindow<UIAdvWindow>();
       
    }



}
