using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateTextUI : MonoBehaviour
{
    TextMeshProUGUI textUI;

    [SerializeField] Color enableColor = Color.green;
    [SerializeField] Color disableColor = Color.gray;

    void Start()
    {
        textUI = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateText(string text)
    {
        textUI.SetText(text);
    }

    public void UpdateText(string text, bool enabled)
    {
        UpdateText(text);

        if (enabled) textUI.color = enableColor;
        else textUI.color = disableColor;
    }

    public void UpdateText(float value) 
    {
        textUI.SetText(value.ToString()); //Si queremos un parametro se puede pasar en toString
    }
}
