using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{
    public delegate void HandleNewPlayerAdd(Player player);
    public event HandleNewPlayerAdd OnNewPlayerAddEvent;
    public delegate void HandlePlayerRemove(Player player);
    public event HandlePlayerRemove OnPlayerRemoveEvent;

    public delegate void HandlePrimaryPlayer(Player player);
    public event HandlePrimaryPlayer OnSetPrimaryPlayerEvent;

    private PlayerManager() { }
    private static PlayerManager instance;
    public static PlayerManager Instance
    {
        get
        {
            if (instance == null) instance = new PlayerManager();
            return instance;
        }
    }

    private Dictionary<long, Player> players = new Dictionary<long, Player>();

    public Dictionary<long, Player> Players
    {
        get
        {
            return players;
        }
    }

    public Player PrimaryPlayer;

    public Player SetUser(long userid)
    {

        if (players.ContainsKey(userid))
        {
            return players[userid];
        }
        else
        {
            Player player = new Player() { userId = userid };
            players.Add(userid, player);
            if (OnNewPlayerAddEvent != null) OnNewPlayerAddEvent(player);
            return player;
        }
    }

    public void SetPrimaryPlayer(long userID)
    {
        if (players.ContainsKey(userID))
        {
            PrimaryPlayer = players[userID];
            if(OnSetPrimaryPlayerEvent!=null)
            {
                OnSetPrimaryPlayerEvent(PrimaryPlayer);
            }
        }
        else
        {

        }
    }

    public Player GetPrimaryPlayer()
    {
        return PrimaryPlayer;
    }

    public void RemoveUser(long userid)
    {
        if (players.ContainsKey(userid))
        {
            if (OnPlayerRemoveEvent != null) OnPlayerRemoveEvent(players[userid]);
             players[userid].Dispose();
            players.Remove(userid);
        }
        if (PrimaryPlayer != null)
        {
            if (PrimaryPlayer.userId == userid)
            {
                PrimaryPlayer = null;
            }
        }
    }

    public Player GetPlayer(long userid)
    {
        if (players.ContainsKey(userid))
        {
            return players[userid];
        }
        return null;
    }

}
