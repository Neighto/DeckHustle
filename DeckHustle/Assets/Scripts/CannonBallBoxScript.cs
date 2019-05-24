using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallBoxScript : MonoBehaviour {

    public bool gotCannon = false;
    public AudioClip warDrumSound;

    private AudioSource source;

    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gotCannon == false)
        {
            gotCannon = true;
            source.PlayOneShot(warDrumSound);
            print("Got the cannonball");
        }
    }


}
