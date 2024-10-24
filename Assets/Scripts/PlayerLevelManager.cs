

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelManager : MonoBehaviour
{
    public int playerLevel = 1; 
    public float currentXP = 0;
    public float xpToNextLevel = 100; 
    [SerializeField] private ExpBar _expBar;

    

    public void GainExperience(float amount)
    {
        currentXP += amount;
        _expBar.UpdateExpBar(xpToNextLevel, currentXP);

       
        if (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        playerLevel++;
        currentXP -= xpToNextLevel;
        xpToNextLevel += 20; 
        
        _expBar.UpdateExpBar(xpToNextLevel, currentXP);

        GameController.instance.DoLevel();
     
    }
}

