using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerNameInputManager : MonoBehaviour
{
    public void SetPlayerName(string playerName)
    {
        if (string.IsNullOrWhiteSpace(playerName))
        {
            return;
        }
        PhotonNetwork.NickName = playerName;
    }
}
