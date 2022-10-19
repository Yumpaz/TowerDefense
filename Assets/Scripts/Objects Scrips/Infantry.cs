using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infantry : MonoBehaviour
{
    private int life, attack, speed, cost, range, type;

    void Start()
    {
        switch (type)
        {
            case (0):
                life = 4;
                attack = 1;
                speed = 2;
                cost = 1;
                range = 2;
                break;
            case (1):
                life = 6;
                attack = 2;
                speed = 1;
                cost = 2;
                range = 2;
                break;
            case (2):
                life = 8;
                attack = 1;
                speed = 2;
                cost = 3;
                range = 1;
                break;
        }
    }

    private void Update()
    {

    }
}
