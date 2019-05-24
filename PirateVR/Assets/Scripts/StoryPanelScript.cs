using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryPanelScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if (Input.anyKey)
        {
            this.gameObject.SetActive(false);
        }


	}
}
