using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : ItemController
{
    PlayerController playerController;
    protected override void ItemGain()
    {
        playerController = base.player.GetComponent<PlayerController>();
        if(playerController.Bomb < 3)
        {
            playerController.Bomb++;
            UIManager.instance.BombCheck(playerController.Bomb); // wow
        }
    }
}
