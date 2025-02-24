using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.timeStart = true;
            this.gameObject.SetActive(false);
        }
    }
}
