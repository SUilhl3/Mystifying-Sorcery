using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public float playerSpeed = 15f;

    
    public PlayerData data = null;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(instance);
    }

    public static GameManager GetInstance()
    {
        return instance;
    }
  
    public void AssignObjects()
    {
        data = GameObject.Find("Player").GetComponent<PlayerController>().playerData; //finds a player in scene and applies its game playerData to data for changing
        
    }
}
