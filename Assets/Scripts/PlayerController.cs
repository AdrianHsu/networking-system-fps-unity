using UnityEngine;
using UnityEngine.Networking;

public class PlayerController: NetworkBehaviour 
{
	public UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpsController;
	public Camera fpsCamera;
	public AudioListener audioListener;
	//public ShootingScript shootingScript;

	public override void OnStartLocalPlayer()
	{
		//多人連線所有的Player都有相同的控制項目，所以我們把這些項目都先關閉，只有在一開始控制自己的角色時，以下的控制才打開已防止操作相互干擾。
		fpsController.enabled = true;
		fpsCamera.enabled = true;
		audioListener.enabled = true;
		//shootingScript.enabled = true;

		gameObject.name = "ME";
		base.OnStartLocalPlayer ();
	}

}

