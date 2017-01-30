using UnityEngine;

// Maths utils to handle cartesian and spherical coordiantes frame
public class CoordinateFinder : MonoBehaviour {

	/// <summary>
	/// Cartesians to spherical coordinates.
	/// </summary>
	/// <returns>The to spherical coordinates.</returns>
	/// <param name="cartesianCoord">Cartesian coordinate.</param>
	public static Vector2 CartesianToSphericalCoordinates(Vector3 cartesianCoord)
    {
        Vector2 result = new Vector2();
		result.x = Mathf.Asin(cartesianCoord.y) * Mathf.Rad2Deg;
		result.y = Mathf.Atan2(-cartesianCoord.x, cartesianCoord.z) * Mathf.Rad2Deg;
        return result;
    }
}