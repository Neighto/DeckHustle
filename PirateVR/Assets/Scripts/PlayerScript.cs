using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public GameObject carryBall;
    public GameObject gameControllerObject;
    public AudioClip hitByCannonBall;
    public AudioClip hitByGhost;

    public GameObject leftShoulderObject;
    public GameObject rightShoulderObject;
    public GameObject spineObject;
    private CapsuleCollider leftShoulder;
    private CapsuleCollider rightShoulder;
    private BoxCollider spine;
    private Rigidbody rbSpine;
    private Transform brockTransform;

    private AudioSource source;
    private GameController gameController;
    private CapsuleCollider brockBody;
    private Animator brockAnimator;
    private Vector3 emptyVector = new Vector3(0, 0, 0);
    private float speed = 8.0f;


    // Use this for initialization
    void Start () {
        brockAnimator = GetComponent<Animator>();
        brockBody = this.GetComponent<CapsuleCollider>();
        gameController = gameControllerObject.GetComponent<GameController>();
        source = GetComponent<AudioSource>();
        leftShoulder = leftShoulderObject.GetComponent<CapsuleCollider>();
        rightShoulder = rightShoulderObject.GetComponent<CapsuleCollider>();
        spine = spineObject.GetComponent<BoxCollider>();
        rbSpine = spineObject.GetComponent<Rigidbody>();
        brockTransform = GetComponent<Transform>();
	}

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "CannonBallBox")
        {
            brockAnimator.SetBool("HasBall", true);
            carryBall.SetActive(true);
            speed = 5.5f;
        }
        else if (collision.gameObject.tag == "Cannon")
        {
            brockAnimator.SetBool("HasBall", false);
            carryBall.SetActive(false);
            speed = 8.0f;
        }
        else if (collision.gameObject.tag == "CannonBall" || collision.gameObject.tag == "Ghost")
        {
            if (collision.gameObject.tag == "Ghost")
            {
                collision.gameObject.GetComponent<GhostScript>().KilledPlayer();
                source.PlayOneShot(hitByGhost);
            }
            else
            {
                source.PlayOneShot(hitByCannonBall);
            }

            brockTransform.position = new Vector3(brockTransform.position.x, brockTransform.position.y + 2, brockTransform.position.z);

            brockAnimator.enabled = false;
            leftShoulder.enabled = true;
            rightShoulder.enabled = true;
            spine.enabled = true;
            gameController.MenuPopUp(false);
            brockBody.radius = 0;
            brockBody.height = 0;
            this.enabled = false;
        }

    }

    // Update is called once per frame
    void FixedUpdate () {

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        Vector3 movement = new Vector3(x, 0.0f, z);
        if (movement != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F);

        transform.Translate(movement, Space.World);

        if (movement != emptyVector)
            brockAnimator.SetBool("IsMoving", true);
        else
            brockAnimator.SetBool("IsMoving", false);
        
    }
}
