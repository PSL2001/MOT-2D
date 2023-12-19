using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeManager : MonoBehaviour
{
    Volume volume;
    Vignette vignette;

    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out vignette);
    }

    public void EnableVignette()
    {
        vignette.active = true;
        vignette.intensity.Override(1f);
    }

    public void EnableVignette(float second)
    {
        vignette.active = true;
        StartCoroutine(ActivateFilter(vignette.intensity, 0f, 1f, second));
        //StartCoroutine(ActivateVignetteFilter(second));
    }


    private IEnumerator ActivateFilter(ClampedFloatParameter volumeFloat, float initValue, float endValue, float seconds)
    {
        //Inicializaciones
        volumeFloat.Override(initValue);

        //Valores de control
        float elapsedTime = 0f;

        //Bucle
        while (elapsedTime < seconds)
        {
            float t = elapsedTime / seconds;

            vignette.intensity.Override(Mathf.Lerp(initValue, endValue, t));

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        // Comprobación de valor final
        vignette.intensity.Override(endValue);
    }


    public void DisableVignette()
    {
        vignette.active = false;
        vignette.intensity.Override(0f);
    }
}
