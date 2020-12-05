using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchScript : MonoBehaviour
{
    private int _gameScene;

    public void Start()
    {
        _gameScene = SceneManager.GetActiveScene().buildIndex + 1;
    }
    
    public void OnStartClick()
    {
        SceneManager.LoadScene(_gameScene);
    }
}