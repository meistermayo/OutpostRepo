using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {
	Animator animator;
	[SerializeField] GameObject menu1;
	[SerializeField] GameObject menu2;
	[SerializeField] Text tankText;
	[SerializeField] Text fighterText;
	[SerializeField] Text footmanText;
	[SerializeField] Text turretText;

	void Start()
	{
		animator = GetComponent<Animator> ();
	}

	void Update()
	{
		if (Input.anyKeyDown) {
			animator.SetTrigger ("SkipOpening");
		}
	}

	public void PlayButton()
	{
		SceneManager.LoadScene ("_TestScene");
	}

	public void ExitButton()
	{
		Application.Quit ();
	}

	public void OptionsButton()
	{
		menu1.SetActive (false);
		menu2.SetActive (true);
	}

	public void ReturnButton()
	{
		menu2.SetActive (false);
		menu1.SetActive (true);
	}

	public void ToggleTanks()
	{
		OptionsSettings.tanksOn = !OptionsSettings.tanksOn;
		tankText.text = "TANKS: " + GetStringBool (OptionsSettings.tanksOn);
	}

	public void ToggleFighters()
	{
		OptionsSettings.fightersOn = !OptionsSettings.fightersOn;
		fighterText.text = "FIGHTERS: " + GetStringBool (OptionsSettings.fightersOn);
	}

	public void ToggleFootmen()
	{
		OptionsSettings.footmenOn = !OptionsSettings.footmenOn;
		footmanText.text = "FOOTMEN: " + GetStringBool (OptionsSettings.footmenOn);
	}

	public void ToggleTurrets()
	{
		OptionsSettings.turretsOn = !OptionsSettings.turretsOn;
		turretText.text = "TURRETS: " + GetStringBool (OptionsSettings.turretsOn);
	}

	public void Set_Player_HP(string value)
	{
		if (value == "")
			OptionsSettings.playerHP_Mult = 1f;
		else
			OptionsSettings.playerHP_Mult = float.Parse(value);
	}

	public void Set_Player_Damage(string value)
	{
		if (value == "")
			OptionsSettings.playerDamage_Mult = 1f;
		else
		OptionsSettings.playerDamage_Mult = float.Parse(value);
	}

	public void Set_Player_Ammo(string value)
	{
		if (value == "")
			OptionsSettings.playerAmmo_Mult = 1f;
		else
		OptionsSettings.playerAmmo_Mult =  float.Parse(value);
	}

	public void Set_Enemy_HP(string value)
	{
		if (value == "")
			OptionsSettings.enemyHP_Mult = 1f;
		else
		OptionsSettings.enemyHP_Mult =float.Parse(value);
	}

	public void Set_Enemy_Damage(string value)
	{
		if (value == "")
			OptionsSettings.enemyDamage_Mult = 1f;
		else
		OptionsSettings.enemyDamage_Mult = float.Parse(value);
	}

	public void Set_Dropship_Speed(string value)
	{
		if (value == "")
			OptionsSettings.dropshipSpeed_Mult = 1f;
		else
		OptionsSettings.dropshipSpeed_Mult = float.Parse(value);
	}

	public void Set_Player_Speed(string value)
	{
		if (value == "")
			OptionsSettings.playerSpeed_Mult = 1f;
		else
		OptionsSettings.playerSpeed_Mult = float.Parse(value);
	}

	public void Set_Gravity(string value)
	{
		if (value == "")
			Physics.gravity = Vector3.down * 20f;
		else
		Physics.gravity = Vector3.down * float.Parse(value);
	}

	string GetStringBool (bool killMe)
	{
		if (killMe)
			return "YES";
		else
			return "NO";
	}

	public void Set_ItemRespawn_Mult(string value)
	{
		if (value == "")
			OptionsSettings.itemRespawn_Mult = 1f;
		else
			OptionsSettings.itemRespawn_Mult = float.Parse(value);
	}

	public void Set_GloveCol(int value)
	{
		OptionsSettings.glove_col = (OptionsSettings.GLOVE_COL)value;
	}
}
