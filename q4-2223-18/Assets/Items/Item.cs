using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    
    public Sprite defaultIcon;
    public Sprite selectedIcon;
    public int number;
    public string name;
    public ItemFunction function;
    public int boost; 
    
}
public enum ItemFunction
{
    healing,
    attackBoost
}
