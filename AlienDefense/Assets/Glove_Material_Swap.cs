using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glove_Material_Swap : MonoBehaviour {
	[SerializeField] MeshRenderer leftHand;
	[SerializeField] MeshRenderer rightHand;

	void Start()
	{
		/*
		switch (OptionsSettings.glove_col) {
		case OptionsSettings.GLOVE_COL.BLUE:
			leftHand.materials [0].color = new Color (0f,1f,1f);
			leftHand.materials [1].color = new Color (1f,1f,1f);
			rightHand.materials [0].color = new Color(0f,1f,1f);
			rightHand.materials [1].color = new Color(1f,1f,1f);
			break;
		case OptionsSettings.GLOVE_COL.RED:
			leftHand.materials [0].color = new Color (0.7f,0f,0f);
			leftHand.materials [1].color = new Color (1f,1f,1f);
			rightHand.materials [0].color = new Color(0.7f,0f,0f);
			rightHand.materials [1].color = new Color(1f,1f,1f);
			break;
		case OptionsSettings.GLOVE_COL.GREEN:
			leftHand.materials [0].color = new Color (0f,0f,0f);
			leftHand.materials [1].color = new Color (1f,1f,1f);
			rightHand.materials [0].color = new Color(0f,0f,0f);
			rightHand.materials [1].color = new Color(1f,1f,1f);
			break;
		}
		*/
	}
}
