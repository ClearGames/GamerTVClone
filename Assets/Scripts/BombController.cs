using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : ItemController
{
    PlayerController playerController;
    protected override void ItemGain()
    {
        playerController = base.player.GetComponent<PlayerController>();
        if(playerController.Bomb < 4)
        {
            playerController.Bomb++;
        }
    }
}
