using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuiManager : MonoBehaviour
{
    public static GuiManager instance {get; private set;}
    [SerializeField] private TMP_Text scoreText;
    private int totalScore = 0;

    private void Awake() {
        if(instance != null && instance != this){
            Destroy(this.gameObject);
        }

        instance = this;
    }

    public void UpdateScore(int points){
        totalScore += points;

        scoreText.text = string.Format("Score: {0}",totalScore);
    }
}
