using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScreen : MonoBehaviour
{
    public Text slainEnemiesText;
    public Text finalStatsText;

    public GameObject playerPrefab;
    public Transform playerPosition;

    public GameObject returnToMainMenuButtonObject;

    Unit playerUnit;

    void Start()
    {
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(returnToMainMenuButtonObject);

        GameObject playerGO = Instantiate(playerPrefab, playerPosition);
        playerUnit = playerGO.GetComponent<Unit>();

        slainEnemiesText.text = $"You defeated {Statistics.Kills} monsters!";

        if(playerUnit is Hero hero)
        {
            hero.TakeGlobalStats();

            finalStatsText.text += "\n" + hero.ToString();
        }
    }
}
