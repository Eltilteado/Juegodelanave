using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    [SerializeField] private float speed = 4.0f;           // Velocidad constante de caída/avanzado
    [SerializeField] private float rotationSpeed = 45.0f;   // Velocidad de giro para efecto visual
    [SerializeField] private bool moveTowardsPlayer = true; // Si persigue a la nave o cae libremente

    private Rigidbody2D rb;
    private Transform playerTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Configuración para evitar físicas pasivas
        if (rb != null)
        {
            rb.gravityScale = 0f; // Quitamos gravedad por si interfiere
        }

        // Buscar a la nave en la escena
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }

        // Dirección inicial
        Vector2 direction = Vector2.down; // Por defecto hacia abajo

        if (moveTowardsPlayer && playerTransform != null)
        {
            // Apuntar directamente hacia la posición de la nave
            direction = (playerTransform.position - transform.position).normalized;
        }

        // Asignar velocidad inmediata al Rigidbody2D
        if (rb != null)
        {
            rb.linearVelocity = direction * speed;
            rb.angularVelocity = Random.Range(-rotationSpeed, rotationSpeed);
        }
    }

    void Update()
    {
        // Si no tiene Rigidbody2D, forzar movimiento por Transform
        if (rb == null)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }
    }
}