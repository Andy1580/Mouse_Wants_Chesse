using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject canvasPause;
    public GameObject canvasResume;
    
    public bool pauseCanvas;
    public bool resumeCanvas;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && !pauseCanvas)
        {
            pauseCanvas = true;
            resumeCanvas = false;
            Time.timeScale = 0f;
            canvasPause.SetActive(true);
            canvasResume.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pauseCanvas)
        {
            pauseCanvas = false;
            resumeCanvas = true;
            Time.timeScale = 1f;
            canvasPause.SetActive(false);
            canvasResume.SetActive(true);
        }
    }

    public void Resume()
    {
        pauseCanvas = false;
        resumeCanvas = true;
        Time.timeScale = 1f;
        canvasPause.SetActive(false);
        canvasResume.SetActive(true);
    }
}
