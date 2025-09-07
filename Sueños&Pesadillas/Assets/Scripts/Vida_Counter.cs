using UnityEngine;

[CreateAssetMenu(fileName = "Vida_Counter", menuName = "Scriptable Objects/Vida_Counter")]
public class Vida_Counter : ScriptableObject
{
    public int vidaMaxima = 3;
    public int vidaActual = 3;

    public void ResetVida()
    {
        vidaActual = vidaMaxima;
    }
}
