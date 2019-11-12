using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playAnimation : MonoBehaviour {
    public Animator animator;

	// Use this for initialization
	void Start () {
        animator.Play("loading");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
