using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseLevel : MonoBehaviour
{
    
    public List<Skill_2DataLevel> skill2Levels; 
   
    public Skill_3DataLevel[] skill3Levels;
    public List<Skill_4DataLevel> skill4Levels;
    public List<Skill_5DataLevel> skill5Levels;

    public ScriptSkill1 skill1; 
    public ScriptSkill2 skill2; 
    public ScriptSkill3 skill3; 
    public ScriptSkill4 skill4; 
    public ScriptSkill5 skill5;


    private int clickCount1 = 0;
    private int clickCount2 = 0; 
    private int clickCount3 = 0;
    private int clickCount4 = 0;
    private int clickCount5 = 0;
    public void OnOpen()
    {
        this.gameObject.SetActive(true);
    }

    public void OnClose()
    {
        this.gameObject.SetActive(false);
    }
   
    public void RandomlySelectSkills()
    {
        // Danh sách các ID kỹ năng có thể random ban đầu
        List<int> allIDs = new List<int> { 1, 2, 3, 4, 5 };
        // Kiểm tra nếu clickCount đã đạt mức tối đa thì loại bỏ khỏi danh sách random
        if (clickCount1 >= 5) allIDs.Remove(1);
        if (clickCount2 >= 5) allIDs.Remove(2);
        if (clickCount3 >= 5) allIDs.Remove(3);
        if (clickCount4 >= 5) allIDs.Remove(4);
        if (clickCount5 >= 5) allIDs.Remove(5);
        // Tạo một danh sách để lưu các kỹ năng được chọn
        HashSet<int> selectedIDs = new HashSet<int>();
        System.Random random = new System.Random();
        // Random các kỹ năng cho đến khi có đủ 3 kỹ năng
        while (selectedIDs.Count < 3 && allIDs.Count > 0) // Thêm điều kiện kiểm tra allIDs.Count > 0
        {
            int randomIndex = random.Next(allIDs.Count);
            selectedIDs.Add(allIDs[randomIndex]);
        }
        // Bật hoặc tắt các kỹ năng dựa trên kết quả random
        foreach (int id in new List<int> { 1, 2, 3, 4, 5 })
        {
            if (selectedIDs.Contains(id))
            {
                switch (id)
                {
                    case 1:
                        skill1.OnOpen();
                        break;
                    case 2:
                        skill2.OnOpen();
                        break;
                    case 3:
                        skill3.OnOpen();
                        break;
                    case 4:
                        skill4.OnOpen();
                        break;
                    case 5:
                        skill5.OnOpen();
                        break;
                }
            }
            else
            {
                switch (id)
                {
                    case 1:
                        skill1.OnClose();
                        break;
                    case 2:
                        skill2.OnClose();
                        break;
                    case 3:
                        skill3.OnClose();
                        break;
                    case 4:
                        skill4.OnClose();
                        break;
                    case 5:
                        skill5.OnClose();
                        break;
                }
            }
        }
    }

    public void OnclickSkill_1()
    {

        UiController.instance.PauseLevel.OnClose();
        GameController.instance.ContinueGame();

        GameController.instance.SpawnerCurrent.PlayerRotation.TrySpawnOrbitingPrefab();
        clickCount1++;
    }
    public void OnclickSkill_2()
    {
        UiController.instance.PauseLevel.OnClose();
        GameController.instance.ContinueGame();

        PlayerHealth playerHealth = GameObject.FindObjectOfType<PlayerHealth>();

        if (clickCount2 < skill2Levels.Count)
        {
            if (playerHealth != null && skill2Levels[clickCount2] != null)
            {
                playerHealth.ApplySkill_2(skill2Levels[clickCount2]);
                clickCount2++; 
            }
            else
            {
                Debug.LogError("PlayerHealth hoặc Skill_2DataLevel chưa được gán!");
            }
        }
        else
        {
            Debug.Log("Level tối đa đã đạt được!");
        }
    }
    public void OnclickSkill_3()
    {
        UiController.instance.PauseLevel.OnClose();
        GameController.instance.ContinueGame();

        PlayerRotation playerRotation = GameObject.FindObjectOfType<PlayerRotation>();
        if (playerRotation != null)
        {
            if (clickCount3 < skill3Levels.Length)
            {
                Skill_3DataLevel currentSkill3 = skill3Levels[clickCount3];
                playerRotation.Shoot();
                clickCount3++; 
                playerRotation.currentSkillLevel++;
            }
            else
            {
                Debug.Log("Level tối đa đã đạt được!");
            }
        }
       
    }
    public void OnclickSkill_5()
    {
        UiController.instance.PauseLevel.OnClose();
        GameController.instance.ContinueGame();
        PlayerHealth playerHealth = GameObject.FindObjectOfType<PlayerHealth>();


        if (clickCount5 < skill5Levels.Count)
        {
            if (playerHealth != null && skill5Levels[clickCount5] != null)
            {
                playerHealth.ApplySkill_5(skill5Levels[clickCount5]);
                clickCount5++;
            }
            else
            {
                Debug.LogError("PlayerHealth hoặc Skill_5DataLevel chưa được gán!");
            }
        }
        else
        {
            Debug.Log("Level tối đa đã đạt được!");
        }

    }
    public void OnclickSkill_4()
    {
        UiController.instance.PauseLevel.OnClose();
        GameController.instance.ContinueGame();
        
        if (GameController.instance.SpawnerCurrent.damageZone == null)
            Debug.Log("GameController.instance.SpawnerCurrent. damageZone NULL");
        if (clickCount4 < skill4Levels.Count)
        {
            
            Skill_4DataLevel currentSkill4 = skill4Levels[clickCount4];
            
            GameController.instance.SpawnerCurrent. damageZone.gameObject.SetActive(true);
            GameController.instance.SpawnerCurrent.damageZone.SetRadius(currentSkill4.Radius);
            GameController.instance.SpawnerCurrent.damageZone.damagePerSecond = currentSkill4.damagePerSecond;
            clickCount4++;
        }
        else
        {
            Debug.Log("Level tối đa đã đạt được!");
        }
    }



}
