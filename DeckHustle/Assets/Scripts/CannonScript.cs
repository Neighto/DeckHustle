using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CannonScript : MonoBehaviour
{

    public GameObject cannonBallBoxObject;
    public GameObject explosionPrefab;
    public Transform explosionSpawn;
    public AudioClip cannonSound1;
    public AudioClip cannonSound2;
    public AudioClip cannonSound3;
    public GameObject gameControllerObject;
    public GameObject cannonPrefab;

    private AudioSource source;
    private float lowPitchRange = 0.80F;
    private float highPitchRange = 1.20F;
    private int cannonSoundNumber;
    private GameController gameController;

    CannonBallBoxScript cannonBallBox;

    // Use this for initialization
    void Start()
    {
        gameController = gameControllerObject.GetComponent<GameController>();
        cannonBallBox = cannonBallBoxObject.GetComponent<CannonBallBoxScript>();
        source = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (cannonBallBox.gotCannon == true)
        {
            cannonBallBox.gotCannon = false;

            MakeExplosion(explosionSpawn);
            CannonSounds(source);
            MakeCannonBall(explosionSpawn);
            print("Gave the cannonball");

            StartCoroutine(WaitToDeduct());
        }
    }

    IEnumerator WaitToDeduct()
    {
        yield return new WaitForSeconds(0.5f);
        gameController.DeductShipHealth();
    }

    public void CannonSounds(AudioSource src)
    {
        src.pitch = Random.Range(lowPitchRange, highPitchRange);
        cannonSoundNumber = Random.Range(1, 4);
        if (cannonSoundNumber == 1)
            src.PlayOneShot(cannonSound1);
        else if (cannonSoundNumber == 2)
            src.PlayOneShot(cannonSound2);
        else if (cannonSoundNumber == 3)
            src.PlayOneShot(cannonSound3);
    }

    public void MakeExplosion(Transform expSpawn)
    {
        var explosion = (GameObject)Instantiate(explosionPrefab, expSpawn.position, expSpawn.rotation);
        explosion.transform.localScale = new Vector3(5, 5, 5);
        Destroy(explosion, 0.5f);
    }

    public void MakeCannonBall(Transform expSpawn)
    {
        var cannonBall = (GameObject)Instantiate(cannonPrefab, expSpawn.position, expSpawn.rotation);
        cannonBall.GetComponent<Rigidbody>().velocity = cannonBall.transform.up * 8;
        Destroy(cannonBall, 3.0f);
    }

}
