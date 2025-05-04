using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ProtectionBuff : MonoBehaviour
{
    [SerializeField] private bool isProtection;
    private BuffManager playerBuffs;

    private void OnTriggerEnter(Collider other)
    {
        playerBuffs = other.attachedRigidbody.GetComponent<BuffManager>();

        if (playerBuffs)
        {
            if (isProtection)
            {
                playerBuffs.SetImmortality();
            }
            else
            {
                playerBuffs.SetDoubleCoins();
            }
            Destroy(gameObject);
        }
    }
}
