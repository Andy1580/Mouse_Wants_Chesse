
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventarioRaton : MonoBehaviour
{
    #region CORE
    private static InventarioRaton self;
    private Canvas canvas;
    public ItemSo ItemSo;
    private void Awake()
    {
        self = this;
        canvas = GetComponent<Canvas>();
        Awake_Slots();
        SlotSeleccionado = null;

    }

private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            canvas.enabled = !canvas.enabled;
    }
    #endregion CORE

    #region Panel Info

    [SerializeField] private Image icono;
    [SerializeField] private TMP_Text nombre;
    [SerializeField] private TMP_Text descripcion;
    [SerializeField] private Button botonUsar;
    [SerializeField] private Button botonSoltar;
    private static SlotItem _slotSelecionado;

    public static SlotItem SlotSeleccionado
    {
        get => _slotSelecionado;
        set
        {
            _slotSelecionado = value;
    
            bool hayItem = false;

            if (value)
                if (value.ItemSo)
                    hayItem = true;

            if (hayItem)
            {
                self.icono.enabled = true;
                self.icono.sprite =  value.ItemSo.Icono;
                self.nombre.text = value.ItemSo.Nombre;
                self.descripcion.text = value.ItemSo.descripcion;
                self.botonUsar.interactable = value.ItemSo.itemGo.Usable;
                self.botonSoltar.interactable = true;
            }
            else
            {
                self.icono.enabled = false;
                    self.nombre.text = string.Empty;
                    self.descripcion.text= string.Empty;
                    self.botonSoltar.interactable  = false;
                    self.botonUsar.interactable = false;

            }
        }
    }

    public void UsarItem()
    {
        SlotSeleccionado.ItemSo.itemGo.Usar(Jugador.Self);
        SlotSeleccionado.ItemSo = null;
        SlotSeleccionado = null;
    }

    public void SoltarItem()
    {
        //if (Input.GetKeyDown(KeyCode.X))
        //{
        Transform trans = Jugador.Self.transform;
        Vector3 posicion = trans.position + (trans.forward * 2);
        Vector3 force = trans.up * 2;
        Instantiate(SlotSeleccionado.ItemSo.itemGo, posicion, Quaternion.identity);
       
        SlotSeleccionado.ItemSo = null;
        SlotSeleccionado = null;
        //}
        
    }
    #endregion
    #region Slots

    [SerializeField] private Transform PanelSlots;
    private static SlotItem[] slots;
    private static int slotsDisponibles;
    //public void Start()
    //{
    //    Transform trans = Jugador. Self.transform;
    //    Vector3 posicion = trans.position + (trans.forward * 2);
    //    Instantiate(SlotSeleccionado.ItemSo.itemGo, posicion, Quaternion.identity);
    //    SlotSeleccionado.ItemSo = null;
    //    SlotSeleccionado = null;
    //}

    private void Awake_Slots()
    {
        slots = PanelSlots.GetComponentsInChildren<SlotItem>();
        slotsDisponibles= slots.Length; 
    }

    public static void Agregarltem(ItemSo itemSo)
    {
        //Busca en todos tos slots .
        foreach (SlotItem slot in slots)
        {
            //Si ono esta vocio ,
            if (slot.ItemSo == null)
            {
                //Guordo el Ttem
                slot.ItemSo = itemSo;
                slotsDisponibles--;
                return;
            }
        }
    }
    public static bool HayEspacios => slotsDisponibles > 0;

    #endregion Slots

}
