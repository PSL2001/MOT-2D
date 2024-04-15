using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public string newGameScene = "Level1";

    //Referencias menu principal
    [Header("Menu")]
    [SerializeField] GameObject menupanel;
    [SerializeField] Button menuInitButton;
    [SerializeField] Button continueMenuButton;


    //Menu creditos
    [Header("Credit")]
    [SerializeField] GameObject creditPanel;
    [SerializeField] Button creditInitbutton;

    //Menu Ajustes
    [Header("Ajustes")]
    [SerializeField] GameObject ajustesPanel;
    [SerializeField] Button ajustesInitButton;

    private void Start()
    {
        if (SaveManager.Instance.HasSaveData()) 
        {
            continueMenuButton.interactable = true;
        }
    }

    public void ContinueButton() 
    {
        //Carga los datos
        SaveManager.Instance.LoadData();
        //Cambia la escena
        SceneManager.LoadScene(GameDataManager.Instance.gameData.currentSceneName);
    }

    public void NewGame() 
    {
        SceneManager.LoadScene(newGameScene);
    }

    public void CreditButton() 
    {
        menupanel.SetActive(false);
        creditPanel.SetActive(true);
        creditInitbutton.Select();
    }

    public void ReturntoMain() 
    {
        creditPanel.SetActive(false);
        menupanel.SetActive(true);
        menuInitButton.Select();
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void OpenMenu(GameObject menuName) 
    {
        CloseAll();
        menuName.SetActive(true);
    }

    public void CloseAll() 
    {
        creditPanel.SetActive(false);
        menupanel.SetActive(false);
    }
}
