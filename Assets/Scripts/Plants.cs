using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plants
{
    public int id;
    public string name;
    public float time;
    public int quantity;
    public float sell_price;
    public float buy_price;
    public int season = 0;
    public int sprite_num = 2; // Se empieza a contar desde el 0

    public float grow_tick;

    // Sprite
    public List<Sprite> sprites;
    public int curr_sprite = 0;


    public Plants()
    {
        sprites = new List<Sprite>();
    }

    public void initPlant()
    {
        grow_tick = (float)(Timer.timer.tick) + time;
    }

    public void LoadSprites()
    {
        for (int i = 0; i < sprite_num; i++)
        {
            sprites.Add(Resources.Load<Sprite>(name + i.ToString()));
        }
    }

    public bool Grow(int n)
    {
        if (curr_sprite >= sprite_num)
        {
            GameManager.gm.coins += sell_price;
            GameManager.gm.UpdateCoinsText(GameManager.gm.coins);

            curr_sprite = 0;

            return false;
        }

        if (n >= sprite_num)
            n = 1;

        curr_sprite += n;

        return true;
    }
}