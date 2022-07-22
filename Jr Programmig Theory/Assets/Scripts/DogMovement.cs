using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMovement : MonoBehaviour
{
    private Rigidbody dogRb;
    [SerializeField] float dogSpeed = 15.0f;
    [SerializeField] float rotateSpeed = 60.0f;
    private GameObject catPlayer;
    [SerializeField] private ParticleSystem dirtSplatter;
    // Start is called before the first frame update
    void Start()
    {
        dogRb = GetComponent<Rigidbody>();
        catPlayer = GameObject.Find("Cat");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dirtSplatter.Play();
        dogRb.AddForce(CalculateDirection() * dogSpeed ,ForceMode.Force);
        if (transform.position.y <= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        RotateToCat();
    }

    Vector3 CalculateDirection()
    {
        Vector3 dir = (catPlayer.transform.position - transform.position).normalized;
        return dir;
    }
    void RotateToCat()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(catPlayer.transform.position - transform.position), rotateSpeed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            gameObject.SetActive(false);
            Destroy(collision.gameObject);
            SoundManager.soundManager.RatTrapSound();
            dirtSplatter.Stop();
        }
    }
}
