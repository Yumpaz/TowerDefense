using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfantryKiller : MonoBehaviour
{
    private int life, attack, speed, cost, range, type, x, y;

    void Start()
    {
        life = 8;
        attack = 1;
        speed = 2;
        cost = 3;
        range = 1;
    }

    public void UpdatePosition(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public int GetX()
    {
        return x;
    }

    public int GetY()
    {
        return y;
    }

    private void Update()
    {
        
    }
}
