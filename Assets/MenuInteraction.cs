//////////////////////////////////////////////
//Assignment/Lab/Project: Final
//Name: Eliza Majernik
//Section: SGD.213.2172
//Instructor: Brian Sowers
//Date: 
/////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuInteraction : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button howToButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button quitButton;

    [SerializeField] private GameObject howToPanel;
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(Quit);
        if (howToButton != null)
        {
            howToButton.onClick.AddListener(DisplayHowTo);
            closeButton.onClick.AddListener(CloseHowTo);
        }
    }


    void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    void DisplayHowTo()
    {
        howToPanel.SetActive(true);
    }

    void CloseHowTo()
    {
        howToPanel.SetActive(false);
    }

    void Quit()
    {
        Application.Quit();
    }
}
