using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Text healthText;

    [SerializeField] private Image _healthbarPlayer;

    public void UpdateHealthbarPlayer(float maxHealth, float currentHelth)
    {
         healthText.text = currentHelth.ToString() + " / " + maxHealth.ToString();
        _healthbarPlayer.fillAmount = currentHelth / maxHealth;
    }
}
