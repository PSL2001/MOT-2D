using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax1 : MonoBehaviour
{
    // Start is called before the first frame update

    private float longitud, posInicial;


    public GameObject cam;

    public float effectoParallax;
    void Start()
    {
        cam = Camera.main.gameObject;
        posInicial = transform.position.x;
        longitud = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - effectoParallax));

        float distancia = (cam.transform.position.x * effectoParallax);

        transform.position = new Vector3(posInicial + distancia, transform.position.y, transform.position.z);

        if (temp > posInicial + longitud) posInicial += longitud;
        else if (temp < posInicial - longitud) posInicial -= longitud;
    }
}
