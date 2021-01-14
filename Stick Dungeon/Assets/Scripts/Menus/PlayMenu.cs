using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayMenu : MonoBehaviour
{
    public SaveManager saveManager;

    public GameObject newGameButtonObject;
    public GameObject loadGameButtonObject;
    Button loadGameButton;

    void Start()
    {
        loadGameButton = loadGameButtonObject.GetComponent<Button>();

        if (saveManager.SavesEmpty())
        {
            loadGameButton.interactable = false;
        }
    }

    public void SetSelected()
    {
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(newGameButtonObject);

    }
}
