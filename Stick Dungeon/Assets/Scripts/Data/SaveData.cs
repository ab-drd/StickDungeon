[System.Serializable]
public class SaveData
{
    private static SaveData _current;
    public static SaveData current
    {
        get
        {
            if(_current == null)
            {
                _current = new SaveData();
            }
            return _current;
        }
        set
        {
            if(value != null)
            {
                _current = value;
            }
        }
    }

    public string heroName;
    public int heroLevel;
    public int heroExperience;
    public int heroDamage;
    public int heroMaxHP;
    public int heroCurrentHP;
    public int heroHealAmount;

    public int totalKills;
}
