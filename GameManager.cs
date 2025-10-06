using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public DeckManager deckManager;
    public HumanPlayer humanPlayer;
    public List<CPUPlayer> cpuPlayers;
    public Text narrationText;
    public Text hoverText;
    public Transform[] playerCardSlots;
    public GameObject cardPrefab;

    private string selectedRank;

    void Awake()
    {
        Instance = this;
    }

    public void StartGame()
    {
        narrationText.text = "";
        hoverText.text = "";
        List<PlayerBase> allPlayers = new List<PlayerBase> { humanPlayer };
        allPlayers.AddRange(cpuPlayers);

        foreach (var player in allPlayers)
        {
            for (int i = 0; i < 7; i++)
            {
                player.AddCard(deckManager.DrawCard());
            }
            player.CheckForSets();
        }
            UpdatePlayerHandVisuals();
            humanPlayer.TakeTurn();
    }

    public void SetSelectedRank(string rank)
    {
        selectedRank = rank;
    }
    public string GetSelectedRank()
    {
        return selectedRank;
    }

    public void AskForCard(string rank, PlayerBase asker, PlayerBase target)
    {
        Debug.Log(asker.name + " is asking " + target.name + " for " + rank);
        List<Card> matchingCards = target.hand.FindAll(c => c.rank == rank);

        if (matchingCards.Count > 0)
        {
            Debug.Log("match found! " + matchingCards.Count + " cards.");
            foreach (var card in matchingCards)
            {
                asker.AddCard(card);
            }
            target.hand.RemoveAll(c => c.rank == rank);
            UpdatePlayerHandVisuals();
            asker.CheckForSets();

            if (asker.hand.Count > 0)
            {
                asker.TakeTurn();
            }
            else
            {
                Debug.Log("asker has no more cards after taking cards. skipping turn");
                NextTurn(asker);
            }
        }
        else
        {
            if (deckManager.deck.Count > 0)
            {
                ShowNarration("go fish");
                Debug.Log("go fish");

                Card drawn = deckManager.DrawCard();
                if (drawn != null)
                {
                    asker.AddCard(drawn);
                    UpdatePlayerHandVisuals();
                    ShowNarration(asker.name + " drew a card");
                    Debug.Log("card drawn");

                    asker.CheckForSets();

                    if (drawn.rank == rank && asker.hand.Count > 0)
                    {
                        Debug.Log("drew card they asked for go again");
                        asker.TakeTurn();
                    }
                    else
                    {
                        Debug.Log("didn't draw requested card, next players turn");
                        NextTurn(asker);
                    }
                }
                else
                {
                    Debug.Log("deck is empty");
                    ShowNarration("deck is empty. skipping draw");
                    NextTurn(asker);
                }
            }
            else
            {
                ShowNarration("deck is empty. skipping draw");
                Debug.Log("deck is empty, moving to next turn");
                NextTurn(asker);
            }
        } 
        
    }
    void NextTurn(PlayerBase current)
    {
        List<PlayerBase> turnOrder = new List<PlayerBase> { humanPlayer };
        turnOrder.AddRange(cpuPlayers);

        int currentIndex = turnOrder.IndexOf(current);
        int totalPlayers = turnOrder.Count;

        for (int i = 1; i <= totalPlayers; i++)
        {
            int nextIndex = (currentIndex + i) % totalPlayers;
            PlayerBase nextPlayer = turnOrder[nextIndex];

            if (nextPlayer.hand.Count > 0)
            {
                nextPlayer.TakeTurn();
                return;
            }
        }
        ShowNarration("game over! everyone is out of cards");
    }

    public PlayerBase GetRandomOpponent(PlayerBase self)
    {
        List<PlayerBase> candidates = new List<PlayerBase> { humanPlayer };
        candidates.AddRange(cpuPlayers);
        candidates.Remove(self);
        return candidates[UnityEngine.Random.Range(0, candidates.Count)];
    }
    public void UpdatePlayerHandVisuals()
    {
        foreach (Transform slot in playerCardSlots)
        {
            foreach (Transform child in slot)
            {
                Destroy(child.gameObject);
            }
        }

        for (int i = 0; i < humanPlayer.hand.Count && i < playerCardSlots.Length; i++)
        {
            Card card = humanPlayer.hand[i];
            Transform slot = playerCardSlots[i];
            GameObject cardGo = Instantiate(cardPrefab, slot.position,slot.rotation, slot );
            cardGo.transform.SetParent(playerCardSlots[i], true);

            var renderer = cardGo.GetComponentInChildren<SpriteRenderer>();
            if (renderer != null)
            {
                renderer.sprite = card.cardSprite;
            }

            var interaction = cardGo.GetComponent<DeckInteraction>();
            if (interaction != null)
            {
                interaction.currentCard = card;
            }
        }
    }

    public void ShowNarration(string text)
    {
        narrationText.text = text;
    }
    public void ShowHoverText(string text)
    {
        hoverText.text = text;
    }
}
