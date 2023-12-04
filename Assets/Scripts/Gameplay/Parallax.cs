using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    //Hay que obtener la referencia de la camara, hacemos una variable publica
    public Camera Camera;
    // Un valor dado que se le da a la imagen del paralax, tambien pública para los layers, esto nos ayuda a dictar la velocidad y distancia del sprite de fondo
    public float parallax_value;

    //Un vector con la posicion inicial
    private Vector3 startPosition;
    // Un Vector con la longitud para el calculo de las imagenes
    private Vector2 length;
    // Start is called before the first frame update
    void Start()
    { // Comienzo Start
        //Primero obtenemos la posicion de donde está nuestra imagen
        startPosition = transform.position;
        //Luego obtenemos la longitud de la imagen que estamos renderizando en el fondo
        length = GetComponentInChildren<SpriteRenderer>().bounds.size;
    } //Fin Start

    // Update is called once per frame
    void Update()
    { // Comienzo Update
        /*
         * Como tenemos que estar mirando cada frame a que distancia está el fondo del personaje debemos hacerlo en el Update
         * Lo que hacemos primero es obtener la posicion relativa que obtenemos multiplicando como un Vector de 3 Dimensiones de la posicion de la camara con
         * el valor del parallax
         */
        Vector3 relativePosition = Camera.transform.position * parallax_value;
        // Con esta posicion relativa, si se lo restamos a la posicion actual de la cámara tenemos la distancia que hay entre los 2 objetos (Camara y Sprite)
        Vector3 dist = Camera.transform.position - relativePosition;
        //A partir de aqui es simplemente comprobar si la distancia de x es mayor o menor a la SUMA de la posicion en la que empezó y su longitud
        if (dist.x > startPosition.x + length.x) {
            //Si es mayor, entonces simplemente sumamos la posicion inicial del sprite consigo misma y la longitud
            startPosition.x += length.x;
        }
        //Si en otro caso la distancia es menor a la RESTA de la posicion inicial con la distancia
        if (dist.x < startPosition.x - length.x)
        {
            //Entonces simplemente restamos la posicion inicial consigo misma y la longitud
            startPosition.x -= length.x;
        }
        //Una vez que hemos comprobado la distancia de x, igualamos la posicion Z para evitar descuadres de la relativa con la inicial
        relativePosition.z = startPosition.z;
        //Y ya por ultimo la posicion inicial se iguala a la suma de startPosition (que es la posicion inicial CUANDO ARRANCO EL PROGRAMA) con la posicion relativa
        transform.position = startPosition + relativePosition;
        
    } // Fin Update
}
