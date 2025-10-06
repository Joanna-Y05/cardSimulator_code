using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class DeckInteraction : MonoBehaviour
{
    public Card currentCard;

    public void OnClicked()
    {
        GameManager.Instance.humanPlayer.OnDeckClicked();
        GameManager.Instance.humanPlayer.SelectCard(currentCard);
    }
}
