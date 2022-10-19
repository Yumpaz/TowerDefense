using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private int life, attack, speed, cost, range, type;

    void Start()
    {
        switch (type)
        {
            case (0):
                life = 3;
                attack = 2;
                speed = 0;
                cost = 2;
                range = 2;
                break;
            case (1):
                life = 4;
                attack = 3;
                speed = 0;
                cost = 3;
                range = 2;
                break;
        }
    }

    private void Update()
    {

    }
}
