using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ArmorBar : MonoBehaviour
{
    public Text ArmorText;

    [SerializeField] private Image _armorPlayer;

    public void UpdateArmorbarPlayer(float maxArmor)
    {
        ArmorText.text = "Armor: "+ maxArmor.ToString();
        _armorPlayer.fillAmount = maxArmor;
    }
}


