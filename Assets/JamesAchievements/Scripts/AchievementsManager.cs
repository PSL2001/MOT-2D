using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsManager : MonoBehaviour
{


    /*
    * Recommended colors 
    * Background -> #FFFFFF - #B1FFB1 - etc...
    * Text       -> #6A6A6A - #319A31 - etc...
    */
    [Tooltip("Color del fondo para las distintas rarezas")]
    [SerializeField] private Color[] rarityBackgroundColors = new Color[6] { Color.white, Color.green, Color.blue, Color.magenta, Color.red, Color.black };
    [Tooltip("Color del texto para las distintas rarezas")]
    [SerializeField] private Color[] rarityTextColors = new Color[6] { Color.white, Color.green, Color.blue, Color.magenta, Color.red, Color.black };
    [Tooltip("Referencia hexadecimal del color de texto para usar en Text Mesh Pro si se usan iconos")]
    [SerializeField] private string[] rarityTextColorsHex = new string[6] { "#6A6A6A", "#319A31", "#319A31", "#319A31", "#319A31", "#319A31" };
    [SerializeField] private AudioSource unlockAchievementAudio;
    [SerializeField] private Animator achievementAnimator;
    [SerializeField] private Animator achievementsAnimator;
    [SerializeField] private GameObject aSVContent;
    [Min(1)]
    [SerializeField] private float animatorSpeed;
    [SerializeField] private KeyCode achievementsMenuKey;
    [SerializeField] private TextMeshProUGUI achievementText;
    [SerializeField] private Image achievementBackground;
    private bool usingAchievementsMenu;
    [SerializeField] private List<GameObject> achievements;
    private List<GameObject> sA = new List<GameObject>();

    //Modifications
    public int rarity = 1;

    /*
     * Se usan PlayerPrefs para guardar los distintos logros desbloqueados, se puede cambiar a cualquier otro sistema de serialización de datos
     * id -> id del logro
     * Rarity 
     * 0-Comun | 1-PocoComun  | 2-Raro | 3-Epico | 4-Legendario | 5-Mamadisimo
     */
    public void UnlockAchievementComun(string id) => UnlockAchievement(id, 0);

    public void UnlockAchievement(string id, int rarity)
    {
        if (PlayerPrefs.HasKey(id))
            return;
        else
        {
            unlockAchievementAudio.Play();
            achievementBackground.color = rarityBackgroundColors[rarity];
            achievementText.color = rarityTextColors[rarity];
            achievementText.text = "<sprite index=0 color="+ rarityTextColorsHex[rarity]+">"+id+ "<sprite index=0 color=" + rarityTextColorsHex[rarity]+">";
            achievementAnimator.SetTrigger("unlockAchievement");
            PlayerPrefs.SetInt(id, 0);
            for (int i = 0; i < sA.Count; ++i)
            {
                if (PlayerPrefs.HasKey(sA[i].GetComponent<Achievement>().GetAchievementID()))
                {
                    sA[i].GetComponent<Achievement>().Unlock();
                }
            }
        }
    }

    /*
     * Se usan PlayerPrefs para guardar los distintos logros desbloqueados, se puede cambiar a cualquier otro sistema de serialización de datos
     * id -> id del logro
     * Rarity 
     * 0-Comun | 1-PocoComun  | 2-Raro | 3-Epico | 4-Legendario | 5-Mamadisimo
     */
    public void UnlockAchievement(string id)
    {
        if (PlayerPrefs.HasKey(id))
            return;
        else
        {
            PlayerPrefs.SetInt(id, 0);
            for (int i = 0; i < sA.Count; ++i)
            {
                unlockAchievementAudio.Play();
                achievementBackground.color = rarityBackgroundColors[rarity];
                achievementText.color = rarityTextColors[rarity];
                achievementText.text = "<sprite index=0 color=" + rarityTextColorsHex[rarity] + ">" + sA[i].GetComponent<Achievement>().GetTitle() + "<sprite index=0 color=" + rarityTextColorsHex[rarity] + ">";
                achievementAnimator.SetTrigger("unlockAchievement");
                if (PlayerPrefs.HasKey(sA[i].GetComponent<Achievement>().GetAchievementID()))
                {
                    sA[i].GetComponent<Achievement>().Unlock();
                }
            }

        }
    }

    public void ResetArchievements() 
    {
        PlayerPrefs.DeleteAll();
    }
    public void setRarity(int id) 
    {
        rarity = id;
    }

    private void Start()
    {
        achievementsAnimator.SetFloat("speed", animatorSpeed);
        StartCoroutine(ManageAchievementsMenu());
        for (int i=0; i<achievements.Count; ++i)
        {
            GameObject a = Instantiate(achievements[i], aSVContent.transform);
            if(PlayerPrefs.HasKey(a.GetComponent<Achievement>().GetAchievementID())) {
                a.GetComponent<Achievement>().Unlock();
            }
            sA.Add(a);
        }
    }

    private IEnumerator ManageAchievementsMenu()
    {
        while (true)
        {
            while (!usingAchievementsMenu)
            {
                if (Input.GetKeyUp(achievementsMenuKey))
                {
                    Time.timeScale = 0;
                    achievementsAnimator.SetTrigger("open");
                    yield return new WaitForSecondsRealtime(1/animatorSpeed);
                    usingAchievementsMenu = true;
                }
                yield return null;
            }
            while (usingAchievementsMenu)
            {
                if (Input.GetKeyUp(achievementsMenuKey))
                {
                    Time.timeScale = 1;
                    achievementsAnimator.SetTrigger("close");
                    yield return new WaitForSeconds(1/animatorSpeed);
                    usingAchievementsMenu = false;
                }
                yield return null;
            }
            yield return null;
        }
    }

}
