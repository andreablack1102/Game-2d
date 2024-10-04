using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameoverpanel;
    public GameObject replay;
    public void GameOver1()
    {
        gameoverpanel.SetActive(true);
        replay.SetActive(true);
    }
    public void restart()
    {
        Health.totalHealth = 0.9f;
        Scoring.totalScore = 0;
        SceneManager.LoadScene("Level1");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
