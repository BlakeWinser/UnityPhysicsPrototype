using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using Steamworks.Data;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class NetworkingTest : MonoBehaviour
{
    public static NetworkingTest Instance;
    public int playerServerAmount;

    private string steamName;
    private float steamID;
    private string steamIDString;
    private bool connectedToSteam;

    public bool LobbyPartnerDisconnected;
    public Lobby currentLobby;
    private Lobby hostedMultiplayerLobby;
    private string staticDataString;

    //Steam Friends
    private Friend lobbyPartner;
    public Friend LobbyPartner;
    public SteamId OpponentSteamId;
    private string ownerNameDataString;
    private string isFriendLobby;

    // Start is called before the first frame update
    void Start()
    {
        SteamworksInitialisation();

    }

    // Update is called once per frame
    void Update()
    {
        SteamClient.RunCallbacks();
    }

    private void OnApplicationQuit()
    {
        SteamClient.Shutdown();
    }

    void SteamworksInitialisation()
    {
        try
        {
            //Create steam client (Change app ID to steamworks ID once published).
            SteamClient.Init(480, true);

            if (!SteamClient.IsValid)
            {
                Debug.Log("Steam Client not valid");
                throw new Exception();
            }

            //Declare Steamworks with associated variables.
            steamName = SteamClient.Name;
            steamID = SteamClient.SteamId;
            steamIDString = SteamClient.SteamId.ToString();
            connectedToSteam = true;

            //Debug
            Debug.Log("Steam Initialised: " + steamName);
            Debug.Log("Steam ID: " + steamIDString);
        }
        catch (Exception e)
        {
            // Something went wrong - it's one of these:
            //
            //     Steam is closed?
            //     Can't find steam_api dll?
            //     Don't have permission to play app?
            //

            //Cancel connection request, print error to user.
            connectedToSteam = false;
            steamIDString = "NoSteamID";
            Debug.Log("Error Connecting to Steam");
            Debug.Log(e);
        }
    }

    public bool ConnectedToSteam()
    {
        return connectedToSteam;
    }

}