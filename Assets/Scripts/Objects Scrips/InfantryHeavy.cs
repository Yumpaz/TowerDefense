using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfantryHeavy : MonoBehaviour
{
    private int life, attack, speed, cost, range, type;
    public static InfantryHeavy Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        life = 6;
        attack = 2;
        speed = 1;
        cost = 2;
        range = 2;
    }

    private void Update()
    {

    }
    public int GetCost()
    {
        return cost;
    }
}
