using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float velocidad = 50; // Velocidad de la bala
    public float tiempoVida = 2f; // Tiempo de vida de la bala

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = transform.forward * velocidad; // Mueve la bala hacia adelante en el eje Z
        }
        Destroy(gameObject, tiempoVida);
    }


    

}
