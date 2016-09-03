using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {

    // Tower Attributes
    public string title;
    public int damage;
    public float range;

    private Animator animator;

	// Use this for initialization
	void Start ()
    {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void TriggerBuildAnimation()
    {
        animator.SetTrigger("BuildTrigger");
    }

}
