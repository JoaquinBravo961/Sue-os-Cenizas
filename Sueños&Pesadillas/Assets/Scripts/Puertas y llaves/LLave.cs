using UnityEngine;
using static TipoLLave;

public class Llave : MonoBehaviour
{
    public TipoLlave tipoLlave; // Seleccionás en el Inspector qué llave es

    //Si el jugador entra en contacto, agrega la llave al inv
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Busca el componente Inventario en el jugador
            Inventario inv = other.GetComponent<Inventario>();
            if (inv != null)
            {
                // Agrega la llave al inventario del jugador
                inv.AgregarLlave(tipoLlave);
                Destroy(gameObject);
            }
        }
    }
}

