using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[ExecuteInEditMode]
public class EnemiesController : MonoBehaviour, ActorController
{
    [Header("Enemy Info")]
    [SerializeField] public EnemyDataSO enemyData;

    public EnemyStats stats;

    [Header("Eventos Generales")]
    public UnityEvent onDie = new();
    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public Stats GetStats()
    {
        return stats;
    }

    public void OnDamage(float damageAmmount)
    {
        stats.HP.CurrentValue -= damageAmmount;

        
    }

    public void OnHealing(float healAmmount)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyData?.Initialize(this);
        //Me suscribo a los cambios de HP de los stats
        stats.HP.OnIndicatorChange.AddListener(onHPUpdate);
    }

    private void onHPUpdate(float val)
    {
        if (val <= 0) 
        {
            onDie.Invoke();
            //Destroy(gameObject, 1);
        }
    }

    private void OnRenderObject()
    {
        enemyData?.Initialize(this);
    }
}
