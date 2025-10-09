using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New speacker", menuName = "Data/New speacker")]
[System.Serializable] 
public class speacker : ScriptableObject
{
    public string speakerName;
    public Color textColor;

    public List<Sprite> sprites;
    public SpriteController prefab;
}
 