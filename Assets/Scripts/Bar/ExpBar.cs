using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    public Text ExpText;
    [SerializeField] private Image _expBar;
    public void UpdateExpBar(float xpToNextLevel, float currentXP)
    {
        ExpText.text = currentXP.ToString() + " / " + xpToNextLevel.ToString();
        _expBar.fillAmount = currentXP / xpToNextLevel;
    }
}
