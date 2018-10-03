using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseRoomSummonBoss : MonoBehaviour
{
    public GameObject door;
    public GameObject Boss;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player" ))
        {
            return;
        }

        door.SetActive(true);
        Boss.GetComponent<BossBehavior>().WakeUp();

        GetComponent<Collider>().enabled = false;
    }
}
