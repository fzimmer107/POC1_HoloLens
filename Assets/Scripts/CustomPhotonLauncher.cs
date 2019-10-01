using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CustomPhotonLauncher : MonoBehaviourPunCallbacks
{

    string gameVersion = "1";
    PhotonView myPhotonView;
    

    // Start is called before the first frame update
    void Start()
    {
        Connect();
        myPhotonView = GetComponent<PhotonView>();
    }

    public void Connect()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {

        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        gameObject.GetComponent<SpatialMeshHandler>().meshObserver.Suspend();
        Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
    }


    public override void OnJoinRandomFailed(short returnCode, string message)
    {

        // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
        PhotonNetwork.CreateRoom(null, new RoomOptions());
    }

    public override void OnJoinedRoom()
    {
        gameObject.GetComponent<SpatialMeshHandler>().meshObserver.Resume();


        Debug.Log(" OnJoinedRoom() called by PUN. Now this client is in a room.");

    }

}
