using UnityEngine;
using System.Collections;

public class CreateCubeOnStart : Photon.MonoBehaviour {
	private PhotonView myPhotonView;

	// Use this for initialization
	void Start () {
		GameObject block = PhotonNetwork.Instantiate("Cube", new Vector3(0f, 3f, 0f), Quaternion.identity, 0);
		myPhotonView = block.GetComponent<PhotonView>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
