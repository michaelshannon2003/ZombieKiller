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

    private void Awake()
    { 
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Update()
    {

        if (player != null)
        {
            var distance = Vector3.Distance(player.transform.position, transform.position);

            if (distance >= minimumDistanceFromPlayer && (Time.time - shotTime) > shotInterval)
            {
                LookAtTarget();
                Shoot();
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
