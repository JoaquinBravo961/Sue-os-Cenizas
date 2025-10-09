using UnityEngine;
using static TipoLLave;

public class Llave : MonoBehaviour
{
    public TipoLlave tipoLlave; // Seleccionás en el Inspector qué llave es

    // Asignamos un color visual según el tipo de llave
    private Color ObtenerColorPorTipo()
    {
        switch (tipoLlave)
        {
            case TipoLlave.Roja: return Color.red;
            case TipoLlave.Azul: return Color.blue;
            case TipoLlave.Verde: return Color.green;
            case TipoLlave.Amarilla: return Color.yellow;
            default: return Color.white;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Busca el componente Inventario en el jugador
            Inventario inv = other.GetComponent<Inventario>();
            if (inv != null)
            {
                inv.AgregarLlave(tipoLlave);

                KeyUIManager.Instance?.AddKey(ObtenerColorPorTipo());

                Destroy(gameObject);
            }
        }
    }
}
