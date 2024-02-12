using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[ExecuteInEditMode]
public class PlayerController : MonoBehaviour, ActorController
{
    //Player Default info
    [Header("Player Info")]
    [SerializeField] public PlayerDataSO playerData;


    //Referencia a Stats
    [SerializeField] PlayerStats stats;

    public SpriteRenderer spriteRenderer;

    //Referencia a PlayerInput
    public PlayerInput playerInput;

    //Eventos
    public UnityEvent onDie = new();
    // Start is called before the first frame update
    void Start()
    {
        playerData?.Initialize(this);

        if (GameObject.FindGameObjectWithTag("GameData"))
        {
            stats = GameObject.FindGameObjectWithTag("GameData").GetComponent<Data>().stats;
        } else
        {
            Debug.Log("Error! No se encuentra el objeto con el tag de GameData!!!!!!!!");
        }

        playerInput = GameObject.FindGameObjectWithTag("PlayerInput").GetComponent<PlayerInput>();
        //Me suscribo a los cambios de HP de los stats
        stats.HP.RestartStats();
        stats.HP.onPercentChange.AddListener(onHPUpdate);

    }

    #region Init Editor
    private void OnRenderObject()
    {
        playerData?.Initialize(this);
    }
    #endregion

    private void onHPUpdate(float val)
    {
        if (val <= 0)
        {
            onDie.Invoke();

        }
    }

    public Stats GetStats()
    {
        return stats;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void OnHealing(float healAmmount)
    {
        throw new System.NotImplementedException();
    }

    public void OnDamage(float damageAmmount)
    {
        if(stats.invulnerable) return;

        stats.HP.CurrentValue -= damageAmmount;

        //Lanzar corutina de invulnerabilidad
        if (stats.HP.CurrentValue > 0) StartCoroutine(ApplyInvulneravility2D(stats.invulnerabilitySeconds));
    }

    public void ApplyTempInvulnerabilityWithotColor2D(float seconds)
    {
        StartCoroutine(TemporalInvulnerabilityWithoutColorCoroutine2D(seconds));
    }


    private IEnumerator TemporalInvulnerabilityWithoutColorCoroutine2D(float seconds)
    {
        stats.invulnerable = true;
        yield return new WaitForSecondsRealtime(seconds);
        stats.invulnerable = false;
    }

    public IEnumerator ApplyInvulneravility2D(float seconds)
    {
        //1.1 Activar la invulnerabilidad desactivando las capas (Layer)
        stats.invulnerable = true;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemies"), true);
        //1.2 Hacer cambios en el sprite para que se vea visualmente la invulnerabilidad. Rojo
        Color colorBase = spriteRenderer.color;

        spriteRenderer.color = stats.invulnerabilityColor;
        //2. Esperar el tiempo de invulnerabilidad
        yield return new WaitForSecondsRealtime(seconds);
        //3.1 Deshacemos los cambios de sprite
        spriteRenderer.color = colorBase;
        //3.2 Deshacemos los cambios de los Layers a su estado original
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemies"), false);

        stats.invulnerable = false;
        yield return null;
    }



}
