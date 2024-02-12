using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSO : ScriptableObject
{
    [Header("ActionInfo")]
    public string actionName = "DefaultName";
    public float cooldown;

    public virtual void Use(GameObject origin)
    {
        Debug.Log($"GameObject {origin.name} ha usado la accion: {actionName}");
    }
}
