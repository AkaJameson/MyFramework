using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTips:MonoBehaviour
{
    public virtual void OnEnter()
    {
        gameObject.SetActive(true);
    }

    public virtual void OnExit()
    {
        gameObject.SetActive(false);
    }
}
