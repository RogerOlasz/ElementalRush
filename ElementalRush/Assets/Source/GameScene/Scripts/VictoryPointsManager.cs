using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryPointsManager : MonoBehaviour
{
    [SerializeField] private int blue_team_victory_points = 0;
    [SerializeField] private int red_team_victory_points = 0;

    public Text enemy_team_marquee;
    public Text ally_team_marquee;

    public void AddVictoryPoints(int amount, int layer)
    {
        if(amount > 0)
        {
            if (layer == LayerMask.NameToLayer("TeamBlue"))
            {
                blue_team_victory_points += amount; 
                
                if (layer == this.gameObject.layer)
                {
                    ally_team_marquee.text = blue_team_victory_points.ToString();
                }

                if (layer != this.gameObject.layer)
                {
                    enemy_team_marquee.text = blue_team_victory_points.ToString();
                }
            }
            else if (layer == LayerMask.NameToLayer("TeamRed"))
            {
                red_team_victory_points += amount;

                if (layer == this.gameObject.layer)
                {
                    ally_team_marquee.text = red_team_victory_points.ToString();
                }

                if (layer != this.gameObject.layer)
                {
                    enemy_team_marquee.text = red_team_victory_points.ToString();
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        blue_team_victory_points = 0;
        red_team_victory_points = 0;

        ally_team_marquee.text = blue_team_victory_points.ToString();
        enemy_team_marquee.text = red_team_victory_points.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
