using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OrderSlot : MonoBehaviour
{

    public enum Flavor { Lightning, Cherry, Apple, Honey }
        [Header("Ingredients")]
    [Header("Flavor Settings")]
    [Tooltip("Set the Flavor of Beverage")]
    public Flavor Flavoring;

    public enum Color { Clear, Red, Green, Brown}
    [Header("Color Settings")]
    public Color Coloring;

    public enum Glass { Shot, Double, Mason, Decanter}
    [Header("Glass Settings")]
    public Glass GlassType;

    void Start()
    {

    }

}
