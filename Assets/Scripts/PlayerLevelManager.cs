

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelManager : MonoBehaviour
{
    public int playerLevel = 1; // Cấp độ ban đầu của player
    public float currentXP = 0; // Điểm kinh nghiệm hiện tại
    public float xpToNextLevel = 100; // Kinh nghiệm cần để lên cấp
    [SerializeField] private ExpBar _expBar;

    // Khai báo delegate và event cho level up

    // Hàm tăng kinh nghiệm cho player
    public void GainExperience(float amount)
    {
        currentXP += amount;
        _expBar.UpdateExpBar(xpToNextLevel, currentXP);

        // Kiểm tra nếu kinh nghiệm đã đủ để lên cấp
        if (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }
    }

    // Hàm lên cấp cho player
    private void LevelUp()
    {
        playerLevel++;
        currentXP -= xpToNextLevel; // Trừ kinh nghiệm đã dùng để lên cấp
        xpToNextLevel += 20; // Tăng số kinh nghiệm cần cho level tiếp theo (tùy chỉnh theo ý muốn)
        
        _expBar.UpdateExpBar(xpToNextLevel, currentXP);

        GameController.instance.DoLevel();
     
    }
}

