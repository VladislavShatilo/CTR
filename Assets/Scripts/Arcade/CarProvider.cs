using UnityEngine;
using UnityEngine.AI;

public class CarProvider : MonoBehaviour
{
    [SerializeField] private GameObject[] cars;
    [SerializeField] private Material[] carsMaterisls;
    [SerializeField] private Renderer[] carRenderer;

    public Material[] Materials => carsMaterisls;
    public GameObject[] Cars => cars;
    public static CarProvider Instance { get; private set; }

    private void Awake()
    {
       
        ComponentValidator.CheckAndLog(cars, nameof(cars), this);
        ComponentValidator.CheckAndLog(carsMaterisls, nameof(carsMaterisls), this);
        ComponentValidator.CheckAndLog(carRenderer, nameof(carRenderer), this);

       
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    private void Start()
    {
        SetCar();
        SetCarColor();
    }
    private void SetCar()
    {
        for (int i = 0; i < cars.Length; ++i)
        {
            if (i == Storage.Instance.selectedCar)
            {
                cars[i].gameObject.SetActive(true);
            }
            else
            {
                cars[i].gameObject.SetActive(false);

            }
        }
    }
    public void SetCarColor()
    {

        Material[] materials = carRenderer[Storage.Instance.selectedCar].materials;
        for (int i = 0; i < materials.Length; i++)
        {
            if (materials[i].name.StartsWith("!"))
            {
                materials[i] = carsMaterisls[Storage.Instance.SelectedColor[Storage.Instance.selectedCar]];
            }
        }
        carRenderer[Storage.Instance.selectedCar].materials = materials;
    }
}