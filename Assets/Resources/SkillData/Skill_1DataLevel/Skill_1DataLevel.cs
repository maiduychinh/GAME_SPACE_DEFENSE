using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Skill1_Level", menuName = "ScriptableObjects/Skill_1DataLevel", order = 1)]
[System.Serializable]
public class Skill_1DataLevel : ScriptableObject
{
    public string SkillName;
    public int quantitySaw;
}
