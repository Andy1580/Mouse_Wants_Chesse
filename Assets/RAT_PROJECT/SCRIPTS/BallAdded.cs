using UnityEngine;

public class BallAdded : MonoBehaviour
{
    RatShoot ball;

    private void Start()
    {
        ball = FindObjectOfType<RatShoot>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ball.ballIsAdded == false)
        {
            if (other.gameObject.tag == "Player")
            {
                {
                    Debug.Log("Se destruyo");
                    ball.ballIsAdded = true;
                    Destroy(gameObject);
                }
            }
        }
    }
}
