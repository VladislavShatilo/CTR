using UnityEngine;

public class ProtectionBuff : MonoBehaviour
{
    [SerializeField] private bool isProtection;
    private BuffManager buffManager;

    private void OnTriggerEnter(Collider other)
    {
        buffManager = other.attachedRigidbody.GetComponent<BuffManager>();

        if (buffManager != null)
        {
            if (isProtection)
            {
                buffManager.ActivateBuff(BuffType.Immortality);
            }
            else
            {
                buffManager.ActivateBuff(BuffType.DoubleCoins);
            }
            Destroy(gameObject);
        }
    }
}