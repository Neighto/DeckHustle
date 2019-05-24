using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyScript : MonoBehaviour {

    public static DifficultyScript Instance;
    public bool isLegendaryMode = false;

    private void Awake()
    {
        if (Instance)
            DestroyImmediate(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
    }

    public void EnterNormalMode()
    {
        isLegendaryMode = false;
    }

    public void EnterLegendaryMode()
    {
        isLegendaryMode = true;
    }
	
}
