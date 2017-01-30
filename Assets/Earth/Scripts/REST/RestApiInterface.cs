using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

using UnityEngine;


public class RestApiInterface : MonoBehaviour
{

	/// <summary>
	/// Processes the coordinate.
	/// Get adresse from spherical coordinates
	/// </summary>
	/// <param name="sphericalCoord">Spherical coordinate.</param>
	public static string GoogleAPIProcessCoord (Vector2 sphericalCoord)
	{
		string result = "";
		Debug.Log ("Process coord : " + sphericalCoord.ToString ());
		XmlDocument xDoc = new XmlDocument ();
		String coordinate = sphericalCoord.x + "," + sphericalCoord.y;

		ServicePointManager.ServerCertificateValidationCallback = MyRemoteCertificateValidationCallback;

		xDoc.Load ("https://maps.googleapis.com/maps/api/geocode/xml?latlng=" + coordinate + "&key=AIzaSyBRMo3Z2Dur1c1pzlfwsIB0yH3r85eTRLc");
		XmlNodeList xNodelst = xDoc.GetElementsByTagName ("result");
		XmlNode xNode = xNodelst.Item (0);

	
		if (xNode != null) {
			//if (xNode.SelectSingleNode ("formatted_address") != null) {
			//	string adress = xNode.SelectSingleNode ("formatted_address").InnerText;
			//	result += adress;
			//}
			if (xNode.SelectSingleNode ("address_component[3]/long_name") != null) {
				string mahalle = xNode.SelectSingleNode ("address_component[3]/long_name").InnerText;
				result += mahalle;
			}
			if (xNode.SelectSingleNode ("address_component[4]/long_name") != null) {
				string ilce = xNode.SelectSingleNode ("address_component[4]/long_name").InnerText;
				result += ", " + ilce;
			}
			if (xNode.SelectSingleNode ("address_component[5]/long_name") != null) {
				string il = xNode.SelectSingleNode ("address_component[5]/long_name").InnerText;
				result += ", " + il;
			}
		}
		return result;
	}

	public static WeatherData WeatherMapProcessCoord (Vector2 sphericalCoord)
	{
		WeatherData result = new WeatherData();
		Debug.Log ("Process coord : " + sphericalCoord.ToString ());
		XmlDocument xDoc = new XmlDocument ();
		String coordinate = "lat=" + sphericalCoord.x + "&long=" + sphericalCoord.y;
	
		ServicePointManager.ServerCertificateValidationCallback = MyRemoteCertificateValidationCallback;
	
		xDoc.Load ("http://api.openweathermap.org/data/2.5/weather?"+coordinate+"&mode=xml&APPID=0f90c1f61fda0ae7b576013098a7f91c");
		XmlNodeList xNodelst = xDoc.GetElementsByTagName ("current");
		XmlNode xNode = xNodelst.Item (0);
		xDoc.Save ("out.xml");
		if (xNode != null) {
			if (xNode.SelectSingleNode ("city") != null) {
				result.City = xNode.SelectSingleNode ("city").Attributes.GetNamedItem("name").InnerText;
			}
			if (xNode.SelectSingleNode ("weather") != null) {
				result.Weather = xNode.SelectSingleNode ("weather").Attributes.GetNamedItem("value").InnerText;
			}
		}
		return result;
	
	}


	/// <summary>
	/// My the remote certificate validation callback.
	/// Allow google api request from Unity
	/// </summary>
	/// <returns><c>true</c>, if remote certificate validation callback was myed, <c>false</c> otherwise.</returns>
	/// <param name="sender">Sender.</param>
	/// <param name="certificate">Certificate.</param>
	/// <param name="chain">Chain.</param>
	/// <param name="sslPolicyErrors">Ssl policy errors.</param>
	private static bool MyRemoteCertificateValidationCallback (System.Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
	{
		bool isOk = true;
		// If there are errors in the certificate chain, look at each error to determine the cause.
		if (sslPolicyErrors != SslPolicyErrors.None) {
			for (int i = 0; i < chain.ChainStatus.Length; i++) {
				if (chain.ChainStatus [i].Status != X509ChainStatusFlags.RevocationStatusUnknown) {
					chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
					chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
					chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan (0, 1, 0);
					chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllFlags;
					bool chainIsValid = chain.Build ((X509Certificate2)certificate);
					if (!chainIsValid) {
						isOk = false;
					}
				}
			}
		}
		return isOk;
	}
}
