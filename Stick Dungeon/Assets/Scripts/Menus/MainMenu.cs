using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject playButtonObject;

    public void SetSelected()
    {
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(playButtonObject);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
