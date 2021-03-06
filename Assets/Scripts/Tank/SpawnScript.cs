﻿//This script spawns our drones at a quasi-random interval

using UnityEngine;
using UnityEngine.Networking;

public class SpawnScript : NetworkBehaviour
{
	public GameObject enemy;			//Enemy drone prefab
	public Transform target;			//Tower transform
	public float spawnInterval = 8f;	//Interval in seconds to spawn at

	float spawnVariance;				//A variance which will be used to randomize the spawn times slightly

	void Start () 
	{
		//Variance is always be half of the total interval (Tip: multiplying by .5 is faster than dividing by 2)
		spawnVariance = spawnInterval * .5f;
		//Invoke our spawn method. Assuming default values, the range for spawn time will be between 
		//4 and 12 seconds
		Invoke ("CmdSpawnTank", spawnInterval + Random.Range(-spawnVariance, spawnVariance));
	}

	void Update()
	{
		//Our minimum spawn time is 1 second
		if (spawnInterval > 1f)
		{
			//every 50 seconds of gameplay reduces the timer by 1 second
			float timeReduction = Time.deltaTime / 50;

			//Ensure we don't go below 1 second and recalculate variance
			spawnInterval = Mathf.Max(1f, spawnInterval - timeReduction);
			spawnVariance = spawnInterval * .5f;
		}
	}

	//告訴Server每固定時間產生坦克到世界上
	[Command]
	void CmdSpawnTank()
	{
		//Instantiate a drone and parent to the spawn point (for organization)
        //GameObject enemyObj = Instantiate (enemy, transform.position, transform.rotation) as GameObject;
		GameObject enemyObj = Instantiate (Resources.Load ("Tank"), transform.position, transform.rotation) as GameObject;
        enemyObj.transform.parent = transform;
		//Let's name it
		enemyObj.name = "Tank";
		//Set the target of the drone
		enemyObj.GetComponent<EnemyNavigation> ().target = target;
		//Invoke spawn method again
		Invoke("CmdSpawnTank", spawnInterval + Random.Range(-spawnVariance, spawnVariance));
		NetworkServer.Spawn (enemyObj);
	}
}
