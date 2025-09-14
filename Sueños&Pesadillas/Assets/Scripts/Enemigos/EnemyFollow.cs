using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;           // Referencia al jugador
    public float detectionRadius = 5f; // Radio de detección
    public float speed = 2f;           // Velocidad de movimiento
    public LayerMask obstacleMask;     // Capa de obstáculos (así no la dejas fija en "Obstaculos")

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool enMovimiento;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Movimiento();
    }

    private void Movimiento()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRadius)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            // Raycast para detectar si hay obstáculo directo hacia el jugador
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionRadius, obstacleMask);
            if (hit.collider != null)
            {
                // Si hay una pared, prueba direcciones alternativas
                Vector2 perpendicular = Vector2.Perpendicular(direction);

                Vector2 tryDir1 = (direction + perpendicular).normalized;
                Vector2 tryDir2 = (direction - perpendicular).normalized;

                if (!Physics2D.Raycast(transform.position, tryDir1, 1f, obstacleMask))
                    direction = tryDir1;
                else if (!Physics2D.Raycast(transform.position, tryDir2, 1f, obstacleMask))
                    direction = tryDir2;
                else
                    direction = Vector2.zero; // No puede avanzar
            }

            // Flip visual (opcional: si tu sprite necesita girar solo en X)
            if (direction.x < 0)
                transform.localScale = new Vector3(1, 1, 1);
            else if (direction.x > 0)
                transform.localScale = new Vector3(-1, 1, 1);

            movement = direction;
            enMovimiento = true;
        }
        else
        {
            movement = Vector2.zero;
            enMovimiento = false;
        }

        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        // Dibuja el radio de detección en la escena
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
