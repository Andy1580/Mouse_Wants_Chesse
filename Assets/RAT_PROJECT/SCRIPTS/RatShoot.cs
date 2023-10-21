using UnityEngine;

public class RatShoot : MonoBehaviour
{
    public GameObject ballTramp;
    public Transform pointSpawn;
    public float ballSpeed;
    public bool ballIsAdded;

    private void Start()
    {
        
    }

    private void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        if (ballIsAdded == true && Input.GetKeyDown(KeyCode.Space))
        {
            var ballToInstantiate = Instantiate(ballTramp, pointSpawn.position, pointSpawn.rotation);
            ballToInstantiate.GetComponent<Rigidbody>().velocity = pointSpawn.forward * ballSpeed;
            ballIsAdded = false;
            
        }
    }

}
