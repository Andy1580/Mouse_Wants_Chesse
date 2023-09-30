using UnityEngine;

public class RatShoot : MonoBehaviour
{
    public Transform ballTramp;
    public Transform ponitSpawn;

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
            Instantiate(ballTramp, ponitSpawn.position, ponitSpawn.rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "BallTramp")
        {
            Debug.Log("Entro");
            if (Input.GetKeyDown(KeyCode.Space) && ballIsAdded == false)
            {
                Debug.Log("Se destruyo");
                ballIsAdded = true;
                Destroy(this.gameObject);
            }
        }
    }

}
