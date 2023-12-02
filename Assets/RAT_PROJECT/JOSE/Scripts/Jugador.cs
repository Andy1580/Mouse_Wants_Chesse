using Cinemachine;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class Jugador : Vida
{
    #region Core
    private static Jugador self;
    public static Jugador Self => self;

    private void Start()
    {
        
        estamina = MaxStamina;
        self = this;
    }
    private void Update()
    {
        
        U_Movimiento();
        //Ataque();
        if (estamina > 0 && !stealth) Correr();
        else
        {
            IndicadorRun.gameObject.SetActive(false);
            running = false;
        }
        if (estamina > 0 && !running) Sigilo();
        else
        {
            IndicadorSig.gameObject.SetActive(false);
            stealth = false;
        }
        

        //if ((Input.GetMouseButtonDown(1)))
        //{
        //    objectToActivate.SetActive(!objectToActivate.activeSelf);
        //}
        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        //}
        //else if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        //}



    }
    private void FixedUpdate()
    {
        FU_Movimiento();
        
        
    }
    //public static int Quesos
    //{
    //    get => _quesos;
    //    set
    //    {
    //        _quesos = value;
    //    }
    //}

    public TMP_Text contador;
    public int _quesos;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            if (InventarioRaton.HayEspacios)
            {
                Item item = other.GetComponent<Item>();
                InventarioRaton.Agregarltem(item.ItemSo);
                if(item.gameObject.CompareTag("Queso"))
                {
                    _quesos++;
                }
                contador.text = _quesos.ToString();
                other.gameObject.SetActive(false);
                //Destroy(other.gameObject);
            }
            
        }
    }
    #endregion Core

    #region Ataque
    [Header("Ataque")]
    [SerializeField] private int ataqueDamage;
    [SerializeField] private AnimationClip animacionAtaque;
    private bool atacando;

    public GameObject objectToActivate;
    //public void Ataque()

    //{
    //    if (Input.GetMouseButtonDown(1))
    //        animator.SetTrigger("Ataque1");
    //    return;
    //    //if (atacando)
    //    //    return;

    //    //atacando = true;
    //    //animator.SetTrigger("Ataque1");
    //    //StartCoroutine(TimerAtaque());
    //}
    public IEnumerator TimerAtaque()
    {
        yield return new WaitForSeconds(animacionAtaque.length);
        atacando = false;
    }

    public void OnAtaqueTriggerEnter(Enemigo enemigo)
    {
        //enemigo.vida -= ataqueDamage;
    }

    #endregion Ataque 

    #region Movimiento
    private Vector3 axis;
    public float rotationSpeed = 100f;
    public float movementSpeed = 5f;

    private void U_Movimiento()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        axis = new Vector3(x, 0, z);
        Vector3 movement = new Vector3(x, 0, z).normalized;

        //Rotar hacia donde mira, verificar porque avanza automatico
        //Vector3 lookDirection = transform.forward;

        //if (movement.magnitude > 0)
        //{
        //    Quaternion targetRotation = Quaternion.LookRotation(movement);
        //    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360f * Time.deltaTime);
        //    lookDirection = movement;
        //}

        //transform.Translate(lookDirection * movementSpeed * Time.deltaTime, Space.World);
    
}
   
    private void FU_Movimiento()
    {
        if (axis != Vector3.zero)
        {
            transform.position += axis.normalized * VelocidadMovimiento * Time.deltaTime;
            animator.SetBool("Moverse", true);
        }
        else
        {
            animator.SetBool("Moverse", false);
        }
    }

    public float estamina, MaxStamina;
    public float estaminareg;
    public float runcost;
    public float stealcost;
    public bool running;
    public bool stealth;
    public Image StaminaBar;
    public Image IndicadorRun;
    public Image IndicadorSig;
    public void Correr()
    {

        if (Input.GetKeyDown(KeyCode.R)) 
        {
            running = true;
            IndicadorRun.gameObject.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.R)) 
        {
            running = false;
            IndicadorRun.gameObject.SetActive(false);
        }

        if(running && axis != Vector3.zero) 
        {
            transform.position += axis.normalized * VelocidadCorrer * Time.deltaTime;
            estamina -= runcost * Time.deltaTime;
            if(estamina < 0) estamina = 0;
            StaminaBar.fillAmount = estamina/MaxStamina;
        }
    }

   
    public void Sigilo()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            stealth = true;
            IndicadorSig.gameObject.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            stealth = false;
            IndicadorSig.gameObject.SetActive(false);
        }

        if (stealth && axis != Vector3.zero)
        {
            transform.position -= axis.normalized * VelocidadSigilo * Time.deltaTime;
            estamina -= stealcost * Time.deltaTime;
            if (estamina < 0) estamina = 0;
            StaminaBar.fillAmount = estamina / MaxStamina;
        }
    }

    #endregion Movimiento

    #region Estamina

    //public Transform refugio;
    public void Estamina()
    {
        if (estamina <= MaxStamina)
            estamina += estaminareg * Time.deltaTime;
        StaminaBar.fillAmount = estamina / MaxStamina;
        //transform.position = refugio.position;
        
    }


    #endregion Estamina
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "enemigo")
        {
            Destroy(gameObject);
        }
    }

}
