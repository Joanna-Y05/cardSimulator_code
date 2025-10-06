using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Card
{
    public string suit;
    public string rank;
    public Sprite cardSprite;

    public Card(string rank, string suit, Sprite sprite = null)
    {
        this.rank = rank;
        this.suit = suit;
        this.cardSprite = sprite;
    }

    public string GetCardName()
    {
        return rank + " of " + suit;
    }
}
