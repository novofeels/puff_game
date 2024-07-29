using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
[SerializeField] TextMeshProUGUI textMeshProUGUI;
[SerializeField] Slider slider;
ScoreKeeper scoreKeeper;
private int score;
Health health;

void Update(){
    health = FindObjectOfType<Health>();
	scoreKeeper = FindObjectOfType<ScoreKeeper>();
	textMeshProUGUI.text = "Score: " + scoreKeeper.GetScore();    
    slider.value = health.GetHealth();
}
}
