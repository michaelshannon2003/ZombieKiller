using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public Transform ammunition;
    public float minimumDistanceFromPlayer = 15;
    public float shotInterval = 0.5f;
    private float shotTime = 0;
    GameObject player;
    Animator animator;

    private void Awake()
    { 
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    public void Update()
    {

        if (player != null)
        {
            var distance = Vector3.Distance(player.transform.position, transform.position);

            if (distance >= minimumDistanceFromPlayer && (Time.time - shotTime) > shotInterval)
            {
                Debug.Log("Checking for animation for " + gameObject.name);
                if (animator != null)
                {
                    Debug.Log("Setting animation for " + gameObject.name);
                    animator.SetBool("IsWithinDistance", false);
                }
                Shoot();
            }
            else
            {
                if (animator != null)
                {
                    animator.SetBool("IsWithinDistance", true);
                }
            }
        }
    }

    public void LookAtTarget()
    {
        transform.LookAt(player.transform);       
    }


    public void Shoot()
    {
        shotTime = Time.time;
        var fireform = transform.Find("FireLocation").transform;
        Rigidbody shellInstance =
            Instantiate(ammunition.GetComponent<Rigidbody>(), fireform.position, fireform.rotation);
        shellInstance.velocity = 10f * fireform.forward;
    }
}
