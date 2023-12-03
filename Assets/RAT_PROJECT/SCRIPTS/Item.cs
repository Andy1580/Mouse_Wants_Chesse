using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemSo itemSo;
    public ItemSo ItemSo => itemSo;

    [SerializeField] private bool usabe;
    public bool Usable => usabe;

    public void Usar(Jugador jugador)
    {
        IAccionItem accionItem = GetComponent<IAccionItem>();
        accionItem.UsarItem(jugador);


    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "enemigo")
        {
            Destroy(gameObject);
        }
    }
}
