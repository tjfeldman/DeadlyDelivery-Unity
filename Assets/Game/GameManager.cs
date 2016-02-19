using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameObject player;
    public Slider healthbar;
    public Vector3 startSpawn;
    private GameCamera cam;

	// Use this for initialization
	void Start () {
        cam = GetComponent<GameCamera>();
        startSpawn = new Vector3(0, 0, 0);
        SpawnPlayer();
	}

	private void SpawnPlayer()
    {
        // set our camera's target to the player as well as spawn the player
        cam.SetTarget((Instantiate(player, startSpawn, Quaternion.identity) as GameObject).transform);
        player.GetComponent<PlayerHealth>().healthbar = healthbar;
    }
}
