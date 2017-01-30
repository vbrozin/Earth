using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherData {
	private string city;
	private string weather;

	public string City {
		get {
			return city;
		}
		set {
			city = value;
		}
	}

	public string Weather {
		get {
			return weather;
		}
		set {
			weather = value;
		}
	}
}
