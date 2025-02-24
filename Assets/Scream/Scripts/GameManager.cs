using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<GameObject> workingDesk;
    public GameObject cabinate;

    public List<int> tasks;
    public List<string> taskName;
    public List<GameObject> taskTemplate;

    [Header("Full Screen Canvas")]
    public GameObject interactText;

    [Header("Work Place")]
    public List<GameObject> workFiles;
    public GameObject workCabin;
    public List<int> workTemp;
    public List<GameObject> CurrentWorkDocuments;

    [Header("Cafe Place")]
    public List<GameObject> rotenFood;
    public GameObject cafeDustbin;
    public List<int> cafeTemp;
    public List<GameObject> currentRotenFood;

    [Header("Cafe Clean")]
    public List<GameObject> cafeStains;
    public List<int> cafeStainTemp;
    public List<GameObject> currentCafeStain;

    [Header("Corridor Clean")]
    public List<GameObject> corridorStains;
    public List<int> corridorStainTemp;
    public List<GameObject> currentCorridorStains;

    [Header("Water Refill")]
    public GameObject waterCan;
    public GameObject tempWaterCan;

    [Header("bathroom")]
    public GameObject toiletpaper1;
    public GameObject toiletpaper2;
    public GameObject toiletpapertemp1;
    public GameObject toiletpapertemp2;

    [Header("Mop")]
    public GameObject mop;
    public GameObject tempMop;

    [Header("Timer")]
    public float timeCount;
    public GameObject timerText;
    public bool timeStart;

    [SerializeField] bool pause;
    public GameObject pauseText;
    public AudioSource bgAudio;
    [SerializeField] bool gameOverBool = true;
    public AudioSource gameOverLaugh;
    public GameObject cameraMain;

    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float fadeDuration = 8f;

    public bool taskCompleted = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Start()
    {
        GetTask();
        DisableTheseAtStart();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    void Update()
    {
        if (timeStart == true)
        {
            if (taskCompleted == false)
            {
                if (timeCount <= 0 && gameOverBool == true)
                {
                    FadeIn();
                    timerText.SetActive(false);
                    timeCount = 0;
                    timerText.GetComponent<TextMeshProUGUI>().text = timeCount.ToString();
                    gameOverLaugh.Play();
                    gameOverBool = false;
                }
                else if (timeCount > 0 && gameOverBool == true)
                {
                    timeCount -= 1 * Time.deltaTime;
                    timerText.GetComponent<TextMeshProUGUI>().text = "Timer : " + timeCount.ToString();
                }
            }

            else if (taskCompleted == true)
            {
                timerText.GetComponent<TextMeshProUGUI>().text = "Leave the place";
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pause == false)
            {
                Time.timeScale = 0;
                pauseText.SetActive(true);
                bgAudio.Pause();
                pause = true;
            }
            else
            {
                Time.timeScale = 1;
                pauseText.SetActive(false);
                bgAudio.UnPause();
                Cursor.lockState = CursorLockMode.Locked;
                pause = false;
            }

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

    void DisableTheseAtStart()
    {
        interactText.SetActive(false);
    }

    void GetTask()
    {
        int temp;
        for (int i = 0; i < 5; i++)
        {
            do
            {
                temp = Random.Range(1, 6);
            } while (tasks.Contains(temp));

            tasks.Add(temp);
        }
        DisplayTasks();
    }

    void DisplayTasks()
    {
        for (int i = 0; i < tasks.Count; i++)
        {
            switch (tasks[i])
            {
                case 1:
                    taskName.Add("Organize File");
                    taskTemplate[i].name = "Organize File";
                    taskTemplate[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Work Place";
                    taskTemplate[i].transform.GetChild(1).gameObject.SetActive(true);
                    taskTemplate[i].transform.GetChild(2).gameObject.SetActive(false);
                    taskTemplate[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Take the Documents and Put them in the Cabinate (" + 0.ToString() + "/3)";
                    OrganizeFile();
                    break;
                case 2:
                    taskName.Add("Cafe Cleaning");
                    taskTemplate[i].name = "Cafe Cleaning";
                    taskTemplate[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Cafe";
                    taskTemplate[i].transform.GetChild(1).gameObject.SetActive(true);
                    taskTemplate[i].transform.GetChild(2).gameObject.SetActive(false);
                    taskTemplate[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Clean the Stains (" + 0.ToString() + "/3)";
                    CafeCleaning();
                    break;
                case 3:
                    taskName.Add("Floor Cleaning");
                    taskTemplate[i].name = "Floor Cleaning";
                    taskTemplate[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Corridor";
                    taskTemplate[i].transform.GetChild(1).gameObject.SetActive(true);
                    taskTemplate[i].transform.GetChild(2).gameObject.SetActive(false);
                    taskTemplate[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Clean the Stains (" + 0.ToString() + "/3)";
                    CorridorCleaning();
                    break;
                case 4:
                    taskName.Add("Bathroom");
                    taskTemplate[i].name = "Fit Toilet Paper";
                    taskTemplate[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Bathroom";
                    taskTemplate[i].transform.GetChild(1).gameObject.SetActive(true);
                    taskTemplate[i].transform.GetChild(2).gameObject.SetActive(false);
                    taskTemplate[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Take The Toilet Paper and Refill (" + 0.ToString() + "/2)";
                    Bathroom();
                    break;
                case 5:
                    taskName.Add("Cafe Food Cleaning");
                    taskTemplate[i].name = "Food Throw";
                    taskTemplate[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Cafe";
                    taskTemplate[i].transform.GetChild(1).gameObject.SetActive(true);
                    taskTemplate[i].transform.GetChild(2).gameObject.SetActive(false);
                    taskTemplate[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Take the leftOver and Throw them in the Dustbin (" + 0.ToString() + "/6)";
                    CafeThrowFood();
                    break;
                // taskName.Add("Conference Room");
                // taskTemplate[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Conference Room";
                // taskTemplate[i].transform.GetChild(1).gameObject.SetActive(true);
                // taskTemplate[i].transform.GetChild(2).gameObject.SetActive(false);
                // taskTemplate[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Organize the Room";
                // break;
                case 6:
                    taskName.Add("Cafe Food Cleaning");
                    taskTemplate[i].name = "Food Throw";
                    taskTemplate[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Cafe";
                    taskTemplate[i].transform.GetChild(1).gameObject.SetActive(true);
                    taskTemplate[i].transform.GetChild(2).gameObject.SetActive(false);
                    taskTemplate[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Take the leftOver and Throw them in the Dustbin (" + 0.ToString() + "/6)";
                    CafeThrowFood();
                    break;
                    // default:
                    //     taskName.Add("Default Room");
                    //     taskTemplate[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Default";
                    //     taskTemplate[i].transform.GetChild(1).gameObject.SetActive(true);
                    //     taskTemplate[i].transform.GetChild(2).gameObject.SetActive(false);
                    //     taskTemplate[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Default";
                    //     break;
            }
        }
    }

    void OrganizeFile()
    {
        int temp;
        for (int i = 0; i < 3; i++)
        {
            do
            {
                temp = Random.Range(0, workFiles.Count);
            }
            while (workTemp.Contains(temp));

            workTemp.Add(temp);
            workFiles[temp].AddComponent<Outline>();
            CurrentWorkDocuments.Add(workFiles[temp]);
            workFiles[temp].tag = "doc";
        }
    }

    void CafeThrowFood()
    {
        int temp;
        for (int i = 0; i < 6; i++)
        {
            do
            {
                temp = Random.Range(0, rotenFood.Count);
            }
            while (cafeTemp.Contains(temp));

            cafeTemp.Add(temp);
            rotenFood[temp].AddComponent<Outline>();
            currentRotenFood.Add(rotenFood[temp]);
            rotenFood[temp].SetActive(true);
            rotenFood[temp].tag = "rottenFood";
        }
    }

    void CafeCleaning()
    {
        int temp;
        for (int i = 0; i < 3; i++)
        {
            do
            {
                temp = Random.Range(0, cafeStains.Count);
            }
            while (cafeStainTemp.Contains(temp));

            cafeStainTemp.Add(temp);
            cafeStains[temp].AddComponent<Outline>().enabled = false;
            cafeStains[temp].SetActive(true);
            cafeStains[temp].tag = "cafeStain";
        }
    }

    void CorridorCleaning()
    {
        int temp;
        for (int i = 0; i < 3; i++)
        {
            do
            {
                temp = Random.Range(0, corridorStains.Count);
            }
            while (corridorStainTemp.Contains(temp));

            corridorStainTemp.Add(temp);
            corridorStains[temp].AddComponent<Outline>().enabled = false;
            corridorStains[temp].SetActive(true);
            corridorStains[temp].tag = "corridorStain";
        }
    }


    void Bathroom()
    {
        toiletpaper1.AddComponent<Outline>().enabled = true;
        toiletpaper1.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineVisible;

        toiletpaper2.AddComponent<Outline>().enabled = true;
        toiletpaper2.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineVisible;


        toiletpaper1.tag = "toiletPaper";
        toiletpaper2.tag = "toiletPaper";

    }
}