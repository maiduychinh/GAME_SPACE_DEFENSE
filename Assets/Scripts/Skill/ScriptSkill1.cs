using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptSkill1 : MonoBehaviour
{

    public void OnOpen()
    {
        this.gameObject.SetActive(true);
        Debug.Log("Skill1 Open");
    }

    public void OnClose()
    {
        this.gameObject.SetActive(false);
        Debug.Log("Skill1 close");
    }
}
