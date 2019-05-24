using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour {

    //win conditions and UI state

    public GameObject cannonObject;
    public GameObject explosionPrefab;
    public Transform explosionSpawn;
    public Transform explosionSpawn2;
    public Transform explosionSpawn3;
    public Transform explosionSpawn4;
    public Transform explosionSpawn5;
    public GameObject menu;
    public GameObject victoryText;
    public GameObject loseText;
    public GameObject ghostShipObject;
    public GameObject ghostObject;
    public AudioClip woodExplosionSound;
    public AudioClip woodExplosion2Sound;
    public AudioClip woodExplosion3Sound;
    public AudioClip shipExplosion;
    public AudioClip ghostSound;
    public AudioClip evilLaugh;
    public Material legendaryGhost;

    public GameObject pirateHP;
    public GameObject pirateHP100;
    public GameObject pirateHP90;
    public GameObject pirateHP80;
    public GameObject pirateHP70;
    public GameObject pirateHP60;
    public GameObject pirateHP50;
    public GameObject pirateHP40;
    public GameObject pirateHP30;
    public GameObject pirateHP20;
    public GameObject pirateHP10;
    public GameObject pirateHP00;

    private DifficultyScript difficultyScript;
    private AudioSource source;
    private CannonScript cannonScript;
    private GhostShipScript ghostShip;
    private GhostScript ghost;
    private int health = 110;
    private int randomNum;
    private float nextActionTime = 0.0f;
    private float period = 4f;
    private bool cannonFire = false;
    private bool isCoroutineStarted = false;
    private bool legendaryCannonFire = false;
    private float legendaryCannonFireRate = 3f;


    // Use this for initialization
    void Start () {
        cannonScript = cannonObject.GetComponent<CannonScript>();
        source = this.GetComponent<AudioSource>();
        ghostShip = ghostShipObject.GetComponent<GhostShipScript>();
        ghost = ghostObject.GetComponent<GhostScript>();
        difficultyScript = GameObject.FindGameObjectWithTag("Difficulty").GetComponent<DifficultyScript>();

        if (difficultyScript.isLegendaryMode == true)
        {
            ghost.GetComponentInChildren<SkinnedMeshRenderer>().material = legendaryGhost;
            ghost.LegendaryGhost();
        }
    }

    public void EnemyCannonAttack(int cannonNumber)
    {
        if (cannonNumber == 1)
        {
            cannonScript.MakeExplosion(explosionSpawn);
            cannonScript.MakeCannonBall(explosionSpawn);
        }
        else if (cannonNumber == 2)
        {
            cannonScript.MakeExplosion(explosionSpawn2);
            cannonScript.MakeCannonBall(explosionSpawn2);
        }
        else if (cannonNumber == 3)
        {
            cannonScript.MakeExplosion(explosionSpawn3);
            cannonScript.MakeCannonBall(explosionSpawn3);
        }

        cannonScript.CannonSounds(source);
    }

    public void EnemyCannonSideAttack(int cannonNumber)
    {
        if (cannonNumber == 4)
        {
            cannonScript.MakeExplosion(explosionSpawn4);
            cannonScript.MakeCannonBall(explosionSpawn4);
        }
        else if (cannonNumber == 5)
        {
            cannonScript.MakeExplosion(explosionSpawn5);
            cannonScript.MakeCannonBall(explosionSpawn5);
        }

        cannonScript.CannonSounds(source);
    }

    private IEnumerator LegendaryCannonFire(float fireRate)
    {
        isCoroutineStarted = true;
        yield return new WaitForSeconds(fireRate);
        isCoroutineStarted = false;
        if (cannonFire == true)
        {
            EnemyCannonAttack(2);
            EnemyCannonSideAttack(4);
            EnemyCannonSideAttack(5);
        }
    }

    public void DeductShipHealth()
    {

        health = health - 10;

        if (health <= 0) //game is won
        {
            print("You defeated the ghost ship!");
            source.PlayOneShot(shipExplosion);
            source.PlayOneShot(ghostSound);
            cannonFire = false;
            ghostShip.GhostShipFalls();
            ghost.isAggressive = false;
            pirateHP10.SetActive(false);
            MenuPopUp(true);
        }
        else if (health <= 10)
        {
            pirateHP20.SetActive(false);
        }
        else if (health <= 20)
        {
            pirateHP30.SetActive(false);
            if (difficultyScript.isLegendaryMode == true)
            {
                legendaryCannonFire = true;
            }
            else
                period = 2f;
        }
        else if (health <= 30)
        {
            pirateHP40.SetActive(false);
        }
        else if (health <= 40)
        {
            pirateHP50.SetActive(false);
        }
        else if (health <= 50)
        {
            pirateHP60.SetActive(false);
            if (difficultyScript.isLegendaryMode == true)
            {
                period = 2f;
            }
            else
            {
                ghost.isAggressive = true;
                source.PlayOneShot(evilLaugh);
            }
        }
        else if (health <= 60)
        {
            pirateHP70.SetActive(false);
        }
        else if (health <= 70)
        {
            pirateHP80.SetActive(false);
        }
        else if (health <= 80)
        {
            pirateHP90.SetActive(false);
            if (difficultyScript.isLegendaryMode == true)
            {
                ghost.isAggressive = true;
                source.PlayOneShot(evilLaugh);
            }
            else
                period = 3f;
        }
        else if (health <= 90)
        {
            pirateHP100.SetActive(false);
        }
        else if (health <= 100) //start firing
        {
            cannonFire = true;
            pirateHP.SetActive(true);
            if (difficultyScript.isLegendaryMode == true)
                period = 2.5f;
            else
                period = 4f;
        }

    }

    private IEnumerator WaitForMenu(bool isVictory)
    {
        if (isVictory == true)
        {
            yield return new WaitForSeconds(5f);
            victoryText.SetActive(true);
        }
        else {
            yield return new WaitForSeconds(2f);
            loseText.SetActive(true);
            victoryText.GetComponent<RawImage>().enabled = false;
        }
        menu.SetActive(true);
    }

    public void MenuPopUp(bool wonGame)
    {
        cannonFire = false;
        foreach (GameObject cannonBall in GameObject.FindGameObjectsWithTag("CannonBall"))
            cannonBall.SetActive(false);
        StartCoroutine(WaitForMenu(wonGame));
    }

    public void CollisionExplosion(Transform collPref)
    {
        var explosion = (GameObject)Instantiate(explosionPrefab, collPref.position, collPref.rotation);
        explosion.transform.Rotate(0, 0, 90);
        explosion.transform.localScale += new Vector3(4, 4, 4);
        randomNum = Random.Range(1, 4); //1-3
        source.PlayOneShot(WoodSoundExplosion(randomNum));
        Destroy(explosion, 0.5f);
    }

    private AudioClip WoodSoundExplosion(int randomNum)
    {
        if (randomNum == 1)
            return woodExplosionSound;
        else if (randomNum == 2)
            return woodExplosion2Sound;
        else
            return woodExplosion3Sound;
    }

    private void Update()
    {
        nextActionTime += Time.deltaTime;
        if (nextActionTime > period)
        {
            nextActionTime = 0.0f;

            if (cannonFire == true)
            {
                randomNum = Random.Range(1, 4); //1-3
                EnemyCannonAttack(randomNum);
                randomNum = Random.Range(4, 6); //4-5
                EnemyCannonSideAttack(randomNum);
                if (difficultyScript.isLegendaryMode == true && isCoroutineStarted == false && legendaryCannonFire == true)
                    StartCoroutine(LegendaryCannonFire(legendaryCannonFireRate));
            }

        }
    }
}
