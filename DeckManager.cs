using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public List<Sprite> cardSprites;
    private string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
    private string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K"};

    // Start is called before the first frame update
    void Start()
    {
        GenerateDeck();
        ShuffleDeck();
    }

    void GenerateDeck()
    {
        foreach (var suit in suits)
        {
            foreach (var rank in ranks)
            {
                string spriteName = suit + "_card_" + rank;
                Sprite sprite = cardSprites.Find(s => s.name == spriteName);
                if(sprite == null) Debug.LogWarning("missing sprite: " + spriteName);
                deck.Add(new Card(rank, suit, sprite));
            }
        }
    }

    void ShuffleDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            Card temp = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    public Card DrawCard()
    {
        if (deck.Count == 0) return null;
        Card drawnCard = deck[0];
        deck.RemoveAt(0);
        return drawnCard;
    }
}
