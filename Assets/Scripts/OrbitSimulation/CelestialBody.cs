using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialBody : MonoBehaviour
{
    public float mass;
    public float radius;
    public Vector3 initialVelocity;
    Vector3 currentVelocity;

    void Awake() {
        currentVelocity = initialVelocity;    
    }

    public void UpdateVelocity(CelestialBody[] allBodies, float timeStep) {
        foreach(var otherBody in allBodies) {
            if(otherBody != this) {
                float sqrDst = (otherBody.GetComponent<Rigidbody>().position - GetComponent<Rigidbody>().position).sqrMagnitude;
                Vector3 forceDir = (otherBody.GetComponent<Rigidbody>().position - GetComponent<Rigidbody>().position);
                Vector3 force = forceDir * (Universe.gravitationalConstant * (otherBody.mass * mass) / sqrDst);
                Vector3 acc = force / mass;
                currentVelocity = acc * timeStep;
            }
            
        }
    }

    public void UpdatePosition(float timeStep) {
        GetComponent<Rigidbody>().position += currentVelocity * timeStep;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
