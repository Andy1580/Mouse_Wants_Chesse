using UnityEngine;

public class RatShoot : MonoBehaviour
{
    public Transform ballTramp;
    public Transform ponitSpawn;

    public bool ballIsAdded;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {

        Transform ball = Instantiate(ballTramp, ponitSpawn.position, ponitSpawn.rotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!ballIsAdded && Input.GetKeyDown(KeyCode.Space))
        {
            ballIsAdded = true;
        }
    }

}
