using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitOptions : MonoBehaviour
{
    public GameObject exitButtonObject;

    void Start()
    {
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(exitButtonObject);
        exitButtonObject.GetComponent<Button>().OnSelect(null);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
