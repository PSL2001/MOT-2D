using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameData 
{
    public string currentSceneName;
    public int deathCount;
}
public class GameDataManager : MonoBehaviourSingleton<GameDataManager>
{
    private void Start()
    {
        UpdateCurrentSceneName();
    }

    public GameData gameData;
    public void AddDeath() { gameData.deathCount++; }
    public void UpdateCurrentSceneName() { gameData.currentSceneName = SceneManager.GetActiveScene().name; }

    public void LossProgressAndAddDeath()
    {
        SaveManager.Instance.LoadData();
        AddDeath();
        SaveManager.Instance.SaveData();
    }
    public void UodateSceneAndSave() 
    {
        UpdateCurrentSceneName();
        SaveManager.Instance.SaveData();
    }
}
