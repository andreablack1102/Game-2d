using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowWin : MonoBehaviour
{
    public Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = Scoring.totalScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
