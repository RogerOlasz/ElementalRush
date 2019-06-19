﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryPointsManager : MonoBehaviour
{
    [SerializeField] private int blue_team_victory_points = 0;
    [SerializeField] private int red_team_victory_points = 0;

    public void AddVictoryPoints(int amount, int layer)
    {
        if(amount > 0)
        {
            if (layer == LayerMask.NameToLayer("TeamBlue"))
            {
                blue_team_victory_points += amount;
            }
            else if (layer == LayerMask.NameToLayer("TeamRed"))
            {
                red_team_victory_points += amount;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        blue_team_victory_points = 0;
        red_team_victory_points = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
