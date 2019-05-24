using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public GameObject brock;
    private int DistanceAway = -18;
    private float specialY;

    private void Start()
    {
        specialY = brock.transform.transform.position.y - DistanceAway;
    }

    // Update is called once per frame
    void Update () {

        Vector3 PlayerPOS = brock.transform.transform.position;
        // transform.position = new Vector3(PlayerPOS.x, PlayerPOS.y - DistanceAway, PlayerPOS.z);
         transform.position = new Vector3(PlayerPOS.x, specialY, PlayerPOS.z);
    }
}
