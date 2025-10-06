using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUPlayer : PlayerBase
{
    public override void TakeTurn()
    {
        if (hand.Count == 0) return;
        GameManager.Instance.StartCoroutine(TakeTurnCoroutine());
    }

    IEnumerator TakeTurnCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        string rankToAsk = hand[Random.Range(0, hand.Count)].rank;
        PlayerBase target = GameManager.Instance.GetRandomOpponent(this);

        GameManager.Instance.ShowNarration(name + " asks " + target.name + " for " + rankToAsk + "s.");
        yield return new WaitForSeconds(2f);

        GameManager.Instance.AskForCard(rankToAsk, this, target);
    }
}
