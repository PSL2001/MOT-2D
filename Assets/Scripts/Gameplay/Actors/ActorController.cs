using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ActorController
{
    //Stats
    public Stats GetStats();

    //Referencia
    public GameObject GetGameObject();

    //Metodo Gestion HP
    public void OnHealing(float healAmmount);
    public void OnDamage(float damageAmmount);
}
