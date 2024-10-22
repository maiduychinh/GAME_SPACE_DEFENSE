using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptSkill2 : MonoBehaviour
{
    public void OnOpen()
    {
        this.gameObject.SetActive(true);
        Debug.Log("Skill2 Open");
    }

    public void OnClose()
    {
        this.gameObject.SetActive(false);
        Debug.Log("Skill2 close");
    }
}
