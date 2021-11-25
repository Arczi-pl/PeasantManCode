using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject Cage, Particles;
    private Animator _cageAnimator;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Particles.SetActive(true);
            _cageAnimator = Cage.GetComponent<Animator>();
            GameObject.Find("/Audio/cageDown").GetComponent<AudioSource>().Play();
            _cageAnimator.SetBool("goDown", true);
            Destroy(gameObject);
        }
    }
}
