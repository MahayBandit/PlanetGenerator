using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShapeSettings", menuName = "Planet Generator/ShapeSettings", order = 0)]
public class ShapeSettings : ScriptableObject 
{
    public float planetRadius = 1f;
    public NoiseSettings noiseSettings;
}