using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHeart : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        rotateSpeed = 95.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(GameManager.instance.fireWork, this.transform.position, Quaternion.identity);
            GameManager.instance.fireWork.Play();
            this.gameObject.SetActive(false);
        }
    }
    
}
