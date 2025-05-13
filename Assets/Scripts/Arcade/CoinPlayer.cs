using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPlayer : MonoBehaviour
{
    [SerializeField] private BuffManager buffManager;
    [SerializeField] private GameObject particlesPrefab;
    [SerializeField] private AudioSource coinSound;

    private UIArcadeHUDManager hudManager;

    private void Start()
    {
        hudManager = UIManager.Instance.HUD;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            if (buffManager.IsBuffActive(BuffType.DoubleCoins))
            {
                Storage.Instance.coinsInLevel += 2;
                
            }
            else
            {
                Storage.Instance.coinsInLevel++;
            }
            hudManager.UpdateCoins(Storage.Instance.coinsInLevel);
            Destroy(other.gameObject);
            coinSound.volume = Storage.Instance.volume;
            Instantiate(coinSound, transform.position, transform.rotation);
            Instantiate(particlesPrefab, other.gameObject.transform.position, other.gameObject.transform.rotation);
        }
       
    }
}
