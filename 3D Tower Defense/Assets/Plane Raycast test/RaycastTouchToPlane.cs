using UnityEngine;
using System.Collections;

public class RaycastTouchToPlane : MonoBehaviour {

    private Plane myPlane;

	// Use this for initialization
	void Start ()
    {
        myPlane = new Plane(Vector3.up, 10f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float ent = 10f;
            if (myPlane.Raycast(ray, out ent))
            {
                Debug.Log("Plane Raycast hit at distance: " + ent);
                Vector3 hitPoint = ray.GetPoint(ent);

                GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.transform.position = hitPoint;
                Debug.DrawRay(ray.origin, ray.direction * ent, Color.green);
            }
            else
                Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);

        }
    }
}
