using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClicker : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("clicked on " + hit.collider.gameObject.name);

                DeckInteraction deck = hit.collider.GetComponent<DeckInteraction>();
                if (deck != null)
                {
                    deck.OnClicked();
                }

                CPUInteraction cpu = hit.collider.GetComponent<CPUInteraction>();
                if (cpu != null)
                {
                    cpu.OnClicked();
                }
            }
        }
    }
}
