using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface NoiseFilter {
    float Evaluate(Vector3 point);
}