using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject canvasPause;
    
    public bool activeCanvas;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !activeCanvas)
        {
            activeCanvas = true;
            Time.timeScale = 0f;
            canvasPause.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && activeCanvas)
        {
            activeCanvas = false;
            Time.timeScale = 1f;
            canvasPause.SetActive(false);
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        canvasPause.SetActive(false);
    }
}
