using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{

    public GameObject plataforma;
    public Transform posicion_inicial;
    public Transform posicion_final;
    private Transform posicion_siguiente;
    public float velocidad;
    
    // Start is called before the first frame update
    void Start()
    {
        posicion_siguiente = posicion_final;
    }

    // Update is called once per frame
    void Update()
    {
        plataforma.transform.position = Vector2.MoveTowards(plataforma.transform.position, posicion_siguiente.position, Time.deltaTime * velocidad);
       
        if (plataforma.transform.position == posicion_siguiente.position)
        {
            posicion_siguiente = posicion_siguiente == posicion_final ? posicion_inicial : posicion_final;
        }
    }
}
