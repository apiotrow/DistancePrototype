using UnityEngine;
using System.Collections;

public class CubeController: Photon.MonoBehaviour {
	
	private Vector3 correctPlayerPos = Vector3.zero; // We lerp towards this
	private Quaternion correctPlayerRot = Quaternion.identity; // We lerp towards this
	private Vector3 correctPlayerVel = Vector3.zero;
	private Vector3 correctPlayerAngVel = Vector3.zero;
	PhotonView pv;
	private float interpolationConstant = 0.1f;
	float timeDeltaMultiplier = 20f;


	void Start(){
		pv = GetComponent<PhotonView>();
	}

	void Update()
	{


		if (!photonView.isMine)
		{
			if(this.GetComponent<Rigidbody>() != null){
				this.GetComponent<Rigidbody>().useGravity = false;
			}
			//transform.position = Vector3.Lerp(transform.position, this.correctPlayerPos, Time.deltaTime * 5);
			//transform.rotation = Quaternion.Lerp(transform.rotation, this.correctPlayerRot, Time.deltaTime * 5);
			transform.localPosition = Vector3.Lerp (transform.localPosition,
			                                        Vector3.Lerp(transform.position, this.correctPlayerPos, Time.deltaTime * timeDeltaMultiplier),
			                                        interpolationConstant);
			transform.localRotation = Quaternion.Slerp (transform.localRotation,
			                                            Quaternion.Slerp(transform.rotation, this.correctPlayerRot, Time.deltaTime * timeDeltaMultiplier),
			                                            interpolationConstant);
			rigidbody.velocity = Vector3.Lerp (rigidbody.velocity,
			                                   Vector3.Lerp (rigidbody.velocity, this.correctPlayerVel, Time.deltaTime * timeDeltaMultiplier),
			                                   interpolationConstant);
			rigidbody.angularVelocity = Vector3.Lerp (rigidbody.angularVelocity,
			                                          Vector3.Lerp (rigidbody.angularVelocity, this.correctPlayerAngVel, Time.deltaTime * timeDeltaMultiplier),
			                                          interpolationConstant);
		}
	}
	
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			// We own this player: send the others our data
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);

			stream.SendNext(rigidbody.velocity);
			stream.SendNext(rigidbody.angularVelocity);
			

		}
		else
		{
			// Network player, receive data
			this.correctPlayerPos = (Vector3)stream.ReceiveNext();
			this.correctPlayerRot = (Quaternion)stream.ReceiveNext();
			this.correctPlayerVel = (Vector3)stream.ReceiveNext();
			this.correctPlayerAngVel = (Vector3)stream.ReceiveNext();
		}
	}
}