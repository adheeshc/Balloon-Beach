using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI scoreUI;
    void Update()
    {
        scoreUI.text = score.ToString();
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.tag == "scoreUp") {
            score++;
        }
    }
}
