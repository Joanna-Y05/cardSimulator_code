using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Card card;
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("on object bounds");
        GameManager.Instance.ShowHoverText(card.GetCardName());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("out of object bounds");
        GameManager.Instance.ShowHoverText("");
    }

}
