using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateFilledImageUI : MonoBehaviour
{
    //Referencia a imagen
    Image img;
    // Start is called before the first frame update
    void Awake()
    {
        img = GetComponent<Image>();
    }

    //Metodo para actualizar interfaz
    public void UpdateFilledImage(float value)
    {
        img.fillAmount = value;
    }
}
