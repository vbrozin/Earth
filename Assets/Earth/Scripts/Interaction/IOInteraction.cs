using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IOInteraction : MonoBehaviour {

	[SerializeField] private Camera cam;

	[SerializeField] private Transform earth;

	//TODO : move on a view
	[SerializeField] private Text displayer;

	void Update()
	{
		// Trigerred when the user press a button
		if(Input.GetButtonDown("Fire1"))
		{
			Ray r = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;
			if (Physics.Raycast(r, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Earth")))
			{
				// Get cartesian coordinates
				Vector3 cartesianCoord = earth.InverseTransformPoint(hitInfo.point).normalized;
				// Compute spherical coordinates
				Vector2 sphericalCoord = CoordinateFinder.CartesianToSphericalCoordinates(cartesianCoord);
				// Process
				string location = RestApiInterface.GoogleAPIProcessCoord(sphericalCoord);
				//WeatherData weatherData = RestApiInterface.WeatherMapProcessCoord (sphericalCoord);
				// Display resul
				displayer.text = location;
			}
		}
		// Escape -> quit 
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}
	}
}
