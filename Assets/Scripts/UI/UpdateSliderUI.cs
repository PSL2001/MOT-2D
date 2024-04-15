using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateSliderUI : MonoBehaviour
{
    //Referencia a slider
    Slider slider;
    // Start is called before the first frame update
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    //Metodo para actualizar interfaz
    public void UpdateSlider(float value)
    {
        slider.value = value;
    }
}
