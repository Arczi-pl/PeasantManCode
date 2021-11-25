using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject Cage, Particles;
    private Animator _cageAnimator;

    void OnCollisionEnter(Collision collision)
    {
        // if key collider with player
        if (collision.gameObject.tag == "Player")
        {
            // show lighting effects
            Particles.SetActive(true);
            _cageAnimator = Cage.GetComponent<Animator>();
            // run sound of a falling cage
            GameObject.Find("/Audio/cageDown").GetComponent<AudioSource>().Play();
            // run animation of a falling cage
            _cageAnimator.SetBool("goDown", true);
            // destroy key
            Destroy(gameObject);
        }
    }
}
