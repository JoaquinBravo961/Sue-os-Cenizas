using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class KeyUIManager : MonoBehaviour
{

    public static KeyUIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    [Header("Prefab del icono de llave")]
    public GameObject keyIconPrefab;

    [Header("Contenedor de los iconos")]
    public Transform keyContainer;

    private List<GameObject> keyIcons = new List<GameObject>();
    

    // Añadir una llave (con color)
    public void AddKey(Color keyColor)
    {
        GameObject icon = Instantiate(keyIconPrefab, keyContainer);
        Image iconImage = icon.GetComponent<Image>();
        iconImage.color = keyColor;
        keyIcons.Add(icon);
    }

    // Quitar todas las llaves (por ejemplo, al cambiar de escena)
    public void ClearKeys()
    {
        foreach (var icon in keyIcons)
        {
            Destroy(icon);
        }
        keyIcons.Clear();
    }
    public void RemoveKey(Color keyColor)
    {
        for (int i = 0; i < keyIcons.Count; i++)
        {
            Image iconImage = keyIcons[i].GetComponent<Image>();
            if (iconImage.color == keyColor)
            {
                Destroy(keyIcons[i]);
                keyIcons.RemoveAt(i);
                break;
            }
        }
    }

}
