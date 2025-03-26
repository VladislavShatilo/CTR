using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PreFinish : MonoBehaviour
{
    [SerializeField] float speed;
    private GameObject player;
    private void Start()
    {
        enabled = false;
        player = PlayerMove.Instance.gameObject;

    }

    private void Update()
    {
        float x = Mathf.MoveTowards(player.transform.position.x, 0, Time.deltaTime * 15f);
        float z = player.transform.position.z + speed * Time.deltaTime;
        player.transform.position = new Vector3(x, 0, z);
        player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.Euler(0f, 0, 0), Time.deltaTime * 10);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerMove.Instance.enabled = false;
        enabled = true;
    }
}
