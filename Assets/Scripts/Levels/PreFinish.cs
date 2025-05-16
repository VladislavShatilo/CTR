using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PreFinish : MonoBehaviour
{
    [SerializeField] private float speed;
    private GameObject player;

    private void Start()
    {
        enabled = false;
        ComponentValidator.CheckAndLog(PlayerMove.Instance, nameof(PlayerMove.Instance), this);
        player = PlayerMove.Instance.gameObject;
    }

    private void Update()
    {
        if (!Storage.Instance.isPauseGlobal)
        {
            float x = Mathf.MoveTowards(player.transform.position.x, 0, Time.deltaTime * 15f);
            float z = player.transform.position.z + speed * Time.deltaTime;
            player.transform.SetPositionAndRotation(new Vector3(x, 0, z), Quaternion.Slerp(player.transform.rotation, Quaternion.Euler(0f, 0, 0), Time.deltaTime * 10));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerMove.Instance.enabled = false;
        enabled = true;
    }
}