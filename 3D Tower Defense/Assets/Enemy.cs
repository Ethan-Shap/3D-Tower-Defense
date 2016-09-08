using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public enum Type
    {

    }

    [SerializeField]
    private float speed;

    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
