using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolarSystem
{
	public class PlanetOrbit : MonoBehaviour
	{

		public Quaternion planetRot { get; private set; }
		//public Vector3 planetPos { get; private set; }
		public float currentAxisAngle { get; private set; }
		
		[Header("Orbit parameters")]
		public float orbitAngle = 0.1f;
		public float periapis = 147.2f;
		public float apoapsis = 152.1f;
		public float tilt = 23.4f;
		public float distanceScale = 1;

		[Header("Durations")]
		// Allow flexible day/month/year durations since real timescales are a bit slow...
		public float dayDurationMinutes;
		public float monthDurationMinutes;
		public float yearDurationMinutes;

		[Header("Time state")]
		[Range(0, 1)]
		public float dayT;
		[Range(0, 1)]
		public float monthT;
		[Range(0, 1)]
		public float yearT;
		

		[Header("Debug")]
		public float debug_dst;

		public void UpdateOrbit()
		{
			float daySpeed = 1 / (dayDurationMinutes * 60);

			dayT += daySpeed * Time.deltaTime;
			monthT += 1 / (monthDurationMinutes * 60) * Time.deltaTime;
			yearT += 1 / (yearDurationMinutes * 60) * Time.deltaTime;

			dayT %= 1;
			monthT %= 1;
			yearT %= 1;

			Vector3 xAxis = new Vector3(Mathf.Cos(orbitAngle * Mathf.Deg2Rad), Mathf.Sin(orbitAngle * Mathf.Deg2Rad), 0);
			Vector3 yAxis = Vector3.forward;

			Vector2 orbitEllipse = Orbit.CalculatePointOnOrbit(periapis, apoapsis, yearT);
			Vector3 planetPos = (xAxis * orbitEllipse.x + yAxis * orbitEllipse.y) * distanceScale;
			debug_dst = orbitEllipse.magnitude;

			float siderealDayAngle = -dayT * 360;
			float solarDayAngle = siderealDayAngle - yearT * 360;
			currentAxisAngle = solarDayAngle;

			planetRot = Quaternion.Euler(0, 0, -tilt) * Quaternion.Euler(0, currentAxisAngle, 0);

			transform.position = planetPos;
			transform.rotation = planetRot;
		}

		void Update()
		{
			UpdateOrbit();
		}
	}
}