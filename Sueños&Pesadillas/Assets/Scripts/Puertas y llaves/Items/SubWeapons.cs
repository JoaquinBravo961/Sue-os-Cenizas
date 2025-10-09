using UnityEngine;

public class SubWeapons : MonoBehaviour
{
    public int AmmoCost = 1;               // Cuánto "ammo" cuesta curarse
    public int healAmount = 1;             // Cuánta vida se recupera
   
    private SaludSystem saludSystem;       // Referencia al sistema de salud

    void Start()
    {
        // Busca el componente SaludSystem en el mismo objeto
        saludSystem = GetComponent<SaludSystem>();
    }

    void Update()
    {
        UseHeal();
    }

    public void UseHeal()
    {
        // Verifica que haya salud y ammo antes de intentar curar
        if (Input.GetKeyDown(KeyCode.C) && saludSystem != null && AmmoCost <= Ammo.instance.AmmosAmount)
        {
            // Si la vida ya está al máximo, no hace nada
            if (saludSystem.vidaActual >= saludSystem.vidaMaxima)
            {
                // Opcional: sonido o mensaje de que la vida está llena
                 Debug.Log("Vida al máximo, no se puede curar.");
                return;
            }

            // Consume la munición
            Ammo.instance.SubItem(-AmmoCost);

            // Cura la vida sin pasar el máximo
            saludSystem.vidaActual = Mathf.Min(saludSystem.vidaActual + healAmount, saludSystem.vidaMaxima);

            // Opcional: actualizar el corazón manualmente si no se actualiza automáticamente
            // saludSystem.ActualizarCorazon(); // si el método fuera público
        }
    }
}
