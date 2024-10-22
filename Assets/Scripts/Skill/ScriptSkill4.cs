using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptSkill4 : MonoBehaviour
{
    public void OnOpen()
    {
        this.gameObject.SetActive(true);
        Debug.Log("Skill4 Open");
    }

    public void OnClose()
    {
        this.gameObject.SetActive(false);
        Debug.Log("Skill4 close");
    }



}
