using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {

    public GameObject planet;
    void Update() {
        transform.LookAt(planet.transform);
    }
}
