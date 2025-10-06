using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUInteraction : MonoBehaviour
{
    public CPUPlayer cpuPlayer;
    public void OnClicked()
    {
        GameManager.Instance.humanPlayer.OnCPUPlayerClicked(cpuPlayer);
    }
}
