using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class OfficeMonster : MonoBehaviour
{
    public GameObject monster;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            monster.SetActive(false);
            Destroy(this);
        }
    }
}
