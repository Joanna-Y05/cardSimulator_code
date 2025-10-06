using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class PlayerBase : MonoBehaviour
{
   

    public List<Card> hand = new List<Card>();
    public List<string> completedSets = new List<string>();
    public GameObject[] setVisuals;

    public void AddCard(Card card)
    {
        hand.Add(card);
    }

    public void CheckForSets()
    {
        Dictionary<string, int> rankCount = new Dictionary<string, int>();
        foreach (var card in hand)
        {
            if (!rankCount.ContainsKey(card.rank))
                rankCount[card.rank] = 0;
            rankCount[card.rank]++;
        }
        foreach (var pair in rankCount)
        {
            if (pair.Value == 4 && !completedSets.Contains(pair.Key))
            {
                completedSets.Add(pair.Key);
                hand.RemoveAll(c => c.rank == pair.Key);
                ShowSet(pair.Key);
                GameManager.Instance.ShowHoverText(pair.Key + " completed!");
            }
        }

        if (this == GameManager.Instance.humanPlayer)
        {
            GameManager.Instance.UpdatePlayerHandVisuals();
        }
    }

    void ShowSet(string rank)
    {
        foreach (GameObject visual in setVisuals)
        {
            if (visual.name == rank + "_Set")
            {
                visual.SetActive(true);
            }
        }
    }
    public abstract void TakeTurn();
}
