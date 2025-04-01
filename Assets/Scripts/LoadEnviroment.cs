using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class LoadEnviroment : MonoBehaviour
{
    [SerializeField] private string environentAddress = null;
    private GameObject levelGo;
    private void Awake()
    {
        levelGo = GameObject.Find("Level");
        levelGo.SetActive(false);
        LoadEnviromentFun();
    }

    private void LoadEnviromentFun()
    {
        Addressables.LoadAssetAsync<GameObject>(environentAddress).Completed += OnEnviromentLoaded;
    }
    private void OnEnviromentLoaded(AsyncOperationHandle<GameObject> obj)
    {
        if(obj.Status == AsyncOperationStatus.Succeeded)
        {
            Instantiate(obj.Result);
            levelGo.SetActive(true);
            Debug.Log("Succeded");
        }
        else
        {
            Debug.Log("Not Succeded");

        }

    }

}
