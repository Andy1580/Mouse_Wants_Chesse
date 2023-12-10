using UnityEngine;

public class RumbaFunctions : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject gameOverCanvas;

    public bool IsDead;

    RumbaPatrol rumbaPatrolScript;
    Collider col;

    private void Start()
    {
        rumbaPatrolScript = GetComponent<RumbaPatrol>();
        col = GetComponent<Collider>();
        IsDead = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
                //Destroy(playerPrefab);
                playerPrefab.SetActive(false);
                Time.timeScale = 0f;
                gameOverCanvas.SetActive(true);
                IsDead = true;
        }

        //if (collision.gameObject.tag == "Tramp")
        //{
        //    rumbaPatrolScript.enabled = false;
        //    isAlive = false;

        //}
    }


}
