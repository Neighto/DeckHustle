using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour {

    public Transform brock;
    public Transform ghostWatchPlace;
    private Transform ghostTransform;
    private Vector3 lazyDirection;
    public bool isAggressive = false;
    private bool isCoroutineStarted = false;
    private bool killedPlayer = false;
    private bool legendaryGhost = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "CannonBall")
        {
            Destroy(collision.gameObject);
        }
    }

    public void Dies()
    {
        isAggressive = false;
    }

    public void KilledPlayer()
    {
        killedPlayer = true;
    }

    public void LegendaryGhost()
    {
        legendaryGhost = true;
        transform.localScale += new Vector3(1, 1, 1);
    }

    IEnumerator LazyAttack()
    {
        isCoroutineStarted = true;
        lazyDirection = brock.position;
        yield return new WaitForSeconds(2f);
        isCoroutineStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (killedPlayer == false)
            transform.LookAt(brock);

        if (isAggressive == false)
        {
            transform.position = ghostWatchPlace.position;
        }
        else //aggressive
        {
            if (!isCoroutineStarted)
                StartCoroutine(LazyAttack());
            if (Vector3.Distance(transform.position, lazyDirection) >= 1) //graphical issue prevention
            { 
                if (legendaryGhost == true)
                    transform.position = Vector3.MoveTowards(transform.position, lazyDirection, 7f * Time.deltaTime);
                else
                    transform.position = Vector3.MoveTowards(transform.position, lazyDirection, 4f * Time.deltaTime);
            }
        }

    }
}
