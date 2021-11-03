using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueJugador : MonoBehaviour
{
    public bool Ataque;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Enemy"))
        {
            print("DanoEnemigo");
        }
    }
}
