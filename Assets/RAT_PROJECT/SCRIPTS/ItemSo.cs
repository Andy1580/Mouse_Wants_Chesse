using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Item  SO")]
public class ItemSo : ScriptableObject
{
    public string Nombre;
    public Sprite Icono;
    [TextArea] public string descripcion;
    public Item itemGo;
}
