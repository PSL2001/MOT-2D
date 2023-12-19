using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class MovePlatform : MonoBehaviour
{
    [Header("Plataform Movement Parameters")]
    [SerializeField] float speed;

    int nextPosition;
    [SerializeField] List<Vector3> positions;

    //Eventos
    public UnityEvent onActionEnd;

    private void Start()
    {
        //Rellenar con la ruta
        Transform patrolGD = gameObject.transform.Find("Patrol");

        for (int i = 0; i < patrolGD.childCount; i++)
        {
            positions.Add(patrolGD.GetChild(i).position);
        }
        positions.Add(transform.position);

        //inicializo la posicion de destino
        nextPosition = 0;
    }

    public void Use()
    {
        StartCoroutine(MovetoNextPosition());
    }

    private IEnumerator MovetoNextPosition()
    {
        while (transform.position != positions[nextPosition])
        {
            transform.position = Vector3.MoveTowards(transform.position, positions[nextPosition], speed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
        nextPosition = (nextPosition + 1) % positions.Count;
        onActionEnd.Invoke();
    }
}
