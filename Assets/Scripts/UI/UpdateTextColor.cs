using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateTextColor : MonoBehaviour
{
    TextMeshProUGUI textUI;

    [SerializeField] Color swapColor = Color.gray;
    // Start is called before the first frame update
    void Start()
    {
        textUI = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void UpdateColor() 
    {
        textUI.color = swapColor;
    }
}
