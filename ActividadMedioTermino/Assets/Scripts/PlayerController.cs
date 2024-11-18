using System.Collections;
using UnityEngine;
using TMPro; // Para el mensaje de Ganaste

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f; // Velocidad normal
    public float slowSpeed = 2.5f; // Velocidad lenta (Shift)
    public GameObject proyectilPrefab; // Prefab de la bala que dispara el jugador
    public Transform puntoDeDisparo; // Posición desde donde se disparan las balas
    public string inputId; // Identificador para inputs (por ejemplo, "Player1")
    public TextMeshProUGUI mensajeTexto; // Texto para mostrar mensajes en pantalla

    private bool juegoTerminado = false; // Para verificar si el juego ya terminó
    private float tiempoInicio; // Para rastrear el tiempo desde el inicio

    void Start()
    {
        tiempoInicio = Time.time; // Registrar el tiempo al inicio del juego
        if (mensajeTexto != null)
        {
            mensajeTexto.text = ""; // Asegurarse de que el mensaje esté vacío al principio
        }
    }

    void Update()
    {
        if (juegoTerminado) return; // No hacer nada si el juego terminó

        // Mover al personaje en el eje X
        float horizontalInput = Input.GetAxis("Horizontal" + inputId);
        float velocidadActual = Input.GetKey(KeyCode.LeftShift) ? slowSpeed : speed; // Cambiar velocidad con Shift
        transform.Translate(Vector3.right * horizontalInput * velocidadActual * Time.deltaTime);

        // Disparar con la tecla Espacio
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Disparar();
        }

        // Verificar si ha pasado un minuto
        if (Time.time - tiempoInicio >= 60f)
        {
            MostrarMensaje("¡Ganaste!");
            TerminarJuego();
        }
    }

    void Disparar()
    {
        // Crear la bala en el punto de disparo
        if (proyectilPrefab != null && puntoDeDisparo != null)
        {
            Instantiate(proyectilPrefab, puntoDeDisparo.position, puntoDeDisparo.rotation);
        }
    }

    void MostrarMensaje(string mensaje)
    {
        if (mensajeTexto != null)
        {
            mensajeTexto.text = mensaje; // Mostrar el mensaje en pantalla
        }
    }

    void TerminarJuego()
    {
        juegoTerminado = true; // Detener todas las actualizaciones
        Time.timeScale = 0; // Detener el tiempo (opcional)
    }

    void OnCollisionEnter(Collision collision)
    {
        // Detectar si la colisión es con una bala
        if (collision.gameObject.CompareTag("Bala"))
        {
            MostrarMensaje("¡Perdiste!");
            TerminarJuego();
        }
    }

}
