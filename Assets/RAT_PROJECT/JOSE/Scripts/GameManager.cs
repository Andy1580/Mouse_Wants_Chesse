using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor.PackageManager.UI;

public class GameManager : MonoBehaviour
{
    public Jugador jugador;
    public TMP_Text alertatx;
    public Animator popupAnimator;
    private Queue<string> popupQueue; //make it different type for more detailed popups, you can add different types, titles, descriptions etc
    private Coroutine queueChecker;


    [SerializeField] public GameObject queso0;
    [SerializeField] public GameObject queso1;
    [SerializeField] public GameObject queso2;
    [SerializeField] public GameObject alertaObj;





    private void Start()
    {
        
        alertaObj.gameObject.SetActive(false);

        alertaObj = transform.GetChild(0).gameObject;
        popupAnimator = alertaObj.GetComponent<Animator>();
        alertaObj.SetActive(false);
        popupQueue = new Queue<string>();
    }
    private void Update()
    {

    
    }

    //public IEnumerator Alerta(string message)
    //{
    //    if (jugador._quesos == 3)
    //    {
    //        message = alertatx.text;
    //        alertatx.text = "Rapido vuelve a la guarida!!!";
    //        AddToQueue(alertatx.text);
    //        //ShowPopup(alertatx.text);
    //    }

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

}
