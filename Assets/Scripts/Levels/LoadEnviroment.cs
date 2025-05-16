using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LoadEnviroment : MonoBehaviour
{
    [SerializeField] private string environentAddress = null;
    private GameObject levelGo;

    private void Awake()
    {
        levelGo = GameObject.Find("Level");
        ComponentValidator.CheckAndLog(levelGo, nameof(levelGo), this);

        levelGo.SetActive(false);
        LoadEnviromentFun();
    }

    private void LoadEnviromentFun()
    {
        Addressables.LoadAssetAsync<GameObject>(environentAddress).Completed += OnEnviromentLoaded;
    }

    private void OnEnviromentLoaded(AsyncOperationHandle<GameObject> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            Instantiate(obj.Result);
            levelGo.SetActive(true);
        }
    }
}