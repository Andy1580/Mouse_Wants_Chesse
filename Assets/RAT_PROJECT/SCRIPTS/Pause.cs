using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject canvasPause;
    public GameObject canvasControles;
    public RumbaFunctions Ratondead;

    public GameObject PanelEstamina;
    public GameObject PanelCorrer;
    public GameObject Panelsigilo;
    public GameObject PanelItem;
    public GameObject PanelObjetivos;

    public bool pauseCanvas;

    private void Start()
    {
        pauseCanvas = false;
    }


    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && !pauseCanvas)
        {
            pauseCanvas = true;
            Time.timeScale = 0f;
            canvasPause.SetActive(true);

            //if (Ratondead.IsDead == false)
            //{
            //    canvasControles.SetActive(false);
            //    PanelEstamina.SetActive(false);
            //    PanelCorrer.SetActive(false);
            //    Panelsigilo.SetActive(false);
            //    PanelItem.SetActive(false);
            //    PanelObjetivos.SetActive(false);
            //}

        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pauseCanvas)
        {
            pauseCanvas = false;
            Time.timeScale = 1f;
            canvasPause.SetActive(false);
            canvasControles.SetActive(false);
            //PanelEstamina.SetActive(true);
            //PanelCorrer.SetActive(true);
            //Panelsigilo.SetActive(true);
            //PanelItem.SetActive(true);
            //PanelObjetivos.SetActive(true);
        }
    }

    public void Resume()
    {
        pauseCanvas = false;
        Time.timeScale = 1f;
        canvasPause.SetActive(false);
        //PanelEstamina.SetActive(true);
        //PanelCorrer.SetActive(true);
        //Panelsigilo.SetActive(true);
        //PanelItem.SetActive(true);
        //PanelObjetivos.SetActive(true);
    }
}
