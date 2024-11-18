using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShootingManager : MonoBehaviour
{
    public TextMeshProUGUI contadorBalasTexto; // Referencia al componente TextMeshPro

    public List<EnemyShooting> robots; // Lista de todos los robots.
    public List<EnemyShooting> robotsZZ; // Lista de robots en la hilera para izquierda a derecha.

    public float intervaloDisparo = 2f; // Intervalo entre disparos individuales.
    public float cambioModoIntervalo = 10f; // Tiempo para cambiar de modo de disparo.

    private List<GameObject> balasVivas = new List<GameObject>(); // Rastrear las balas vivas.

    private int extremoIzquierdo = 0;
    private int extremoDerecho = 9;

    private int indiceActual = 0; // Índice del robot que dispara.
    private bool disparoIzquierdaADerecha = true; // Dirección del disparo.
    private int modoActual = 0; // Índice del modo de disparo actual.
    private List<System.Action> modosDisparo; // Lista de modos de disparo.

    void Start()
    {
        // Inicializar modos de disparo
        modosDisparo = new List<System.Action>
        {
            DisparoIzquierdaADerecha,
            DisparoRandom,
            DisparoExtremosAlCentro
        };


        // Iniciar los ciclos
        InvokeRepeating("DispararModoActual", 0.5f, intervaloDisparo);
        InvokeRepeating("CambiarModo", cambioModoIntervalo, cambioModoIntervalo);
    }

    void Update()
    {
        if (contadorBalasTexto != null)
        {
            contadorBalasTexto.text = $"Balas vivas: {ContarBalasVivas()}";
        }
    }

    void DispararModoActual()
    {
        // Ejecutar el modo de disparo actual
        modosDisparo[modoActual]();
    }

    void CambiarModo()
    {
        // Cambiar al siguiente modo de disparo
        modoActual = (modoActual + 1) % modosDisparo.Count;
        
        if (modoActual == 1) {
            intervaloDisparo = .25f;
        }
        else if (modoActual == 2)
        {
            intervaloDisparo = 0.75f;
        }
        else
        {
            intervaloDisparo = .5f;
        }
    }

    // Modo 1: Disparo de izquierda a derecha y viceversa
    void DisparoIzquierdaADerecha()
    {
        if (robotsZZ.Count == 0) return;

        // Disparar el robot actual
        robotsZZ[indiceActual].Disparar();

        // Cambiar al siguiente robot
        if (disparoIzquierdaADerecha)
        {
            indiceActual++;
            if (indiceActual >= robotsZZ.Count)
            {
                disparoIzquierdaADerecha = false;
                indiceActual = robotsZZ.Count - 1;
            }
        }
        else
        {
            indiceActual--;
            if (indiceActual < 0)
            {
                disparoIzquierdaADerecha = true;
                indiceActual = 0;
            }
        }
    }

    // Modo 2: Disparo aleatorio
    void DisparoRandom()
    {
        if (robots.Count == 0) return;

        // Elegir un robot aleatorio y disparar
        int indiceAleatorio = Random.Range(0, robots.Count);
        robots[indiceAleatorio].Disparar();
    }


    void DisparoExtremosAlCentro()
    {
        if (robots.Count == 0) return;
        

        // Disparar los robots actuales en los extremos
        if (extremoIzquierdo <= extremoDerecho) robots[extremoIzquierdo].Disparar();
        if (extremoIzquierdo < extremoDerecho) robots[extremoDerecho].Disparar();
        Debug.Log("Dispara: " + extremoDerecho);
        Debug.Log("Dispara: " + extremoIzquierdo);

        // Mover hacia el centro
        extremoIzquierdo++;
        extremoDerecho--;

        // Reiniciar cuando se haya alcanzado el centro
        if (extremoIzquierdo > extremoDerecho)
        {
            extremoIzquierdo = 0;
            extremoDerecho = 9;
        }
    }

    public void RegistrarBala(GameObject bala)
    {
        balasVivas.Add(bala);
    }

    public void EliminarBala(GameObject bala)
    {
        balasVivas.Remove(bala);
    }

    public int ContarBalasVivas()
    {
        return balasVivas.Count;
    }

}
