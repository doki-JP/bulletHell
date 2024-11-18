using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public Vector3 startLeft = new Vector3(-103f, -30f, -119f); // Posición inicial a la izquierda bajo tierra
    public Vector3 startRight = new Vector3(-18f, 2.78f, -119f); // Posición inicial a la derecha
    public Vector3 sizeIncrease = new Vector3(2f, 10000f, 2f); // Tamaño extra para la fase 2
    public Vector3 finalPosition = new Vector3(-59.4f, 2.78f, -92f); // Posición final arriba y atrás
    public Vector3 startPosition = new Vector3(-59.4f, 2.78f, -119f); // Posición final arriba y atrás

    public float horizontalSpeed = 2.0f; // Velocidad de movimiento horizontal
    public float verticalSpeed = 50.0f; // Velocidad de movimiento vertical en fase 2
    public float sizeGrowSpeed = 1.0f; // Velocidad de crecimiento en fase 2

    private int currentPhase = 1; // Fase actual
    private Vector3 targetPosition; // Posición objetivo
    private float phaseStartTime; // Tiempo al inicio de cada fase
    private Vector3 initialScale; // Escala inicial del jefe
    private bool movingRight = true; // Controla si se mueve a la derecha

    void Start()
    {
        // Configurar posición inicial bajo tierra y escala
        transform.position = startLeft;
        initialScale = transform.localScale;
        phaseStartTime = Time.time;
    }

    void Update()
    {
        float elapsedTime = Time.time - phaseStartTime;

        // Mover bajo tierra durante los primeros 29 segundos
        if (Time.time < 30f)
        {
            transform.position = new Vector3(transform.position.x, -30f, transform.position.z);
            return;
        }

        // Subir a la coordenada Y = 2.78 al iniciar el comportamiento
        if (Time.time >= 30f && Time.time < 40)
        {
            transform.position = new Vector3(transform.position.x, 2.78f, transform.position.z);
            currentPhase = 1; // Pasar a la fase de moverse horizontalmente
            phaseStartTime = Time.time;
        }

        // Cambiar de fase según el tiempo transcurrido
        if (currentPhase == 1 && Time.time >= 40f && Time.time < 50) // 29s + 10s
        {
            currentPhase = 2; // Pasar a la fase de agrandarse y moverse verticalmente
            phaseStartTime = Time.time;
        }
        else if (currentPhase == 2 && Time.time >= 50f)
        {
            currentPhase = 3; // Pasar a la fase final
            phaseStartTime = Time.time;
        }

        // Ejecutar el comportamiento de la fase actual
        switch (currentPhase)
        {
            case 1:
                MoverIzquierdaDerecha();
                break;
            case 2:
                AgrandarseYSubirBajar();
                break;
            case 3:
                MoverAFinal();
                break;
        }
    }

    void MoverIzquierdaDerecha()
    {
        // Mover entre las posiciones iniciales izquierda y derecha
        targetPosition = movingRight ? startRight : startLeft;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, horizontalSpeed * Time.deltaTime);

        // Cambiar de dirección al alcanzar los límites
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            movingRight = !movingRight;
        }
    }

    void AgrandarseYSubirBajar()
    {
        // Agrandar el jefe hacia el tamaño objetivo
        Vector3 targetScale = initialScale + sizeIncrease;
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, sizeGrowSpeed * Time.deltaTime);

        transform.position = startPosition;

        // Moverse verticalmente en un patrón sinusoidal
        float newY = Mathf.Sin(Time.time * verticalSpeed) * 9.0f; // Oscila entre -2 y 2
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    void MoverAFinal()
    {
        // Moverse hacia la posición final
        transform.position = Vector3.MoveTowards(transform.position, finalPosition, horizontalSpeed * Time.deltaTime);

        // Ajustar escala para reducir ligeramente si no está en la posición final
        Vector3 finalScale = initialScale + sizeIncrease * 0.8f;
        transform.localScale = Vector3.Lerp(transform.localScale, finalScale, sizeGrowSpeed * Time.deltaTime);
    }
}
