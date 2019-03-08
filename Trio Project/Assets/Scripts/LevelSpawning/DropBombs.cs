using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBombs : MonoBehaviour
{

    public int TotalBombs;
    public int CurrentBombs;

    public GameObject[] Bombspawners;
	public GameObject BombPrefab;
	private float WaitToSpawn;

    //public int BombLimit;

    // Use this for initialization
    void start()
    {
		CurrentBombs = 0;
		WaitToSpawn = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentBombs >= TotalBombs)
        {
            //print(CurrentBombs);
        }

        else
        {
WaitToSpawn += Time.deltaTime;
if(WaitToSpawn >= 6.0f){
            int random = Random.Range(0, Bombspawners.Length);
			GameObject bomb = Instantiate(BombPrefab, Bombspawners[random].transform.position, Bombspawners[random].transform.rotation);
			CurrentBombs++;
			WaitToSpawn = 0;
}
        }

    }
}
