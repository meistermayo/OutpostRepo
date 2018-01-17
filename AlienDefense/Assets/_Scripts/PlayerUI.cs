using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UI_ANIM_HP{HIT=0,HEAL}
public enum UI_ANIM_AMMO{SHOOT=0,EMPTY,SWITCH}
public class PlayerUI : MonoBehaviour {
	static Animator hitAnimator;
	[SerializeField] Text healthTextRef;
	[SerializeField] Text ammoTextRef;
	[SerializeField] Animator hitAnimatorRef;
	[SerializeField]  Text[] gunTextRefs;
	[SerializeField] int bigGunFontRef;
	[SerializeField] int smlGunFontRef;
	static int bigGunFont;
	static int smlGunFont;

	static Text[] gunTexts;
	static Text healthText;
	static Text ammoText;


	void Awake()
	{
		bigGunFont = bigGunFontRef;
		smlGunFont = smlGunFontRef;
		hitAnimator = hitAnimatorRef;
		gunTexts = gunTextRefs;
		ammoText = ammoTextRef;
		healthText = healthTextRef;
	}

	public static void AnimateHit()
	{
		hitAnimator.SetTrigger ("Hit");
	}

	public static void AnimateHit(float angle)
	{
		hitAnimator.SetTrigger ("Hit");
		hitAnimator.SetFloat ("angle",angle);
	}

	public static void UpdateHealthText(float value, float maxValue)
	{
		healthText.text	= value.ToString () + "/" + maxValue.ToString ();
		Animator animator = healthText.GetComponent<Animator> ();
		animator.SetFloat ("HealthPercent",value/maxValue);
	}

	public static void UpdateHealthText(float value, float maxValue, UI_ANIM_HP anim)
	{
		UpdateHealthText (value, maxValue);
		Animator animator = healthText.GetComponent<Animator> ();
		animator.SetFloat ("HealthPercent",value/maxValue);
		switch (anim) {
		case UI_ANIM_HP.HIT:
			animator.SetTrigger ("Hit");
			break;
		case UI_ANIM_HP.HEAL:
			animator.SetTrigger ("Heal");
			break;
		}
	}

	public static void UpdateAmmoText(int value, float maxValue)
	{
		ammoText.text = value.ToString () + "/" + maxValue.ToString ();
	}

	public static void UpdateAmmoText(int value, float maxValue, UI_ANIM_AMMO anim)
	{
		UpdateAmmoText (value, maxValue);
		Animator animator = ammoText.GetComponent<Animator> ();
		switch (anim) {
		case UI_ANIM_AMMO.SHOOT:
			animator.SetTrigger ("Shoot");
			break;
		case UI_ANIM_AMMO.EMPTY:
			animator.SetTrigger ("Empty");
			break;
		case UI_ANIM_AMMO.SWITCH:
			animator.SetTrigger ("Switch");
			break;
		}
	}

	public static void ChangeWeapon(int i)
	{
		if (i > -1 && i < gunTexts.Length) {
			for (int j=0; j<gunTexts.Length; j++)
			{
				gunTexts [j].fontSize = smlGunFont;
			}
			gunTexts [i].fontSize = bigGunFont;
		}
	}
}
