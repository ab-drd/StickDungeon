using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PostBattle : MonoBehaviour
{
    public Text slainEnemyText;
    public Text characterStats;

    public Transform playerPosition;

    public GameObject playerPrefab;
    private Animator animationController;
    
    public GameObject continueButtonObject;
    Button continueButton;

    public GameObject saveButtonObject;
    Button saveButton;

    public SaveManager saveManager;

    static Hero playerUnit;

    void Start()
    {
        continueButton = continueButtonObject.GetComponent<Button>();
        saveButton = saveButtonObject.GetComponent<Button>();

        GameObject playerGO = Instantiate(playerPrefab, playerPosition);
        playerUnit = playerGO.GetComponent<Hero>();

        animationController = playerGO.GetComponentInChildren<Animator>();
        animationController.SetBool("playVictory", true);

        playerUnit.TakeGlobalStats();

        slainEnemyText.text = "You killed the " + Statistics.LastEnemy.Name + "!";

        characterStats.text = "Your current stats:\n\n" + playerUnit.ToString();

        GlobalControl.Instance.savedHero.SaveStats();
        SaveData.current.totalKills = Statistics.Kills;
        
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(saveButtonObject);
    }

    public void OnContinueButton()
    {
        Debug.Log("Clicked button");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void OnSaveButton()
    {
        saveButton.interactable = false;
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(continueButtonObject);

        saveManager.OnSave(GlobalControl.Instance.savedHero.Name);

        characterStats.text += "\nSave created!";
    }
}
