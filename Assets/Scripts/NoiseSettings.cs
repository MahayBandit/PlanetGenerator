using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NoiseSettings", menuName = "Planet Generator/NoiseSettings", order = 0)]
public class NoiseSettings
{
    public float strength = 1f;
    [Range(1, 8)]
    public int numLayers = 1;
    public float baseRoughness = 1f;
    public float roughness = 2f;
    public float persistence = .5f;
    public Vector3 offset;
    
}
