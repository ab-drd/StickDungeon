using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackfeed : MonoBehaviour
{
    [SerializeField]
    GameObject attackfeedItemPrefab;

    public BattleSystem battleSystem;

    public QueueHolder queueHolder;

    void Start()
    {
        queueHolder = new QueueHolder();
        battleSystem.onAttackCallback += OnAttack;
    }

    public void OnAttack(string dealer, int amount)
    {
        GameObject go = Instantiate(attackfeedItemPrefab, transform);
        go.transform.SetAsFirstSibling();
        go.GetComponent<AttackfeedItem>().Setup(dealer, amount);

        if (queueHolder.AddToQueue(go))
        {
            Destroy(queueHolder.goQueue.Dequeue());
        }
    }
}
