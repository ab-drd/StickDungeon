using UnityEngine;

public class EnemyHelper : MonoBehaviour
{
    public GameObject[] prefabs = new GameObject[2];

    public GameObject GenerateRandomEnemy()
    {
        GameObject randomEnemy;
        int randomInt = Random.Range(0, 100);

        if(randomInt < 76)
        {
            randomEnemy = prefabs[0];
        }
        else
        {
            randomEnemy = prefabs[1];
        }

        Unit enemyUnit = randomEnemy.GetComponent<Unit>();
        
        if (enemyUnit is Goblin goblin)
        {
            RandomizeGoblin(goblin);
        }
        else if (enemyUnit is Corrupt corrupt)
        {
            RandomizeCorrupt(corrupt);
        }
        else
        {
            Debug.Log("Error: no type");
        }

        Statistics.EnemiesGenerated++;

        return randomEnemy;
    }

    void RandomizeGoblin(Goblin goblin)
    {
        goblin.SetLevel(Random.Range(1, 4) + Statistics.EnemiesGenerated * 3 / 2);
        goblin.SetHp(Random.Range(15, 26) + goblin.Level * 2 + 3 * Statistics.EnemiesGenerated / 2);
        goblin.SetDamage(Random.Range(5, 11) + goblin.Level + Statistics.EnemiesGenerated * 2);
        goblin.CriticalChance = Random.Range(10, 16) + goblin.Level + Statistics.EnemiesGenerated;
    }

    void RandomizeCorrupt(Corrupt corrupt)
    {
        corrupt.SetLevel(Random.Range(2, 5) + Statistics.EnemiesGenerated * 3 / 2);
        corrupt.SetHp(Random.Range(30, 46) + corrupt.Level * 2 + 2 * Statistics.EnemiesGenerated / 3);
        corrupt.SetDamage(Random.Range(20, 26) + corrupt.Level * 2 / 3 + Statistics.EnemiesGenerated / 3);
    }
}
