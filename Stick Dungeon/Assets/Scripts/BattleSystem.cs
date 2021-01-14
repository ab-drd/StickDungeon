using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum BattleState
{
    START,
    PLAYERTURN,
    ENEMYTURN,
    WON,
    LOST
}

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public EnemyHelper enemyPrefabHolder;

    public Transform playerPosition;
    public Transform enemyPosition;

    static Hero playerUnit;
    Unit enemyUnit;

    public Text dialogueText;
    public Text killsText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public GameObject swingButtonObject;
    public GameObject rageButtonObject;
    public GameObject restButtonObject;
    public GameObject returnToMenuButtonObject;
    
    Button swingButton;
    Button rageButton;
    Button restButton;
    Button returnToMenuButton;

    public GameObject continueButtonObject;
    Button continueButton;

    public delegate void OnAttackCallback(string dealer, int amount);
    public OnAttackCallback onAttackCallback;

    private Animator animationController;
    private Animator enemyAnimationController;

    public BattleState state;

    int restsUsed;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        restsUsed = 0;

        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerPosition);
        playerUnit = playerGO.GetComponent<Hero>();

        playerUnit.TakeGlobalStats();

        GameObject enemyGO = Instantiate(enemyPrefabHolder.GenerateRandomEnemy(), enemyPosition) as GameObject;
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.text = "A " + enemyUnit.Name + " attacked you!";

        killsText.text = $"{Statistics.EnemiesGenerated}/{Statistics.TotalNumberOfBattles}";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        animationController = playerUnit.GetComponentInChildren<Animator>();
        enemyAnimationController = enemyGO.GetComponent<Unit>().GetComponentInChildren<Animator>();

        swingButton = swingButtonObject.GetComponent<Button>();
        rageButton = rageButtonObject.GetComponent<Button>();
        restButton = restButtonObject.GetComponent<Button>();
        returnToMenuButton = returnToMenuButtonObject.GetComponent<Button>();
        continueButton = continueButtonObject.GetComponent<Button>();
        continueButtonObject.SetActive(false);

        DisablePlayerActions();

        yield return new WaitForSecondsRealtime(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        animationController.SetBool("playSwing", true);
        animationController.SetBool("exitPlaySwing", false);

        dialogueText.text = "You swing your sword!";

        yield return new WaitForSeconds(1f);

        animationController.SetBool("playSwing", false);
        animationController.SetBool("exitPlaySwing", true);

        bool isDead = enemyUnit.TakeDamage(playerUnit.Damage);

        enemyHUD.SetHP(enemyUnit.CurrentHP, enemyUnit.MaxHP);
        dialogueText.text = "You deal " + playerUnit.Damage + " damage to the " + enemyUnit.Name + "!";

        onAttackCallback.Invoke(playerUnit.Name, playerUnit.Damage);

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            StartCoroutine(EndBattle());
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator PlayerRageAttack()
    {
        if(playerUnit.CurrentHP < playerUnit.MaxHP / 5)
        {
            dialogueText.text = "Doing that would kill you!";

            yield return new WaitForSeconds(1f);

            PlayerTurn();
        }
        else
        {
            animationController.SetBool("playViolentSwing", true);
            animationController.SetBool("exitPlayViolentSwing", false);

            playerUnit.TakeDamage(playerUnit.MaxHP / 5);
            playerHUD.SetHP(playerUnit.CurrentHP, playerUnit.MaxHP);
            dialogueText.text = "You violently swing your sword!";

            yield return new WaitForSeconds(1f);

            animationController.SetBool("playViolentSwing", false);
            animationController.SetBool("exitPlayViolentSwing", true);
            
            bool isDead = enemyUnit.TakeDamage(playerUnit.Damage * 2);

            enemyHUD.SetHP(enemyUnit.CurrentHP, enemyUnit.MaxHP);
            dialogueText.text = "You deal " + playerUnit.Damage * 2 + " damage to the " + enemyUnit.Name + "!";

            onAttackCallback.Invoke(playerUnit.Name, playerUnit.Damage * 2);

            yield return new WaitForSeconds(2f);

            if (isDead)
            {
                state = BattleState.WON;
                StartCoroutine(EndBattle());
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }
    }

    IEnumerator PlayerHeal()
    {
        if (restsUsed > 2)
        {
            dialogueText.text = "You already used up your Rests for this encounter!";

            yield return new WaitForSeconds(1f);

            PlayerTurn();
        }
        else
        {
            animationController.SetBool("playHeal", true);
            animationController.SetBool("exitPlayHeal", false);

            dialogueText.text = "Heavenly energy surrounds you!";

            int healedAmount = playerUnit.Heal(playerUnit.HealAmount);

            playerHUD.SetHP(playerUnit.CurrentHP, playerUnit.MaxHP);

            yield return new WaitForSeconds(1.5f);

            dialogueText.text = $"You heal {healedAmount} HP!";

            animationController.SetBool("playHeal", false);
            animationController.SetBool("exitPlayHeal", true);

            restsUsed++;

            yield return new WaitForSeconds(2f);

            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        enemyAnimationController.SetBool("playAttack", true);
        enemyAnimationController.SetBool("exitPlayAttack", false);

        if (enemyUnit.Name == "Goblin")
        {
            dialogueText.text = "The Goblin stabs you! ";
        }
        else if (enemyUnit.Name == "Corrupt")
        {
            dialogueText.text = "The Corrupt swings its mighty axe at you! ";
        }

        yield return new WaitForSeconds(1f);

        enemyAnimationController.SetBool("playAttack", false);
        enemyAnimationController.SetBool("exitPlayAttack", true);

        bool isDead;
        int playerDamageTaken;

        if (enemyUnit is Corrupt corrupt)
        {
            if (corrupt.DoAttack())
            {
                playerDamageTaken = playerUnit.MaxHP / 4;
            }
            else
            {
                playerDamageTaken = corrupt.Damage;
            }
        }
        else if (enemyUnit is Goblin goblin)
        {
            if (goblin.IsCriticalAttack())
            {
                dialogueText.text = "Critical hit! ";
                playerDamageTaken = goblin.Damage * 3 / 2;
            }
            else
            {
                playerDamageTaken = goblin.Damage;
            }
        }
        else
        {
            playerDamageTaken = 0;
            Debug.LogError("Incorrect type");
        }

        dialogueText.text += $"The {enemyUnit.Name} deals " + playerDamageTaken + " damage!";
        isDead = playerUnit.TakeDamage(playerDamageTaken);
        playerHUD.SetHP(playerUnit.CurrentHP, playerUnit.MaxHP);

        onAttackCallback.Invoke(enemyUnit.Name, playerDamageTaken);

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    IEnumerator EndBattle()
    {
        Statistics.LastEnemy = enemyUnit;

        if (state == BattleState.WON)
        {
            enemyAnimationController.SetBool("playDeath", true);

            dialogueText.text = "You defeated the " + enemyUnit.Name + "!";
            Statistics.Kills++;

            playerUnit.LevelUp(enemyUnit.Experience);

            GlobalControl.Instance.savedHero.CopyStats(playerUnit);

        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated.";

            animationController.SetBool("playDeath", true);
        }
        else
        {
            Debug.LogError("Incorrect state");
        }

        yield return new WaitForSeconds(2f);

        continueButtonObject.SetActive(true);

        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(continueButtonObject);
    }

    void PlayerTurn()
    {
        dialogueText.text = "Choose an action:";

        AllowPlayerActions();

        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(swingButtonObject);
    }

    void DisablePlayerActions()
    {
        swingButton.interactable = false;
        rageButton.interactable = false;
        restButton.interactable = false;
        returnToMenuButton.interactable = false;
    }

    void AllowPlayerActions()
    {
        swingButton.interactable = true;
        rageButton.interactable = true;
        restButton.interactable = true;
        returnToMenuButton.interactable = true;
    }

    public void OnAttackButton()
    {
        DisablePlayerActions();

        StartCoroutine(PlayerAttack());
    }

    public void OnRageAttackButton()
    {
        DisablePlayerActions();

        StartCoroutine(PlayerRageAttack());
    }

    public void OnRestButton()
    {
        DisablePlayerActions();

        StartCoroutine(PlayerHeal());
    }

    public void OnContinueButton()
    {
        if (state == BattleState.WON)
        {
            if (Statistics.Kills < Statistics.TotalNumberOfBattles)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
            }
        }
        else if (state == BattleState.LOST)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
        }
        else
        {
            Debug.LogError("Continue button appeared at wrong time!");
        }
    }
}
