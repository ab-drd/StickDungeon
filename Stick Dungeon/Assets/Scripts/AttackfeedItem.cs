using UnityEngine;
using UnityEngine.UI;

public class AttackfeedItem : MonoBehaviour
{
    [SerializeField]
    Text text;

    public void Setup(string dealer, int amount)
    {
        text.text = $"<b>{dealer}</b> did {amount} damage!";
    }
}
