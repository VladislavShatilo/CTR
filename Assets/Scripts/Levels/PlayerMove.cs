using UnityEngine;

using YG;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    public static PlayerMove Instance { get; private set; }

    [Header("Car Drive Settings")]
    [SerializeField] private float forwardSpeed = 15f;

    [SerializeField] private float steeringSpeed = 5f;
    [SerializeField] private float maxSteerOffset = 4f;
    [SerializeField] private float visualTiltZ = 15f;
    [SerializeField] private float visualTurnY = 10f;
    [SerializeField] private float rotationSmooth = 5f;
    [SerializeField] private float positionSmooth = 5f;

    [Header("Broke Effects")]
    [SerializeField] private GameObject carDestroyEffectPosition;

    [Header("References")]
    [SerializeField] private InputHandler inputHandler;

    private Rigidbody rb;
    private float targetX = 0f;
    private bool isMobile;
    private float powerCar = 100;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Удалить дубликат
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        isMobile = YG2.envir.isMobile;
        rb = GetComponent<Rigidbody>();
        ComponentValidator.CheckAndLog(rb, nameof(rb), this);

        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
    }

    public void AddPower(int power)
    {
        powerCar += power;
        if (powerCar <= 0)
        {
            DestroyCar();
        }
    }

    public void DestroyCar()
    {
        ComponentValidator.CheckAndLog(ParticleManager.Instance, nameof(ParticleManager.Instance), this);
        ParticleManager.Instance.CreateCarDestroyEffect(carDestroyEffectPosition.transform.position);

        gameObject.SetActive(false);

        ComponentValidator.CheckAndLog(UILevelManager.Instance.Windows, nameof(UILevelManager.Instance.Windows), this);
        var windowManager = UILevelManager.Instance.Windows;
        windowManager.ShowWindow<UILoseLevelWindow>();

        ComponentValidator.CheckAndLog(UILevelManager.Instance.LevelHUD, nameof(UILevelManager.Instance.LevelHUD), this);
        var levelHUD = UILevelManager.Instance.LevelHUD;
        int currentPower = levelHUD.GetCurrentPower();

        windowManager.GetWindow<UILoseLevelWindow>().UpdatePowerText(currentPower);

        levelHUD.SetGameplayUI(false);

        FindObjectOfType<PreFinish>().enabled = false;
    }

    private void Update()
    {
        float input = inputHandler.GetMoveDirectionLevel();
        targetX += input * steeringSpeed * Time.deltaTime;
        targetX = Mathf.Clamp(targetX, -maxSteerOffset, maxSteerOffset);
        float targetZTilt = -input * visualTiltZ;
        float targetYTurn = input * visualTurnY;

        if (Mathf.Abs(targetX) >= maxSteerOffset - 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0, 0), Time.deltaTime * rotationSmooth);
        }
        else
        {
            Quaternion targetRotation = Quaternion.Euler(0f, targetYTurn, targetZTilt);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmooth);
        }

        Vector3 position = rb.position;

        float newX = Mathf.Lerp(position.x, targetX, Time.fixedDeltaTime * positionSmooth);
        Vector3 newPosition = new Vector3(newX, position.y, position.z + forwardSpeed * Time.fixedDeltaTime * powerCar / 90);

        rb.MovePosition(newPosition);
    }
}