using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject proyectilPrefab;
    public Transform puntoDeDisparo;
    public float velocidadProyectil = 50f;
    public float tiempoVidaProyectil = 4f;

    public void Disparar()
    {
        // Crear el proyectil
        GameObject proyectil = Instantiate(proyectilPrefab, puntoDeDisparo.position, puntoDeDisparo.rotation);
        Rigidbody rb = proyectil.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = puntoDeDisparo.forward * velocidadProyectil;
        }
        Destroy(proyectil, tiempoVidaProyectil);
    }
}
