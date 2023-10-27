using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour
{
    [SerializeField]
    float ascendUnit;
    [SerializeField]
    GameObject balloonPopParticles;
    GameObject mainCamera;
    Animator animator;
    ScoreboardController scoreboard;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        transform.LookAt(new Vector3(mainCamera.transform.position.x, transform.position.y, mainCamera.transform.position.z));
        animator = gameObject.GetComponent<Animator>();
        scoreboard = GameObject.Find("Scoreboard").GetComponent<ScoreboardController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + ascendUnit, transform.position.z);

        if(transform.position.y >= 15)
        {
            animator.SetTrigger("Hit");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Hedgehog")
        {
            scoreboard.UpdateScore((int)Vector3.Distance(mainCamera.transform.position, transform.position));
            Instantiate(balloonPopParticles, transform);
            animator.SetTrigger("Hit");
        }
    }
}
