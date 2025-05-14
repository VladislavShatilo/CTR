using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.TestTools;
using UnityEngine.XR;
using YG;

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove Instance { get; private set; }

    [Header("Power")]
    public float powerCar;

    [Header("Car Drive Settings")]
    [SerializeField] private float forwardSpeed = 15f;         // Скорость по Z
    [SerializeField] private float steeringSpeed = 5f;         // Насколько быстро сдвигается по X
    [SerializeField] private float maxSteerOffset = 4f;        // Ограничение по X
    [SerializeField] private float visualTiltZ = 15f;          // Наклон по Z (влево/вправо)
    [SerializeField] private float visualTurnY = 10f;          // Поворот по Y (руль)
    [SerializeField] private float rotationSmooth = 5f;        // Плавность поворотов
    [SerializeField] private float positionSmooth = 5f;

    [Header("Broke Effects")]

    [SerializeField] private GameObject carDestroyEffectPosition;

    private Rigidbody rb;
    private float targetX = 0f;
    private bool isMobile;

    void Awake()
    {
        // Проверка, существует ли уже экземпляр
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Удалить дубликат
            return;
        }

        Instance = this;
       
        // DontDestroyOnLoad(gameObject); // Не уничтожать при смене сцены
    }
    public void addPower(int pwr)
    {
        
        powerCar += pwr;
        if (powerCar <= 0)
        {
            destroyCar();
        }
    }
    public void destroyCar()
    {
        ParticleManager.Instance.CreateCarDestroyEffect(carDestroyEffectPosition.transform.position);
        gameObject.SetActive(false);
        if(UILevelManager.Instance.Windows == null)
        {
            Debug.LogError("UILevelManager.Instance.Windows is null");
            enabled = false;
        }
        var windowManager = UILevelManager.Instance.Windows;
        windowManager.ShowWindow<UILoseLevelWindow>();
        if (UILevelManager.Instance.LevelHUD == null)
        {
            Debug.LogError("UILevelManager.Instance.LevelHUD is null");
            enabled = false;
        }
        var levelHUD = UILevelManager.Instance.LevelHUD;
        int currentPower = levelHUD.GetCurrentPower();
        windowManager.GetWindow<UILoseLevelWindow>().UpdatePowerText(currentPower);
        levelHUD.SetGameplayUI(false);
        FindObjectOfType<PreFinish>().enabled = false;


    }
  




    void Start()
    {
        isMobile = YG2.envir.isMobile;
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
    }
   
    void Update()
    {
        float input;
      
        if (isMobile)
        {
            float targetInput = 0f;
            float inputChangeSpeed = 35f;
            input = 0f;
            if (Input.GetMouseButton(0)) // Зажата левая кнопка мыши (или тап на экране)
            {
                if (Input.mousePosition.x < Screen.width / 2)
                {
                    targetInput = -1f; // Левая часть экрана — едем влево
                }
                else
                {
                    targetInput = 1f;  // Правая часть экрана — едем вправо
                }
            }
            input = Mathf.Lerp(input, targetInput, inputChangeSpeed * Time.deltaTime);
        }
       else
        {
           input = Input.GetAxis("Horizontal"); // A/D или стрелки

            
        }
        targetX += input * steeringSpeed * Time.deltaTime;
        targetX = Mathf.Clamp(targetX, -maxSteerOffset, maxSteerOffset);
        float targetZTilt = -input * visualTiltZ;  // наклон по Z
        float targetYTurn = input * visualTurnY;   // разворот по Y
    


        if (Mathf.Abs(targetX) >= maxSteerOffset - 0.1f) // немного запас по границе
        {

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0, 0), Time.deltaTime * rotationSmooth);
        }
        else
        {
            Quaternion targetRotation = Quaternion.Euler(0f, targetYTurn, targetZTilt);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmooth);
        }

       
    }
   
    void FixedUpdate()
    {
        Vector3 position = rb.position;

        float newX = Mathf.Lerp(position.x, targetX, Time.fixedDeltaTime * positionSmooth);
        Vector3 newPosition = new Vector3(newX, position.y, position.z + forwardSpeed * Time.fixedDeltaTime*powerCar / 90);

        rb.MovePosition(newPosition);
    }
}
