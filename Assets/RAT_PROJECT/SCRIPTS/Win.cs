using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    public ControlRaton Quesoswin;
    public GameObject pausa;
    public GameObject PanelWin;
    public GameObject PanelEstamina;
    public GameObject PanelCorrer;
    public GameObject Panelsigilo;
    public GameObject PanelItem;
    public GameObject PanelObjetivos;
    public bool win;
        

    private void Start()
    {
        win = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Quesoswin._quesos == 3)
            {

                Time.timeScale = 0f;
                PanelEstamina.SetActive(false);
                PanelCorrer.SetActive(false);
                Panelsigilo.SetActive(false);
                PanelItem.SetActive(false);
                PanelObjetivos.SetActive(false);
                pausa.SetActive(false);
                PanelWin.SetActive(true) ;
                win = true;
            }
        }
    }
}
