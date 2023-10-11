using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Vida : MonoBehaviour
{

    #region Componentes
    protected Animator animator;
    #endregion Componentes
    #region Core
    protected void Awake()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
        Awake_Stats();
    }
    #endregion Core

    #region Stats
    //atributos
    //[SerializeField] private string _nombre;
    //[SerializeField] private int _vida;
    private int vidaMax;
    [SerializeField] private float _velocidadMovimiento;
    [SerializeField] private float _velocidadSigilo;
    [SerializeField] private float _velocidadCorrer;

    //[SerializeField] private int _nivel;

    //Componentes UI
    [Header ("Componentes UI")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private TMP_Text txt;
    [SerializeField] private Image barraVida;

    private void Awake_Stats()
    {
    //vidaMax = _vida;
    //vida = _vida;
    //Nombre = _nombre;
    }

    private void LateUpdate()
    {
        canvas.transform.LookAt(Camera.main.transform.position);
    }
    public void Inmunerabilidad(float duracion)
    {

    }

    //public string Nombre
    //{
    //    get => _nombre; 
    //    set
    //    {
    //        _nombre = value;
    //        txt.text = "Lv" + _nivel + "_" + _nombre;
    //    }
    //}
    //public int Nivel
    //{
    //    get => _nivel;
    //    set
    //    {
    //        _nivel = value;
    //        Nombre = _nombre;
    //    }
    //}

    //public virtual int vida
    //{
    //    get => _vida;
    //    set
    //    {
    //        if (value <= 0)
    //        {
    //            Morir();
    //            _vida = 0;
    //        }
    //        else if (value >= vidaMax)
    //        {
    //            _vida = vidaMax;
    //        }
    //        else
    //        {
    //            _vida = value;

    //            barraVida.fillAmount = (float)_vida / vidaMax;
    //        }
    //    }

    //}
        private void Morir()
    {
        animator.SetBool("Muerto", true);
        canvas.enabled = false;
    }

    public virtual float VelocidadMovimiento
    {
        get => _velocidadMovimiento;
        set
        {
            _velocidadMovimiento = value;
            animator.SetFloat("velocidadMovimiento", value);
        }
    }

    public virtual float VelocidadCorrer
    {
        get => _velocidadCorrer;
        set
        {
            _velocidadCorrer = value;
            animator.SetFloat("velocidadCorrer", value);
        }
    }

    public virtual float VelocidadSigilo
    {
        get => _velocidadSigilo;
        set
        {
            _velocidadSigilo = value;
            animator.SetFloat("velocidadSigilo", value);
        }
    }

    #endregion Stats
}
