using UnityEngine;

public class RandomMatchmaker : Photon.MonoBehaviour
{
    private PhotonView myPhotonView;

    // Use this for initialization
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
    }

    void OnJoinedLobby()
    {
        Debug.Log("JoinRandom");
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed()
    {
        PhotonNetwork.CreateRoom(null);
    }

    void OnJoinedRoom()
    {
        GameObject monster = PhotonNetwork.Instantiate("monsterprefab", Vector3.zero, Quaternion.identity, 0);
        monster.GetComponent<myThirdPersonController>().isControllable = true;

		GameObject block = PhotonNetwork.Instantiate("Cube", new Vector3(0f, 3f, 0f), Quaternion.identity, 0);

        myPhotonView = monster.GetComponent<PhotonView>();
		myPhotonView = block.GetComponent<PhotonView>();
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());

        if (PhotonNetwork.connectionStateDetailed == PeerState.Joined)
        {
            bool shoutMarco = GameLogic.playerWhoIsIt == PhotonNetwork.player.ID;

            if (shoutMarco && GUILayout.Button("Marco!"))
            {
                myPhotonView.RPC("Marco", PhotonTargets.All);
            }
            if (!shoutMarco && GUILayout.Button("Polo!"))
            {
                myPhotonView.RPC("Polo", PhotonTargets.All);
            }
        }
    }
}
