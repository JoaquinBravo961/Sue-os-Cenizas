using UnityEngine;

public class SubWeapons : MonoBehaviour
{
    public int AmmoCost = 1;               // Cu�nto "ammo" cuesta curarse
    public int healAmount = 1;             // Cu�nta vida se recupera
   
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
            // Si la vida ya est� al m�ximo, no hace nada
            if (saludSystem.vidaActual >= saludSystem.vidaMaxima)
            {
                // Opcional: sonido o mensaje de que la vida est� llena
                 Debug.Log("Vida al m�ximo, no se puede curar.");
                return;
            }

            // Consume la munici�n
            Ammo.instance.SubItem(-AmmoCost);

            // Cura la vida sin pasar el m�ximo
            saludSystem.vidaActual = Mathf.Min(saludSystem.vidaActual + healAmount, saludSystem.vidaMaxima);

            // Opcional: actualizar el coraz�n manualmente si no se actualiza autom�ticamente
            // saludSystem.ActualizarCorazon(); // si el m�todo fuera p�blico
        }
    }
}
