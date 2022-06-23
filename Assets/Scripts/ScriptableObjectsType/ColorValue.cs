using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newColorData", menuName = "Color")]
public class ColorValue : ScriptableObject
{    
    
    public Color baseColor;
    
    public BoolValue colorSaved;
}
