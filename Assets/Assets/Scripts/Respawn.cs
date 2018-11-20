using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {

    public GameManager _game;

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Respawn")
        {
            _game.StartCoroutine("RespawnEffect");
        }

        if(other.tag == "Finish")
        {
            _game.Finish();
        }

        if(other.tag == "ElectricOrb")
        {
            _game.StartCoroutine("Shock");
        }
	}
}
