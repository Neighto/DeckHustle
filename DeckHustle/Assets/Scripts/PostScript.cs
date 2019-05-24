using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostScript : MonoBehaviour {

    private AudioSource source;
    private float lowPitchRange = 0.80F;
    private float highPitchRange = 1.20F;

    // Use this for initialization
    void Start () {
        source = this.GetComponent<AudioSource>();
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "CannonBall")
        {
            Destroy(collision.gameObject);
            source.pitch = Random.Range(lowPitchRange, highPitchRange);
            source.Play();
        }
    }
}
