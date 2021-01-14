public class Goblin : Unit
{
    public int CriticalChance;

    public bool IsCriticalAttack()
    {
        var random = new System.Random();

        if (random.Next(101) < CriticalChance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}