using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostShipScript : MonoBehaviour {

    public Transform explosionZone1;
    public Transform explosionZone2;
    public Transform explosionZone3;
    public GameObject greenExplosionPrefab;
    public GameObject gameControllerObject;

    private AudioSource source;
    private GameController gameController;

    private float ghostShipSpeed = 0.005f;
    private bool coroutineGoes = false;
    private bool goingUp = true;
    private bool turningRight = true;
    private bool ghostShipFalls = false;

    private float maxHeight;
    private float minHeight;
    private float maxRotation;
    private float minRotation;
    private float nextActionTime;
    private float period = 3f;
    private float sinkHeight;
    private float backDistance;

    private void Start()
    {
        source = this.GetComponent<AudioSource>();
        gameController = gameControllerObject.GetComponent<GameController>();

        maxHeight = transform.position.z + 1f;
        minHeight = transform.position.z - 2f;

        maxRotation = transform.rotation.y + 1f;
        minRotation = transform.rotation.y - 1f;

        sinkHeight = transform.position.y - 30;
        backDistance = transform.position.z - 60;
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameController.CollisionExplosion(collision.transform);
        Destroy(collision.gameObject);
    }

    private IEnumerator WaitToTranslate()
    {
        coroutineGoes = true;
        yield return new WaitForSeconds(1f);
        coroutineGoes = false;
        goingUp = !goingUp;
    }

    private IEnumerator WaitToFall()
    {
        yield return new WaitForSeconds(0.5f);
        GreenExplosion(explosionZone1);
        yield return new WaitForSeconds(1f);
        GreenExplosion(explosionZone3);
        yield return new WaitForSeconds(0.5f);
        GreenExplosion(explosionZone2);
        ghostShipFalls = true;
    }

    public void GhostShipFalls()
    {
        StartCoroutine(WaitToFall());
    }

    private void GreenExplosion(Transform zone)
    {
        source.Play();
        var greenExplosion = (GameObject)Instantiate(greenExplosionPrefab, zone.position, zone.rotation);
        greenExplosion.transform.localScale = new Vector3(8, 8, 8);
        Destroy(greenExplosion, 0.7f);
    }

    // Update is called once per frame
    void Update () {

        //translation
        if (transform.position.z <= maxHeight && goingUp == true)
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + ghostShipSpeed);
        else if (goingUp == true && coroutineGoes == false)
            StartCoroutine(WaitToTranslate());
        else if (transform.position.z >= minHeight && goingUp == false)
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - ghostShipSpeed);
        else if (goingUp == false && coroutineGoes == false)
            StartCoroutine(WaitToTranslate());

        //timer
        nextActionTime += Time.deltaTime;
        if (nextActionTime > period)
        {
            nextActionTime = 0.0f;
            turningRight = !turningRight;
        }

        //rotation
        if (turningRight == true)
            transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.Euler(transform.rotation.x, maxRotation, transform.rotation.z), 0.008f);
        else if (turningRight == false)
            transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.Euler(transform.rotation.x, minRotation, transform.rotation.z), 0.008f);

        if (ghostShipFalls == true)
        {
            
            if (transform.position.y <= sinkHeight)
                ghostShipFalls = false;

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y - 2, transform.position.z), 0.03f);
            
            if (transform.position.z <= backDistance)
                ghostShipFalls = false;

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z - 2), 0.08f);

        }

    }
}
