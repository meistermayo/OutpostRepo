using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAndSceneChange : MonoBehaviour {
	[SerializeField] Transform location;
	[SerializeField] GameObject[] gameObjectsToTurnOff;
	[SerializeField] GameObject[] gameObjectsToTurnOn;
	[SerializeField] bool loadCheckpointRef;
	static bool loadCheckpoint;

	void Start()
	{
		if (loadCheckpointRef)
			loadCheckpoint = true;
		if (loadCheckpoint)
		{
			CheckPoint (FPS_Controls.Instance.gameObject);
		}
			
	}
	void OnTriggerEnter(Collider other)
	{

		if (other.CompareTag ("Player")) {
			CheckPoint (other.gameObject);
		}
	}

	public void CheckPoint(GameObject other)
	{
			loadCheckpoint = true;
			other.transform.position = location.position;
			other.transform.rotation = location.rotation;
			foreach (GameObject go in gameObjectsToTurnOff) {
				go.SetActive (false);
			}
			foreach (GameObject go in gameObjectsToTurnOn) {
				go.SetActive (true);
			}

	}
}
