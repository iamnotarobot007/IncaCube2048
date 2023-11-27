using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public Score score;
    public Player player;

    public void BuyBomb()
    {
        
      PurchaseBomb(200);
      SoundManager.inst.PlaySound(SoundName.ShopClick);

    }

    public void BuyRainbow()
    {
       
        PurchaseRainBow(100);
        SoundManager.inst.PlaySound(SoundName.ShopClick);

    }
    public void PurchaseBomb(int cost)
    {
        if (score.CoinNum >= cost)
        {
            score.CoinNum -= cost;
            player.Bombcount += 1;
            score.UpdateCoinText();
            player.UpdatePowerupCountBomb();
            score.SaveScore();
        }
        else
        {
            Debug.Log("Not enough coins to purchase this item.");
        }
    }
    public void PurchaseRainBow(int cost)
    {
        
        if (score.CoinNum >= cost)
        {
            score.CoinNum -= cost;
            player.RainBowcount += 1;
            score.UpdateCoinText();
            player.UpdatePowerupCountRainbow();
            score.SaveScore();
        }
        else
        {
            Debug.Log("Not enough coins to purchase this item.");
        }
    }
}
