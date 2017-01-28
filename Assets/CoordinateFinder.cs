using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class CoordinateFinder : MonoBehaviour {

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private Transform earth;

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Ray r = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(r, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Earth")))
            {
                Vector3 cartesianCoord = earth.InverseTransformPoint(hitInfo.point).normalized;
                Vector2 sphericalCoord = CartesianToSphericalCoordinates(cartesianCoord);
                ProcessCoord(sphericalCoord);
            }
        }
    }

    private void ProcessCoord(Vector2 sphericalCoord)
    {
        Debug.Log("Process coord : " + sphericalCoord.ToString());
        XmlDocument xDoc = new XmlDocument();
        String coordinate = sphericalCoord.x + "," + sphericalCoord.y;
        //xDoc.Load("https://maps.googleapis.com/maps/api/geocode/xml?latlng=" + coordinate + "&location_type=ROOFTOP&result_type=street_address&key=YOURAPIKEY");

        //XmlNodeList xNodelst = xDoc.GetElementsByTagName("result");
        //XmlNode xNode = xNodelst.Item(0);
        //string adress = xNode.SelectSingleNode("formatted_address").InnerText;
        //string mahalle = xNode.SelectSingleNode("address_component[3]/long_name").InnerText;
        //string ilce = xNode.SelectSingleNode("address_component[4]/long_name").InnerText;
        //string il = xNode.SelectSingleNode("address_component[5]/long_name").InnerText;

        //Debug.Log(adress + "  " + mahalle + "  " + ilce + "  " + il);
    }

    private Vector2 CartesianToSphericalCoordinates(Vector3 cartesianCoord)
    {
        Debug.Log("Transform coord : " + cartesianCoord.ToString());
        Vector2 result = new Vector2();
        
        result.x = Mathf.Acos(cartesianCoord.y) * Mathf.Rad2Deg;
        result.y = Mathf.Atan(-cartesianCoord.x / cartesianCoord.z)  * Mathf.Rad2Deg;
        return result;
    }
}
