using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviour {

    public float speed;

    public float yRot;
		
	private void Update()
    {
        transform.Rotate(0, yRot * Time.deltaTime, 0, Space.World);
	}
}