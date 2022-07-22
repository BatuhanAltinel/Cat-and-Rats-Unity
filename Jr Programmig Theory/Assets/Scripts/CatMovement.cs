using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMovement : MonoBehaviour
{
    private Rigidbody catRb;
    [SerializeField] private float speed = 0.3f;
    private Animator catAnim;
    [SerializeField] float turnSpeed = 50.0f;
    [HideInInspector] public int catLives;
    [SerializeField] private ParticleSystem dirtSplatter;
    
    

    // Start is called before the first frame update
    void Start()
    {
        catRb = GetComponent<Rigidbody>();
        catAnim = this.GetComponent<Animator>();
        catLives = 3;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CatMove();
    }

    private void CatMove()
    {
        MovementBoundary();

        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        
        if (verticalInput != 0 || horizontalInput != 0)
        {
            catAnim.SetInteger("Walk", 1);
            dirtSplatter.Play();
        }
        else
        { 
            catAnim.SetInteger("Walk", 0);
            dirtSplatter.Stop();
            }
        transform.Translate(Vector3.forward * speed * verticalInput);
        transform.Translate(Vector3.right * speed * horizontalInput);

        transform.Rotate(0, turnSpeed * horizontalInput * Time.deltaTime,0);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Rat"))
        {
            SoundManager.soundManager.CatPainSound();
            catLives--;
            if (catLives <= 0)
            {
                catLives = 0;
                GameManager.instance.GameOver();
            }
        }
        GameManager.instance.livesText.text = "Lives: " + catLives;
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("LiveUp"))
        {
            SoundManager.soundManager.HeartUpSound();
            catLives++;
            other.gameObject.SetActive(false);
        }
        GameManager.instance.livesText.text = "Lives: " + catLives;
    }
    void MovementBoundary()
    {
        if (transform.position.y <= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        if (transform.position.x < -29)
        {
            transform.position = new Vector3(-29, transform.position.y, transform.position.z);
        }
        if (transform.position.x > 29)
        {
            transform.position = new Vector3(29, transform.position.y, transform.position.z);
        }
        if (transform.position.z < -16)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -16);
        }
        if (transform.position.z > 15)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 15);
        }
    }

}
