using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    public static GlobalControl _instance = null;
    public static GlobalControl Instance { get { return _instance; } }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (_instance != this && _instance != null)
        {
            Destroy(gameObject);
            return;
        }

        if (Instance) return;

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public Hero savedHero;
    public string savedHeroName;
}