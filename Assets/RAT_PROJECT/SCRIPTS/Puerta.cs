using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puerta : MonoBehaviour
{

    public float Puertavida;
    public float Puertacost;
    public float Puertavidamax;
    public Image PuertaBar;
    public bool PuertaX;


    // Start is called before the first frame update
    void Start()
    {
        Puertavida = Puertavidamax;
        PuertaBar.fillAmount = Puertavidamax;
        PuertaBar.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Adiospuerta();
    }

    public void Adiospuerta()
    {
        if (Input.GetKey(KeyCode.T))
        {
            PuertaX = true;
        }

        
           if (PuertaX && Puertavida > 0)
            {
                Puertavida -= Puertacost * Time.deltaTime;
                if (Puertavida < 0) Puertavida = 0;
                PuertaBar.fillAmount = Puertavida / Puertavidamax;
            }
            if (Puertavida <= 0)
                gameObject.SetActive(false);
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        PuertaBar.gameObject.SetActive(true);
        
    }
    public void OnCollisionExit(Collision collision)
    {
        PuertaBar.gameObject.SetActive(false);
        PuertaX = false;
    }

}
