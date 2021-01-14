using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    private string[] saveFiles;

    [SerializeField]
    public GameObject loadButtonObject;

    private List<GameObject> generatedButtons;
    
    public void OnSave(string saveName)
    {
        SaveSystem.SaveData(saveName, SaveData.current);
    }

    public void GetLoadFiles()
    {
        if(!Directory.Exists(Application.persistentDataPath + "/saves/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves/");
        }

        saveFiles = Directory.GetFiles(Application.persistentDataPath + "/saves/");
    }

    public void ShowLoadScreen()
    {
        generatedButtons = new List<GameObject>();

        GetLoadFiles();

        for (int i = 0; i < saveFiles.Length; i++)
        {
            GameObject button = Instantiate(loadButtonObject) as GameObject;
            button.SetActive(true);

            generatedButtons.Add(button.gameObject);

            button.GetComponent<ButtonListButton>().SetText(saveFiles[i]);

            button.transform.SetParent(loadButtonObject.transform.parent, false);
        }

        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(generatedButtons[0]);
        generatedButtons[0].GetComponent<Button>().OnSelect(null);
    }

    public void ClearGeneratedButtons()
    {
        foreach(GameObject button in generatedButtons)
        {
            Destroy(button);
        }

        generatedButtons.Clear();
    }

    public void ButtonClicked(string path)
    {
        SaveData.current = (SaveData) SaveSystem.LoadData(path);
        GlobalControl.Instance.savedHero.GetStatsFromSave();

        Statistics.Kills = SaveData.current.totalKills;
        Statistics.TotalNumberOfBattles = 10;
        Statistics.EnemiesGenerated = Statistics.Kills;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public bool SavesEmpty()
    {
        GetLoadFiles();

        if (saveFiles.Length == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
