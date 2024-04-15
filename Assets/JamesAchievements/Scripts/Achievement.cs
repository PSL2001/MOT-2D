using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour
{

    [Tooltip("Achievement ID")]
    [SerializeField] private string achievementID;
    [SerializeField] private string achievementTitle;
    [SerializeField] private GameObject description;
    [SerializeField] private Color color1;
    [SerializeField] private Color color2;
    [SerializeField] private Image backgroundImage, image, descriptionBackground;
    [SerializeField] private TextMeshProUGUI descriptionText;
    private bool unlocked;

    private void Start()
    {
        if (!unlocked)
        {
            backgroundImage.color = Color.white;
            image.color = Color.black;
        }
    }

    public string GetTitle() 
    {
        return achievementTitle;
    }

    public string GetAchievementID()
    {
        return achievementID;
    }

    public void ManageDescription(bool state)
    {
        if (!unlocked)
            return;
        description.SetActive(state);
    }

    public void Unlock()
    {
        backgroundImage.color = color1;
        image.color = color2;
        descriptionBackground.color = color2;
        descriptionText.color = color1;
        descriptionText.text = achievementTitle;
        unlocked = true;
    }
}
