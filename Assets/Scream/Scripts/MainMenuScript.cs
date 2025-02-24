using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public GameObject rules;

    void Start()
    {
        Cursor.visible = true;
    }

    public void OnRuleClick()
    {
        rules.SetActive(true);
    }

    public void OnCrossClick()
    {
        rules.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
