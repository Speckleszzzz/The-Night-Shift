using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    public Camera mainCamera;
    private Ray ray;
    private RaycastHit hit;
    public float maxDistanceOfRay;
    public GameObject handPosition;
    public GameObject playerCapsule;
    [SerializeField] GameObject objectInHand;

    [SerializeField] bool canGrab = true;
    [SerializeField] string grabbing;

    [Header("Mop")]
    public bool mopInHand = false;

    [Header("WorkPlace")]
    [SerializeField] int documentStored;
    public GameObject officeCollider;
    public GameObject officeMonsterGameObject;
    public bool OfficeMonster = true;

    [Header("Cafe Place")]
    [SerializeField] int rottenFoodThrownCount;
    [SerializeField] int CafeStains;
    public GameObject cafeMonsterGameObject;
    public bool cafeMonster = true;

    [Header("Corridor Place")]
    [SerializeField] int corridorStains;

    [Header("Ui Canvas for Sticky Notes")]
    public GameObject notes1;
    public GameObject notes2;
    public GameObject notes3;
    public GameObject notes4;

    [Header("Bathroom Toilet Paper")]
    public List<GameObject> toiletPaperTemp;
    [SerializeField] int toiletPaperCount;

    [Header("GameOver")]
    [SerializeField] int gameOverScore;

    [Header("Creepy Laugh")]
    public AudioSource creepySmile;

    [Header("Exit Door")]
    [SerializeField] bool canRunAway = false;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float fadeDuration = 8f;



    public GameObject closeDoor;
    public GameObject openDoor;
    [SerializeField] bool monsterSpawnInBath = true;
    public GameObject bathRoomMonster;


    void Update()
    {
        ray = mainCamera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));

        if (canGrab)
        {
            PleaseGrab();
        }
        else
        {
            CantGrab();
        }
        if (objectInHand != null)
        {
            if (mopInHand != true)
            {
                objectInHand.transform.position = Vector3.Lerp(objectInHand.transform.position, handPosition.transform.position, 50f * Time.deltaTime);
            }
            else
            {
                objectInHand.transform.position = Vector3.Lerp(objectInHand.transform.position, new Vector3(handPosition.transform.position.x, handPosition.transform.position.y - 2f, handPosition.transform.position.z), 50f * Time.deltaTime);
            }

            objectInHand.transform.rotation = Quaternion.Slerp(objectInHand.transform.rotation, playerCapsule.transform.rotation, 15f * Time.deltaTime);
        }

        if (Physics.Raycast(ray, out hit, maxDistanceOfRay))
        {
            if (hit.point != null)
            {
                if (hit.transform.tag == "BathroomDoorClose")
                {
                    Debug.Log("looking at bathroom Door");
                    GameManager.instance.interactText.SetActive(true);
                    GameManager.instance.interactText.GetComponent<TextMeshProUGUI>().text = "Press E to interact with door";
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Debug.Log("Yes");
                        if (monsterSpawnInBath == true)
                        {
                            Debug.Log("Monster");
                            bathRoomMonster.SetActive(true);
                            monsterSpawnInBath = false;
                        }
                        openDoor.SetActive(false);
                        closeDoor.SetActive(true);
                        
                        

                        // DoorMechanism.instance.BathroomDoorOpenClose();
                        // if (cafeMonster == true)
                        // {
                        //     cafeMonsterGameObject.SetActive(true);
                        //     cafeMonster = false;
                        // }
                    }

                }

                if (hit.transform.tag == "BathroomDoorOpen")
                {
                    Debug.Log("looking at bathroom Door");
                    GameManager.instance.interactText.SetActive(true);
                    GameManager.instance.interactText.GetComponent<TextMeshProUGUI>().text = "Press E to interact with door";
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        openDoor.SetActive(true);
                        closeDoor.SetActive(false);

                        

                        //DoorMechanism.instance.BathroomDoorOpenClose();
                        // if (cafeMonster == true)
                        // {
                        //     cafeMonsterGameObject.SetActive(true);
                        //     cafeMonster = false;
                        // }
                    }

                }


                if (hit.transform.tag == "exitDoor")
                {
                    Debug.Log("Looking at Exit Door");
                    if (canRunAway == false)
                    {
                        GameManager.instance.interactText.SetActive(true);
                        GameManager.instance.interactText.GetComponent<TextMeshProUGUI>().text = "Finish your tasks";
                    }

                    else if (canRunAway != false)
                    {
                        GameManager.instance.interactText.SetActive(true);
                        GameManager.instance.interactText.GetComponent<TextMeshProUGUI>().text = "Press E to Leave Shift";

                        if (Input.GetKey(KeyCode.E))
                        {
                            FadeIn();
                            GameManager.instance.timeCount = 100000f;
                            GameManager.instance.timerText.SetActive(false);
                        }
                    }
                }



                else if (hit.transform.tag == "notes1")
                {
                    notes1.SetActive(true);
                }

                else if (hit.transform.tag == "notes2")
                {
                    notes2.SetActive(true);
                }

                else if (hit.transform.tag == "notes3")
                {
                    notes3.SetActive(true);
                }

                else if (hit.transform.tag == "notes4")
                {
                    notes4.SetActive(true);
                }
            }
        }

        else
        {
            notes1.SetActive(false);
            notes2.SetActive(false);
            notes3.SetActive(false);
            notes4.SetActive(false);
        }
    }

    void PleaseGrab()
    {
        if (Physics.Raycast(ray, out hit, maxDistanceOfRay))
        {

            if (hit.point != null)
            {
                if (hit.transform.tag == "doc")
                {
                    if (mopInHand == false)
                    {
                        GameManager.instance.interactText.SetActive(true);
                        GameManager.instance.interactText.GetComponent<TextMeshProUGUI>().text = "Press E to Grab Document";
                        Debug.Log("Documents");

                        if (Input.GetKey(KeyCode.E))
                        {
                            objectInHand = hit.transform.gameObject;
                            grabbing = "Work Document";
                            GameManager.instance.workCabin.GetComponent<Outline>().enabled = true;

                            foreach (GameObject docs in GameManager.instance.CurrentWorkDocuments)
                            {
                                docs.GetComponent<Outline>().enabled = false;
                            }
                            if (OfficeMonster == true)
                            {
                                officeMonsterGameObject.SetActive(true);
                                officeCollider.SetActive(true);
                                OfficeMonster = false;
                            }
                            canGrab = false;
                        }
                    }

                }

                else if (hit.transform.tag == "rottenFood")
                {
                    if (mopInHand == false)
                    {
                        GameManager.instance.interactText.SetActive(true);
                        GameManager.instance.interactText.GetComponent<TextMeshProUGUI>().text = "Press E to grab rotten Food";
                        Debug.Log("Roten Food");

                        if (Input.GetKey(KeyCode.E))
                        {
                            objectInHand = hit.transform.gameObject;
                            grabbing = "Rotten Food";
                            GameManager.instance.cafeDustbin.GetComponent<Outline>().enabled = true;

                            objectInHand.GetComponent<BoxCollider>().enabled = false;

                            foreach (GameObject food in GameManager.instance.currentRotenFood)
                            {
                                food.GetComponent<Outline>().enabled = false;
                            }

                            canGrab = false;
                        }
                    }

                }

                else if (hit.transform.tag == "Mop")
                {
                    if (mopInHand == false)
                    {
                        GameManager.instance.interactText.SetActive(true);
                        GameManager.instance.interactText.GetComponent<TextMeshProUGUI>().text = "Press E to grab Mop";
                        Debug.Log("Mop");

                        if (Input.GetKey(KeyCode.E))
                        {
                            objectInHand = hit.transform.gameObject;
                            objectInHand.GetComponent<BoxCollider>().enabled = false;
                            grabbing = "Mop In Hand";
                            GameManager.instance.tempMop.SetActive(true);

                            if (GameManager.instance.currentCafeStain.Count > 0)
                            {
                                foreach (GameObject cafeStains in GameManager.instance.cafeStains)
                                {
                                    Debug.Log("Stains");
                                    cafeStains.GetComponent<Outline>().enabled = true;
                                }
                            }
                            else
                            {
                                Debug.Log("Nothing there");
                            }
                            canGrab = false;
                            mopInHand = true;
                        }
                    }
                }

                else if (hit.transform.tag == "toiletPaper")
                {
                    if (mopInHand == false)
                    {
                        GameManager.instance.interactText.SetActive(true);
                        GameManager.instance.interactText.GetComponent<TextMeshProUGUI>().text = "Press E to grab Toilet Paper";
                        Debug.Log("Toilet Paper");

                        if (Input.GetKey(KeyCode.E))
                        {
                            objectInHand = hit.transform.gameObject;
                            objectInHand.GetComponent<BoxCollider>().enabled = false;
                            grabbing = "toilet Paper In Hand";

                            for (int i = 0; i < toiletPaperTemp.Count; i++)
                            {
                                // toiletPaperTemp[i].SetActive(true);
                                toiletPaperTemp[i].GetComponent<Outline>().enabled = true;
                                toiletPaperTemp[i].GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineVisible;
                                toiletPaperTemp[i].tag = "toiletPaperTemp";
                            }
                            canGrab = false;
                        }

                    }
                }

                else
                {
                    Debug.Log("Is this working");
                    GameManager.instance.interactText.SetActive(false);
                    GameManager.instance.interactText.GetComponent<TextMeshProUGUI>().text = "";
                }
            }
        }
        else
        {

            Debug.Log("Is this working");
            GameManager.instance.interactText.SetActive(false);
            GameManager.instance.interactText.GetComponent<TextMeshProUGUI>().text = "";
        }

    }


    void CantGrab()
    {
        if (Physics.Raycast(ray, out hit, maxDistanceOfRay))
        {
            if (hit.transform.tag == "workCabinate")
            {
                if (grabbing == "Work Document")
                {
                    GameManager.instance.interactText.SetActive(true);
                    GameManager.instance.interactText.GetComponent<TextMeshProUGUI>().text = "Press E to Place Document";
                    Debug.Log("Cabin");

                    if (Input.GetKey(KeyCode.E))
                    {

                        documentStored += 1;
                        UpdateOrganizeTaskCount("Organize File");

                        GameManager.instance.workCabin.GetComponent<Outline>().enabled = false;
                        GameManager.instance.CurrentWorkDocuments.Remove(objectInHand);
                        foreach (GameObject docs in GameManager.instance.CurrentWorkDocuments)
                        {
                            docs.GetComponent<Outline>().enabled = true;
                        }

                        Destroy(objectInHand);
                        objectInHand = null;
                        CheckTask("Organize File");
                        canGrab = true;
                    }
                }
            }

            if (hit.transform.tag == "toiletPaperTemp")
            {
                if (grabbing == "toilet Paper In Hand")
                {
                    GameManager.instance.interactText.SetActive(true);
                    GameManager.instance.interactText.GetComponent<TextMeshProUGUI>().text = "Press F to place the Toilet Paper";
                    Debug.Log("Toilet paper on Roll");

                    if (Input.GetKey(KeyCode.F))
                    {
                        toiletPaperCount += 1;
                        GameObject temp = objectInHand;
                        objectInHand = null;
                        temp.GetComponent<BoxCollider>().enabled = false;
                        temp.GetComponent<Outline>().enabled = false;
                        temp.transform.position = hit.transform.position;
                        toiletPaperTemp.Remove(temp);

                        for (int i = 0; i < toiletPaperTemp.Count; i++)
                        {
                            toiletPaperTemp[i].GetComponent<Outline>().enabled = false;
                        }

                        UpdateOrganizeTaskCount("Fit Toilet Paper");
                        CheckTask("Fit Toilet Paper");
                        canGrab = true;
                    }
                }
            }

            if (hit.transform.tag == "cafeDustbin")
            {
                if (grabbing == "Rotten Food")
                {
                    GameManager.instance.interactText.SetActive(true);
                    GameManager.instance.interactText.GetComponent<TextMeshProUGUI>().text = "Press E to Throw waste";
                    Debug.Log("Dustbin");

                    if (Input.GetKey(KeyCode.E))
                    {
                        rottenFoodThrownCount += 1;
                        UpdateOrganizeTaskCount("Food Throw");

                        GameManager.instance.cafeDustbin.GetComponent<Outline>().enabled = false;
                        GameManager.instance.currentRotenFood.Remove(objectInHand);
                        foreach (GameObject food in GameManager.instance.currentRotenFood)
                        {
                            food.GetComponent<Outline>().enabled = true;
                        }

                        Destroy(objectInHand);
                        objectInHand = null;
                        CheckTask("Food Throw");
                        canGrab = true;
                    }
                }

            }

            if (hit.transform.tag == "MopTemp")
            {
                if (mopInHand)
                {
                    GameManager.instance.interactText.SetActive(true);
                    GameManager.instance.interactText.GetComponent<TextMeshProUGUI>().text = "Press F to return Mop";
                    Debug.Log("Mop Placement");

                    if (Input.GetKey(KeyCode.F))
                    {
                        objectInHand = null;
                        GameManager.instance.mop.transform.position = GameManager.instance.tempMop.transform.position;
                        GameManager.instance.tempMop.SetActive(false);

                        if (GameManager.instance.currentCafeStain.Count > 0)
                        {
                            foreach (GameObject cafeStains in GameManager.instance.cafeStains)
                            {
                                cafeStains.GetComponent<Outline>().enabled = false;
                            }
                        }
                        else
                        {
                            Debug.Log("Nothing there");
                        }

                        GameManager.instance.mop.GetComponent<BoxCollider>().enabled = true;
                        grabbing = "";
                        canGrab = true;
                        mopInHand = false;
                    }
                }
            }


            if (hit.transform.tag == "cafeStain")
            {
                if (mopInHand)
                {
                    GameManager.instance.interactText.SetActive(true);
                    GameManager.instance.interactText.GetComponent<TextMeshProUGUI>().text = "Left Click to Clean Stain";
                    Debug.Log("Clean Cafe Stain");

                    if (Input.GetKey(KeyCode.Mouse0))
                    {
                        GameManager.instance.cafeStainTemp.Remove(0);
                        Destroy(hit.transform.gameObject);
                        CafeStains += 1;
                        UpdateOrganizeTaskCount("Cafe Cleaning");
                        CheckTask("Cafe Cleaning");
                    }
                }
            }

            if (hit.transform.tag == "corridorStain")
            {
                if (mopInHand)
                {
                    GameManager.instance.interactText.SetActive(true);
                    GameManager.instance.interactText.GetComponent<TextMeshProUGUI>().text = "Left Click to Clean Stain";
                    Debug.Log("Clean Corridor Stain");

                    if (Input.GetKey(KeyCode.Mouse0))
                    {
                        GameManager.instance.corridorStainTemp.Remove(0);
                        Destroy(hit.transform.gameObject);
                        corridorStains += 1;
                        UpdateOrganizeTaskCount("Floor Cleaning");
                        CheckTask("Floor Cleaning");
                    }
                }
            }


            else
            {
                //GameManager.instance.interactText.SetActive(false);
                //GameManager.instance.interactText.GetComponent<TextMeshProUGUI>().text = "";
            }
        }

        else
        {
            GameManager.instance.interactText.SetActive(false);
            GameManager.instance.interactText.GetComponent<TextMeshProUGUI>().text = "";
        }
    }

    void CheckTask(string name)
    {
        foreach (GameObject task in GameManager.instance.taskTemplate)
        {
            if (task.name == "Organize File" && task.name == name)
            {
                if (documentStored == 3)
                {
                    task.transform.GetChild(1).gameObject.SetActive(false);
                    task.transform.GetChild(2).gameObject.SetActive(true);
                    gameOverScore += 1;
                }
            }

            else if (task.name == "Food Throw" && task.name == name)
            {
                if (rottenFoodThrownCount == 6)
                {
                    task.transform.GetChild(1).gameObject.SetActive(false);
                    task.transform.GetChild(2).gameObject.SetActive(true);
                    gameOverScore += 1;
                }
            }

            else if (task.name == "Cafe Cleaning" && task.name == name)
            {
                if (CafeStains == 3)
                {
                    task.transform.GetChild(1).gameObject.SetActive(false);
                    task.transform.GetChild(2).gameObject.SetActive(true);
                    gameOverScore += 1;
                }
            }

            else if (task.name == "Floor Cleaning" && task.name == name)
            {
                if (corridorStains == 3)
                {
                    task.transform.GetChild(1).gameObject.SetActive(false);
                    task.transform.GetChild(2).gameObject.SetActive(true);
                    gameOverScore += 1;
                }
            }

            else if (task.name == "Fit Toilet Paper" && task.name == name)
            {
                if (toiletPaperCount == 2)
                {
                    task.transform.GetChild(1).gameObject.SetActive(false);
                    task.transform.GetChild(2).gameObject.SetActive(true);
                    gameOverScore += 1;
                }
            }
        }

        CheckGameOver();
    }

    void UpdateOrganizeTaskCount(string name)
    {
        foreach (GameObject task in GameManager.instance.taskTemplate)
        {
            if (task.name == "Organize File" && task.name == name)
            {
                task.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Take the Documents and Put them in the Cabinate (" + documentStored.ToString() + "/3)";
            }
            else if (task.name == "Food Throw" && task.name == name)
            {
                task.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Take the leftOver and Throw them in the Dustbin (" + rottenFoodThrownCount.ToString() + "/6)";
            }
            else if (task.name == "Cafe Cleaning" && task.name == name)
            {
                task.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Clean the Stains (" + CafeStains.ToString() + "/3)";
            }
            else if (task.name == "Floor Cleaning" && task.name == name)
            {
                task.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Clean the Stains (" + corridorStains.ToString() + "/3)";
            }
            else if (task.name == "Fit Toilet Paper" && task.name == name)
            {
                task.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Clean the Stains (" + toiletPaperCount.ToString() + "/2)";
            }
        }
    }

    void CheckGameOver()
    {
        if (gameOverScore == 5)
        {
            canRunAway = true;
            GameManager.instance.taskCompleted = true;
            Debug.Log("Run mata Fker");
            creepySmile.Play();
        }
    }

    void FadeIn()
    {
        StartCoroutine(FadeCanvas(canvasGroup, canvasGroup.alpha, 1, fadeDuration));
    }

    private IEnumerator FadeCanvas(CanvasGroup cg, float start, float end, float duration)
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, end, elapsedTime / duration);
            yield return null;
        }

        cg.alpha = end;

        SceneManager.LoadScene("Menu");
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray);
    }
}
