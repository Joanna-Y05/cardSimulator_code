using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class HumanPlayer : PlayerBase
{
    private bool deckClicked = false;
    private CPUPlayer selectedCPU = null;
    public Image[] handSlots;
    private int cardDisplayStartIndex = 0;

    public void OnDeckClicked()
    {
       /* if (GameManager.Instance.deckManager.deck.Count == 0)
        {
            GameManager.Instance.ShowNarration("deck is empty");
            Debug.Log("deck empty");
            return;
        }*/
        deckClicked = true;
        Debug.Log("deck clicked. select CPU player");
        GameManager.Instance.ShowNarration("Deck selected, Now pick a player to ask");
    }

    public void OnCPUPlayerClicked(CPUPlayer cpu)
    {
        if (!deckClicked) return;

        selectedCPU = cpu;
        string rank = GameManager.Instance.GetSelectedRank();
        if (rank != null)
        {
            GameManager.Instance.ShowNarration(" you asked " + cpu.name + " for " + rank + "s");
            GameManager.Instance.AskForCard(rank, this, selectedCPU);
            deckClicked = false;
            selectedCPU = null;
        }
        else
        {
            GameManager.Instance.ShowNarration("select a card from your hand first");
        }
    }
    public void SelectCard(Card card)
    {
        GameManager.Instance.SetSelectedRank(card.rank);
        GameManager.Instance.ShowNarration("Selected rank: " + card.rank + ". now click the desk, then a CPU");
    }

    /*public void UpdateHandDisplay()
    {
        for (int i = 0; i < handSlots.Length; i++)
        {
            int cardIndex = cardDisplayStartIndex + i;
            if (cardIndex < hand.Count)
            {
                handSlots[i].sprite = hand[cardIndex].cardSprite;
                handSlots[i].gameObject.SetActive(true);
            }
            else
            {
                handSlots[i].gameObject.SetActive(false);
            }
        }
    }*/

    /*public void NextCardView()
    {
        if (cardDisplayStartIndex + handSlots.Length < hand.Count)
        {
            cardDisplayStartIndex++;
            UpdateHandDisplay();  
        }
    }

    public void PreviousCardView()
    {
        if (cardDisplayStartIndex > 0)
        {
            cardDisplayStartIndex--;
            UpdateHandDisplay();
        }
    }*/
   
   public override void TakeTurn()
    {
        GameManager.Instance.ShowNarration("it is your turn, choose a rank to ask");
        //UpdateHandDisplay();
    }
}
