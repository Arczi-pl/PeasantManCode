using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject cage;
    public GameObject particles;
    Animator cageAnimator;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            particles.SetActive(true);
            cageAnimator = cage.GetComponent<Animator>();
            cageAnimator.SetBool("goDown", true);
            Destroy(gameObject);
        }
    }
}
