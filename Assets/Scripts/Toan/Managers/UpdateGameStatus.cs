using EnumCollection;
using Manager;
using System.Collections.Generic;
using UnityEngine;

public class UpdateGameStatus : MonoBehaviour
{
    private List<Player> players;
    private void Awake()
    {
        players = new List<Player>();
    }
    public void AddPlayer(Player player)
    {
        if(!players.Contains(player))
        {
            players.Add(player);
        }
    }

    private void UpdatePlayerState()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if(players[i] != null && !players[i].IsAlive())
            {
                players[i].Lose();
            }
        }
    }
}
