using System.Collections.Generic;
using UnityEngine;
using static TipoLLave;

public class Inventario : MonoBehaviour
{
    // Lista de llaves que posee el jugador
    public List<TipoLlave> llaves = new List<TipoLlave>();


    //Agrega una llave al inventario si no la tiene ya
    public void AgregarLlave(TipoLlave tipo)
    {
        if (!llaves.Contains(tipo))
        {
            llaves.Add(tipo);
            Debug.Log("Llave obtenida: " + tipo);
        }
    }
    // Verifica si el inventario contiene una llave de un tipo
    public bool TieneLlave(TipoLlave tipo)
    {
        return llaves.Contains(tipo);
    }
    // Usa (elimina) una llave del inventario si la tiene
    public void UsarLlave(TipoLlave tipo)
    {
        if (llaves.Contains(tipo))
        {
            llaves.Remove(tipo);
            Debug.Log("Llave usada: " + tipo);
        }
    }
}

