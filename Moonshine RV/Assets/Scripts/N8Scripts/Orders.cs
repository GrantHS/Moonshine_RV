using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Order", menuName = "Orders")]
public class Orders : ScriptableObject
{
    public string OrderName;
    public string Description;

    public Sprite Artwork;

    public int Timer;

    public GameObject GlassType;
    public Sprite Flavors;

    public int Color;




}
