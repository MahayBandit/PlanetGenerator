using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolarSystem
{
    public class SolarSystemSimulation : MonoBehaviour
    {   
		[Header("References")]
		public GameObject planet;

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