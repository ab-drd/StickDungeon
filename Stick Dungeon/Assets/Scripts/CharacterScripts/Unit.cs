using UnityEngine;

public class Unit : MonoBehaviour
{
    public string Name;
    public int Level;
    public int Experience;

    public int Damage;

    public int MaxHP;
    public int CurrentHP;

    public bool TakeDamage(int damage)
    {
        CurrentHP -= damage;

        if(CurrentHP <= 0)
        {
            CurrentHP = 0;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetHp(int amount)
    {
        MaxHP = amount;
        CurrentHP = amount;
    }

    public void SetLevel(int level)
    {
        Level = level;
        Experience = level * 10 + UnityEngine.Random.Range(1, 5);
    }

    public void SetDamage(int amount)
    {
        Damage = amount;
    }
}