using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text nameText;
    public Text levelText;
    public Text HPValues;
    public Slider hpSlider;

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.Name;
        levelText.text = "Level " + unit.Level;

        HPValues.text = unit.CurrentHP + "/" + unit.MaxHP;

        hpSlider.maxValue = unit.MaxHP;
        hpSlider.value = unit.CurrentHP;
    }

    public void SetHP(int currentHP, int maxHP)
    {
        HPValues.text = currentHP + "/" + maxHP;
        hpSlider.value = currentHP;
    }
}
