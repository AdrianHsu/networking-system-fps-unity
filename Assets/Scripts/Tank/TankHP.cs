//This script tracks the health of the tower and game status

using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TankHP : NetworkBehaviour
{
	AudioSource damageAudio;		//Audio feedback of hit

    void Awake()
	{
		//Set current lives and get audio component reference
     	damageAudio = GetComponent<AudioSource>();
    }

	void OnTriggerEnter(Collider other)
	{
		//Make sure we can only be hit by enemies and only if tower is alive
		//如果坦克碰到標籤是Bullet的物件，就播放音效並通知Server毀滅此坦克
		if (other.tag != "Bullet")
			return;
		
		damageAudio.Stop ();
		CmdDestoryTank ();
		damageAudio.Play ();

	}

	//告訴Server坦克死了
	[Command]
	void CmdDestoryTank()
	{
		NetworkServer.Destroy(gameObject);
	}
}
