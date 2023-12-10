using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotItem : MonoBehaviour
{
    private ItemSo _itemSo;
    public Image icono;
    void Awake()
    {
        icono = GetComponent<Image>();
    }

    public void Seleccionar()
    {
        InventarioRaton.SlotSeleccionado = this;
    }
    public ItemSo ItemSo
    {
        get => _itemSo;
        set
        {
            _itemSo = value;
            if (value)
            {
                icono.enabled = true;
                icono.sprite = value.Icono;
            }
            else
                {
                icono.enabled = false;
            }

        }
 
}

}
