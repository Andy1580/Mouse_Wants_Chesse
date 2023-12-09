using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject canvasPause;
    
    
    public bool pauseCanvas;
    

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && !pauseCanvas)
        {
            pauseCanvas = true;
            Time.timeScale = 0f;
            canvasPause.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pauseCanvas)
        {
            pauseCanvas = false;
            Time.timeScale = 1f;
            canvasPause.SetActive(false);
        }
    }

    public void Resume()
    {
        pauseCanvas = false;
        Time.timeScale = 1f;
        canvasPause.SetActive(false);
    }
}
