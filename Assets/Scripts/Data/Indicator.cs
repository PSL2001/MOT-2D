using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class Indicator
{
    //Info
    public String indicatorName;
    public Image icon;

    //Datos
    [SerializeField] 
    private float currentValue;
    public float maxValue;
    public float initValue;

    //Gestion recuperación/deterioro automatico
    public bool enableAutoUpdate=true;
    public float autoUpdateRate = 0;

    //Eventos para detectar cambios en el indicador. (mas info en https://gamedevbeginner.com/events-and-delegates-in-unity)
    //Opción 1. Unity Event Facil enlazar elementos en el Editor.
    public UnityEvent<float> onPercentChange;
    public UnityEvent<float> onValueChange;


    //Opción 2. Delegate. Facil pasar parámetros asociados al evento.
    //public delegate void OnIndicatorChangeDelegate(float newValue);
    //public event OnIndicatorChangeDelegate OnIndicatorChange;




    //Constructor y recuperación/deterioro automatico
    public Indicator() {
        UpdateCaller.OnUpdate += Update;
    }

    ~Indicator()
    {
        UpdateCaller.OnUpdate -= Update;
    }

    //Gestion de restauracion/deterioro
    void Update()
    {
        if (enableAutoUpdate && autoUpdateRate != 0) this.CurrentValue += autoUpdateRate * Time.deltaTime;
    }

    //Inicialización
    public virtual void RestartStats()
    {
        CurrentValue = initValue;
        //autoUpdateRate = 0;
    }

    //Operaciones
    public float GetPercentage()
    {
        return CurrentValue / maxValue;
    }

    //Get y Set publico
    public float CurrentValue
    {
        get => currentValue;
        set
        {
            float newValue = Mathf.Clamp(value, 0.0f, maxValue);
            
            if(newValue != currentValue)
            {
                currentValue = newValue;
                //1. Unity Event
                try
                {
                    onPercentChange?.Invoke(GetPercentage());
                    onValueChange?.Invoke(currentValue);
                }
                catch (Exception e)
                {
                    Debug.Log("Ha fallado un evento enganchado a Indicator");
                    Debug.Log(e);
                }

            }


            //2. Delegate
            //if (OnIndicatorChange != null)
            //    OnIndicatorChange(value);
        }
    }

    public void Update(Indicator newIndicator)
    {
        indicatorName = newIndicator.indicatorName;
        currentValue = newIndicator.currentValue;
        initValue = newIndicator.initValue;
        maxValue = newIndicator.maxValue;
        enableAutoUpdate = newIndicator.enableAutoUpdate;
        autoUpdateRate = newIndicator.autoUpdateRate;
    }

}
