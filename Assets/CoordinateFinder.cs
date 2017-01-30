
using UnityEngine;

public class CoordinateFinder : MonoBehaviour {

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private Transform earth;

    private RequestManager requestManager = new RequestManager();

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
                requestManager.RequestCountry(sphericalCoord);
            }
        }
    }

    

    private Vector2 CartesianToSphericalCoordinates(Vector3 cartesianCoord)
    {
        Vector2 result = new Vector2();

        result.x = Mathf.Asin(cartesianCoord.y) * Mathf.Rad2Deg;
        result.y = Mathf.Atan2(-cartesianCoord.x, cartesianCoord.z)  * Mathf.Rad2Deg;

        return result;
    }

}
