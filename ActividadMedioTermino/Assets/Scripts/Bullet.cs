using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private ShootingManager manager;
    public float velocidad = 50f; // Velocidad de la bala

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = transform.forward * velocidad; // Mueve la bala hacia adelante en el eje Z
        }

        // Encontrar el controlador central (asegúrate de que haya un ShootingManager en la escena)
        manager = FindObjectOfType<ShootingManager>();

        // Registrar la bala en el controlador
        if (manager != null)
        {
            manager.RegistrarBala(gameObject);
        }
    }


    void OnDestroy()
    {
        // Eliminar la bala del controlador
        if (manager != null)
        {
            manager.EliminarBala(gameObject);
        }
    }

}
