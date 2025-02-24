using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMechanism : MonoBehaviour
{
    public GameObject door;
    public Transform closerDoorPosition;
    public Transform openDoorPosition;

    public static DoorMechanism instance;
    public bool doorOpen = false;

    [Header("BathroomDoor")]
    [SerializeField] bool monsterSpawnInBath = true;
    public GameObject cafeMonsterGameObject;

    void Awake()
    {
        instance = this;   
    }


    public void BathroomDoorOpenClose()
    {
        Debug.Log("Is it going here");
        if (doorOpen == true)
        {   
            door.transform.rotation = closerDoorPosition.rotation;
            doorOpen = false;
        }

        else
        {
            door.transform.rotation = openDoorPosition.rotation;
            if (monsterSpawnInBath == true)
            {
                Debug.Log("Monster");   
                cafeMonsterGameObject.SetActive(true);
                monsterSpawnInBath = false;
            }
            doorOpen = true;
            Debug.Log("openDoor");
        }
    }

    // public void NormalDoor()
    // {
    //     if(doorOpen == true)
    //     {
    //         Debug.Log("Closed door");
    //         door.transform.eulerAngles = new Vector3(door.transform.eulerAngles.x, door.transform.eulerAngles.y - 110f, door.transform.eulerAngles.z);
    //         //closerDoorPosition.rotation;
    //         doorOpen = false;
    //     }
    //     else
    //     {
    //         Debug.Log("Open door");
    //         door.transform.rotation = openDoorPosition.rotation;
    //         doorOpen = true; 
    //     }
    // }


}
