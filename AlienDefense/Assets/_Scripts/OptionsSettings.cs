using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsSettings {
	public static bool tanksOn = true;
	public static bool fightersOn = true;
	public static bool footmenOn = true;
	public static bool turretsOn = true;

	public static float playerHP_Mult = 1f;
	public static float playerAmmo_Mult = 1f;
	public static float playerDamage_Mult = 1f;
	public static float playerSpeed_Mult = 1f;

	public static float enemyHP_Mult = 1f;
	public static float enemyDamage_Mult = 1f;
	public static float dropshipSpeed_Mult = 1f;

	public static float itemRespawn_Mult = 1f;
	public enum GLOVE_COL{
		RED=0,BLUE,GREEN
	}
	public static GLOVE_COL glove_col;
}
