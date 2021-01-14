public class Hero : Unit
{
    public int HealAmount;

    public void TakeGlobalStats()
    {
        CopyStats(GlobalControl.Instance.savedHero);
    }

    public void CopyStats(Hero copyFrom)
    {
        Name = copyFrom.Name;
        MaxHP = copyFrom.MaxHP;
        CurrentHP = copyFrom.CurrentHP;
        Level = copyFrom.Level;
        Experience = copyFrom.Experience;
        Damage = copyFrom.Damage;
        HealAmount = copyFrom.HealAmount;
    }
    
    public void LevelUp(int expGained)
    {
        Experience += expGained;

        while (Experience >= Data.ExperienceLevels[Level])
        {
            Experience -= Data.ExperienceLevels[Level];
            Level++;

            MaxHP += Level * 5;
            CurrentHP += Level * 5;
            Damage += Level * 2;
            HealAmount += 5 + Level;
        }
    }

    public int Heal(int amount)
    {
        int healed = amount;

        CurrentHP += amount;

        if (CurrentHP > MaxHP)
        {
            healed -= CurrentHP - MaxHP;
            CurrentHP = MaxHP;
        }

        return healed;
    }

    public override string ToString()
    {
        return $"Health:\t\t\t\t{CurrentHP}/{MaxHP}\n" +
            $"Level:\t\t\t\t{Level}\n" +
            $"Experience:\t\t{Experience}/{Data.ExperienceLevels[Level]}\n" +
            $"Damage:\t\t\t{Damage}";
    }

    public static void SetDefault(Hero setDataHero)
    {
        setDataHero.Name = "Warrior";
        setDataHero.MaxHP = 100;
        setDataHero.CurrentHP = 100;
        setDataHero.Level = 1;
        setDataHero.Experience = 0;
        setDataHero.Damage = 15;
        setDataHero.HealAmount = 20;
    }

    public void GetStatsFromSave()
    {
        Name = SaveData.current.heroName;
        MaxHP = SaveData.current.heroMaxHP;
        CurrentHP = SaveData.current.heroCurrentHP;
        Level = SaveData.current.heroLevel;
        Experience = SaveData.current.heroExperience;
        Damage = SaveData.current.heroDamage;
        HealAmount = SaveData.current.heroHealAmount;
    }


    public void SaveStats()
    {
        SaveData.current.heroName = Name;
        SaveData.current.heroMaxHP = MaxHP;
        SaveData.current.heroCurrentHP = CurrentHP;
        SaveData.current.heroLevel = Level;
        SaveData.current.heroExperience = Experience;
        SaveData.current.heroDamage = Damage;
        SaveData.current.heroHealAmount = HealAmount;
    }
}
