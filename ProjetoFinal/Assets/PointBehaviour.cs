using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointBehaviour : MonoBehaviour
{
    public int points = 0;
    public int maxDigits = 7;
    public TMP_Text pointsText;
    
    void Start()
    {
        UpdateUI();
    }

    public void AddPoints(int x){
        points += x;
        UpdateUI();
    }
    public void RemovePoints(int x){
        points -= x;
        UpdateUI();
    }
    public void MultiplyPoints(int x){
        points *= x;
        UpdateUI();
    }
    private void UpdateUI(){
        pointsText.text = points.ToString($"D{maxDigits}");
    }
}   
