using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;

public class StartMenu : MonoBehaviour
{
    
    public GameObject startCam;
    public GameObject gameCam;

    public void SwitchCamera()
    {
        gameCam.SetActive(true);
        GameManager.Instance.StartGame();
        startCam.SetActive(false);
    
    }
}
