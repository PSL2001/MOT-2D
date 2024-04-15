using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnparentMe : MonoBehaviour
{
    // Start is called before the first frame update
    public void Unparent() { transform.SetParent(null); DestroyMe(); }

    public void DestroyMe() { Destroy(this.gameObject, GetComponent<ParticleSystem>().main.duration);}
}
