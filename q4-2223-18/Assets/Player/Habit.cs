using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habit : MonoBehaviour
{
    [SerializeField] private double boost;
    [SerializeField] private double debuff;
    public HabitType habitType; 

    public string name;
    [TextArea]
    public string explanation;

    public double Boost { get => boost; set => boost = value; }
    public double Debuff { get => debuff; set => debuff = value; }

    private void Start()
    {
        explanation += $"\n +{boost * 100}% increase to ";
        explanation += (habitType != HabitType.Defender) ? $"{habitType} attacks" : "defense";  
    }
}
public enum HabitType
{
    Magic,
    Physical,
    Defender
}
