using UnityEngine;

public class RumbaFunctions : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private bool isAlive;

    RumbaPatrol rumbaPatrolScript;
    Collider col;

    private void Start()
    {
        rumbaPatrolScript = GetComponent<RumbaPatrol>();
        col = GetComponent<Collider>();

        isAlive = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (isAlive == true)
            {
                //Destroy(playerPrefab);
                playerPrefab.SetActive(false);
                Time.timeScale = 0f;
                gameOverCanvas.SetActive(true);
            }
        }

        if (collision.gameObject.tag == "Tramp")
        {
            rumbaPatrolScript.enabled = false;
            isAlive = false;

        }
    }


}
