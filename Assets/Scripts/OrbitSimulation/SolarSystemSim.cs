using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolarSystem
{
    public class SolarSystemSim : MonoBehaviour
    {   
		[Header("References")]
		public GameObject planet;

		public float fastForwardDayDuration;
		bool fastForwarding;
		float oldPlayerT;
		float fastForwardTargetTime;
		bool fastForwardApproachingTargetTime;

		//[Header("Debug")]

        void Update()
		{
            if (Input.GetKeyDown("space"))
            {
                Instantiate(planet, new Vector3(0, 0, 0), Quaternion.identity);
            }
			//planet?.UpdateOrbit();
		}
    }
}