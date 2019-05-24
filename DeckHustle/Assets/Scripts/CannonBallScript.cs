using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallScript : MonoBehaviour {


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "GhostShip")
        {
           // GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().CollisionExplosion(this.GetComponent<Transform>());
           // this.GetComponent<MeshRenderer>().enabled = false;
          //  Destroy(this.gameObject);

        }

    }

}
