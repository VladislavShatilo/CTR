using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField] private int rotationSpeed;
    [SerializeField] private GameObject particlesPrefab;
    [SerializeField] private AudioSource coinSound;

    void Update()
    {
        transform.Rotate(0, Time.deltaTime * rotationSpeed, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody.GetComponent<BuffManager>().GetDoubledCoins())
        {
            FindObjectOfType<MainManager>().AddOneCoinInLevel(2);
        }
        else
        {
            FindObjectOfType<MainManager>().AddOneCoinInLevel(1);
        }

        Destroy(gameObject);
        coinSound.volume = Storage.Instance.volume;
        Instantiate(coinSound, transform.position, transform.rotation);
        Instantiate(particlesPrefab, transform.position, transform.rotation);
    }
}
