using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public ControlRaton jugador;
    public Gato muerto;
    public RumbaFunctions acabado;
    public RumbaFunctions acabado1;
    public RumbaFunctions acabado2;
    public RumbaFunctions acabado3;
    public TMP_Text alertatx;
    public Animator popupAnimator;
    private Queue<string> popupQueue; //make it different type for more detailed popups, you can add different types, titles, descriptions etc
    private Coroutine queueChecker;
    public GameObject audioSource;


    [SerializeField] public GameObject queso0;
    [SerializeField] public GameObject queso1;
    [SerializeField] public GameObject queso2;
    [SerializeField] public GameObject alertaObj;

    public GameObject PanelEstamina;
    public GameObject PanelCorrer;
    public GameObject Panelsigilo;
    public GameObject PanelItem;
    public GameObject PanelObjetivos;
    public GameObject core;
    public GameObject instrucciones;
    public GameObject lose;
    public GameObject pause;
    public bool manager;


    private void Start()
    {
        manager = false;
        core.SetActive(true);
        instrucciones.SetActive(false);
        Time.timeScale = 0f;
        alertaObj.gameObject.SetActive(false);
        popupAnimator = alertaObj.GetComponent<Animator>();
        alertaObj.SetActive(false);
        popupQueue = new Queue<string>();

        PanelEstamina.SetActive(false);
        PanelCorrer.SetActive(false);
        Panelsigilo.SetActive(false);
        PanelItem.SetActive(false);
        PanelObjetivos.SetActive(false);
        lose.SetActive(false);
        pause.SetActive(false);
        //movcam.GetComponent<MoveCamera>().enabled = false;
        audioSource.SetActive(false);
    }
    private void Update()
    {
        if(jugador.item == enabled)
        {
            Item();
        }
        else
        {
            PanelItem.SetActive(false);
        }
        

        if(muerto.RatDead == true)
        {
            PanelEstamina.SetActive(false);
            PanelCorrer.SetActive(false);
            Panelsigilo.SetActive(false);
            PanelItem.SetActive(false);
            PanelObjetivos.SetActive(false);
            lose.SetActive(true);
            manager = false;
            
            if (muerto.RatDead == true)
            {
                pause.SetActive(false);
                audioSource.SetActive(false);
            }
        }

        if(acabado.IsDead == true)
        {
            PanelEstamina.SetActive(false);
            PanelCorrer.SetActive(false);
            Panelsigilo.SetActive(false);
            PanelItem.SetActive(false);
            PanelObjetivos.SetActive(false);
            lose.SetActive(true);
            manager = false;
            pause.SetActive(false);
            audioSource.SetActive(false);
        }
        if (acabado1.IsDead == true)
        {
            PanelEstamina.SetActive(false);
            PanelCorrer.SetActive(false);
            Panelsigilo.SetActive(false);
            PanelItem.SetActive(false);
            PanelObjetivos.SetActive(false);
            lose.SetActive(true);
            manager = false;
            pause.SetActive(false);
            audioSource.SetActive(false);
        }
        if (acabado2.IsDead == true)
        {
            PanelEstamina.SetActive(false);
            PanelCorrer.SetActive(false);
            Panelsigilo.SetActive(false);
            PanelItem.SetActive(false);
            PanelObjetivos.SetActive(false);
            lose.SetActive(true);
            manager = false;
            pause.SetActive(false);
            audioSource.SetActive(false);
        }
        if (acabado3.IsDead == true)
        {
            PanelEstamina.SetActive(false);
            PanelCorrer.SetActive(false);
            Panelsigilo.SetActive(false);
            PanelItem.SetActive(false);
            PanelObjetivos.SetActive(false);
            lose.SetActive(true);
            manager = false;
            pause.SetActive(false);
            audioSource.SetActive(false);
        }
    }
    //private void Alertas ()
    //{
    //    if (jugador._quesos == 3)
    //    {
    //        Alerta(message);
    //    }
    //}
    //public IEnumerator Alerta(string message)
    //{

    //        message = alertatx.text;
    //        alertatx.text = "Rapido vuelve a la guarida!!!";
    //        AddToQueue(alertatx.text);
    //        //ShowPopup(alertatx.text);

    //}


    public void AddToQueue(string text)
    {//parameter the same type as queue

        popupQueue.Enqueue(text);
        if (queueChecker == null)
            queueChecker = StartCoroutine(CheckQueue());

    }

    private void ShowPopup(string text)
    { //parameter the same type as queue
        alertaObj.SetActive(true);
        alertatx.text = text;
        popupAnimator.Play("Alert1");
    }

    private IEnumerator CheckQueue()
    {
        do
        {
            ShowPopup(popupQueue.Dequeue());
            do
            {
                yield return null;
            } while (!popupAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Idle"));

        } while (popupQueue.Count > 0);
        alertaObj.SetActive(false);
        queueChecker = null;
    }
     
    public void Controles()
    {
        core.SetActive(false);
        instrucciones.SetActive(true);
    }

    public void Iniciar()
    {
        instrucciones.SetActive(false);
        Time.timeScale = 1f;
        PanelEstamina.SetActive(true);
        PanelCorrer.SetActive(false);
        Panelsigilo.SetActive(false);
        PanelItem.SetActive(true);
        PanelObjetivos.SetActive(true);
        pause.SetActive(true);
        manager = true;
        audioSource.SetActive(true);
    }

    public void Item()
    {
        PanelItem.SetActive(true);
    }
}
