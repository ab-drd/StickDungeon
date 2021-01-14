using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NameInput : MonoBehaviour
{
    public GameObject playButtonObject;
    public GameObject inputFieldObject;
    public GameObject yourNameTextObject;
    public GameObject yourHeroNameTextObject;

    public InputField field;
    private bool wasFocused;

    private string _name;

    void Update()
    {
        if (wasFocused && Input.GetKeyDown(KeyCode.Return))
        {
            GetInput(field.text);
        }

        wasFocused = field.isFocused;
    }

    public void SetStartConditions()
    {
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(inputFieldObject);
        playButtonObject.SetActive(false);
        yourNameTextObject.SetActive(false);
    }

    public void GetInput(string inputName)
    {
        _name = inputName;

        inputFieldObject.SetActive(false);
        yourHeroNameTextObject.SetActive(false);
        playButtonObject.SetActive(true);
        yourNameTextObject.SetActive(true);

        yourNameTextObject.GetComponent<Text>().text = $"Your name is:\n{_name}";

        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(playButtonObject);
    }

    public void OnPlayGameButton()
    {
        Statistics.Kills = 0;
        Statistics.EnemiesGenerated = 0;
        Statistics.TotalNumberOfBattles = 10;

        Hero.SetDefault(GlobalControl.Instance.savedHero);
        GlobalControl.Instance.savedHero.Name = _name;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
