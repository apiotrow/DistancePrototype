﻿using UnityEngine;
using System.Collections;

public class CubeController: Photon.MonoBehaviour {
	
	private Vector3 correctPlayerPos = Vector3.zero; // We lerp towards this
	private Quaternion correctPlayerRot = Quaternion.identity; // We lerp towards this
	PhotonView pv;


	void Start(){
		pv = GetComponent<PhotonView>();
	}

	void Update()
	{


		if (!photonView.isMine)
		{
			transform.position = Vector3.Lerp(transform.position, this.correctPlayerPos, Time.deltaTime * 5);
			transform.rotation = Quaternion.Lerp(transform.rotation, this.correctPlayerRot, Time.deltaTime * 5);
		}
	}
	
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			// We own this player: send the others our data
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
			

		}
		else
		{
			// Network player, receive data
			this.correctPlayerPos = (Vector3)stream.ReceiveNext();
			this.correctPlayerRot = (Quaternion)stream.ReceiveNext();
			

		}
	}
}