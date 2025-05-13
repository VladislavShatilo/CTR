using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnviromentGenerator : MonoBehaviour
{
    [SerializeField] GameObject roadPrefab;
    [SerializeField] float distance;

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(roadPrefab, new Vector3(0, gameObject.transform.position.y, gameObject.transform.position.z + 2 * distance), Quaternion.identity);
        StartCoroutine(DestroyPrev());
    }

    IEnumerator DestroyPrev()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject.transform.parent.GameObject());
            break;
        }
    }
}
