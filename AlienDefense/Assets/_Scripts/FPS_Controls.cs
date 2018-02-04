using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Controls : MonoBehaviour {
	protected static FPS_Controls instance;
	public static FPS_Controls Instance { get {
			return instance; } }

	protected float h,v;
	protected float mx,my;
	protected bool jumpPressed,jumpHeld;
	protected bool firePressed,fireHeld;
	protected bool altFirePressed,altFireHeld;
	protected bool sprintHeld;

	float prevY;
	public static float maxFallSpeed = -20f;

	protected bool onGround;
	bool canWallrun;

	protected Rigidbody body;
	protected Animator animator;
	protected float cameraYrotation;

	[Header("Physics")]
	[SerializeField] protected float moveSpeed;
	[SerializeField] protected float sprintSpeed;
	[SerializeField] protected float accSpeed;
	[SerializeField] protected float airAccSpeed;
	[SerializeField] protected float jumpSpeed;
	[Header("Control")]
	[SerializeField] protected float mouseSensX;
	[SerializeField] protected float mouseSensY;
	[SerializeField] protected float zoomMouseSensX;
	[SerializeField] protected float zoomMouseSensY;
	[Header("Animation")]
	[SerializeField] protected float headBobDepth;
	[SerializeField] protected float headBobRate;
	[SerializeField] protected float zoomFloat;
	[SerializeField] protected float normalFloat;
	[SerializeField] protected float sprintFloat;
	[SerializeField] protected float fovLerp;

	[SerializeField]bool startWeak;
	//[Header("Combat")]
	WeaponManager weaponManager;

	float headBobAngle;
	Vector3 cameraOrigin;
	Vector3 currentGroundNormal;
	LocalAudioManager audioManager;

	protected virtual void Start () {
		accSpeed *= OptionsSettings.playerSpeed_Mult;
		jumpSpeed *= OptionsSettings.playerSpeed_Mult;
		moveSpeed *= OptionsSettings.playerSpeed_Mult;
		sprintSpeed *= OptionsSettings.playerSpeed_Mult;
		audioManager = GetComponent<LocalAudioManager> ();		

		if (instance == null) {
			instance = this;
			//DontDestroyOnLoad (gameObject);
		}
		//else
		//	DestroyImmediate (gameObject);

		body = GetComponent<Rigidbody> ();
		animator = GetComponentInChildren<Animator> ();
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		cameraOrigin = Camera.main.transform.localPosition;
		weaponManager = GetComponentInChildren<WeaponManager> ();
		if (startWeak) {
			StartCoroutine (StartWeak ());
		}
	}

	IEnumerator StartWeak()
	{
		yield return null;
		PlayerHealth pHealth = GetComponent<PlayerHealth> ();
		pHealth.TakeDamage (pHealth.GetMaxHealth / 2f);
		weaponManager.ChangeWeapon (0);
		weaponManager.AddAmmo (-weaponManager.GetCurrentGun.MaxAmmo / 2);
		weaponManager.ChangeWeapon (1);
		weaponManager.AddAmmo (-weaponManager.GetCurrentGun.MaxAmmo / 2);
		weaponManager.ChangeWeapon (0);
	}

	protected virtual void Update()
	{
		if (prevY < maxFallSpeed && body.velocity.y == 0f)
			audioManager.Play (1);
		prevY = body.velocity.y;
		GetInput ();
		MouseLook ();
		Walk ();
		Jump ();
		WallRun ();
		ChangeWeapon ();
		Fire ();
		Zoom ();
	}

	protected void WallRun()
	{
		body.useGravity = true;
		if (Physics.Raycast (transform.position, transform.right, 1f)||Physics.Raycast (transform.position, -transform.right, 1f)) 
		{
			if (canWallrun && !onGround && new Vector3(body.velocity.x,0f,body.velocity.z).sqrMagnitude > accSpeed) 
			{
				if (jumpPressed) {
					animator.SetBool ("wallrun", false);
					canWallrun = false;
					audioManager.Play (0);
					animator.SetTrigger ("jump");
					body.AddForce (Vector3.up * jumpSpeed, ForceMode.VelocityChange);
				} else if (jumpHeld && body.velocity.sqrMagnitude != 0f) {
					if (body.velocity.y <= 0f) {
						animator.SetBool ("wallrun", true);
						body.useGravity = false;
						body.velocity.Set (body.velocity.x, 0f, body.velocity.z);
					}
				} else
					animator.SetBool ("wallrun", false);
			} else 
				animator.SetBool ("wallrun", false);
		} else 
			animator.SetBool ("wallrun", false);
	}

	protected void GetInput()
	{
		mx = Input.GetAxis ("Mouse X");
		my = Input.GetAxis ("Mouse Y");
		h = Input.GetAxisRaw ("Horizontal");
		v = Input.GetAxisRaw ("Vertical");
		firePressed = Input.GetButtonDown ("Fire1");
		fireHeld = Input.GetButton ("Fire1");
		altFirePressed = Input.GetButtonDown ("Fire2");
		altFireHeld = Input.GetButton ("Fire2");
		sprintHeld = Input.GetKey (KeyCode.LeftShift);
		jumpPressed = Input.GetButtonDown ("Jump");
		jumpHeld = Input.GetButton ("Jump");
	}

	protected void MouseLook()
	{
		float xs, ys;

		xs = mouseSensX;
		ys = mouseSensY;

		if (altFireHeld) {
			if (weaponManager.currentGunIndex == 1) {
				xs = zoomMouseSensX;
				ys = zoomMouseSensY;
			}
		}

		body.MoveRotation(body.rotation * Quaternion.Euler (Vector3.up * mx*xs));

		if (cameraYrotation + my*ys < 90f && cameraYrotation + my*ys > -90f) {
			cameraYrotation += my*ys;
			Camera.main.transform.rotation *= Quaternion.Euler (Vector3.right * -my*ys);
		}
	}
		
	protected virtual void Walk()
	{
		float saveY = body.velocity.y;
		body.velocity = new Vector3 (body.velocity.x, 0f, body.velocity.z);

		Vector3 desiredVelocity = body.velocity + Vector3.Normalize (transform.forward * v + transform.right * h) * ((onGround) ? accSpeed : airAccSpeed);
		{
			body.velocity = desiredVelocity;//Vector3.Normalize (transform.forward * v + transform.right * h) * accSpeed;
			RaycastHit hit;
			if (Physics.Raycast (transform.position, desiredVelocity.normalized, out hit, 1f)
				||Physics.Raycast (transform.position, desiredVelocity.normalized, out hit, 1f)) {
				if (hit.normal == Vector3.right || hit.normal == Vector3.left)
					body.velocity = new Vector3(0f,0f,body.velocity.z);
				else if (hit.normal == Vector3.forward || hit.normal == Vector3.back)
					body.velocity = new Vector3(body.velocity.x,0f,0f);
			}
		}

		if (sprintHeld)
			body.velocity = Vector3.ClampMagnitude (body.velocity, sprintSpeed);
		else
			body.velocity = Vector3.ClampMagnitude (body.velocity, moveSpeed);

		bool moving = (h != 0f || v != 0f) ;
		if (!moving && onGround) {
			body.velocity = Vector3.ClampMagnitude (body.velocity, body.velocity.magnitude * .7f);
		}
		body.velocity = new Vector3 (body.velocity.x, saveY, body.velocity.z);

		animator.SetBool ("isRunning", moving && sprintHeld);
		animator.SetBool ("isJumping", !onGround);
		if (moving && body.velocity.y == 0f) {
			headBobAngle++;
			Camera.main.transform.localPosition = cameraOrigin + Vector3.up * Mathf.Sin (headBobAngle * headBobRate) * headBobDepth;
		}

		CheckSlope ();
	}

	protected virtual void Jump()
	{
		CheckGrounded ();
		if (onGround) {
			if (jumpPressed) {
				audioManager.Play (0);
				animator.SetTrigger ("jump");
				body.AddForce (Vector3.up * jumpSpeed, ForceMode.VelocityChange);
			}
		}
	}

	protected void Fire()
	{
		if (fireHeld) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			if (weaponManager != null)
				weaponManager.Shoot ();	
		} else if (altFireHeld) {
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
				if (weaponManager != null)
					weaponManager.AltShoot ();	
		}
	}

	public bool CheckGrounded()
	{
		RaycastHit hit;

		onGround = (Physics.Raycast (transform.position + Vector3.up * .1f, -currentGroundNormal, out hit, .5f));

		if (onGround) {
			canWallrun = true;
		}

		DebugText.SetText (currentGroundNormal.ToString() + ":::" + onGround.ToString ());
		return onGround;
	}

	protected void CheckDownwardSlope(Vector3 walkDir)
	{
		RaycastHit hit;
		if (Physics.Raycast (transform.position + walkDir + Vector3.up * .1f, Vector3.down, out hit, 1f)) {
			if (hit.normal != Vector3.up)
				body.MovePosition (transform.position + Vector3.down * hit.distance);
		}
	}

	protected void CheckSlope()
	{
		if (!onGround)
		{
			currentGroundNormal = Vector3.up;
			return;
		}

		RaycastHit hit;
		if (Physics.Raycast (transform.position + Vector3.up * .1f, Vector3.down, out hit, 1f)) {
			currentGroundNormal = hit.normal;
			if (hit.normal != Vector3.up) {
				body.velocity = Quaternion.Euler(hit.normal) * body.velocity;
			}
		}
	}

	public void Zoom()
	{
		float amountToZoom=Camera.main.fieldOfView;

		if (altFireHeld && weaponManager.currentGunIndex == 1) {
			amountToZoom = zoomFloat;
		} else if (!onGround) {
			amountToZoom = normalFloat + (sprintFloat - normalFloat) * (Mathf.Clamp (body.velocity.magnitude / sprintFloat, 0f, 1f));
		} else 
			amountToZoom = normalFloat;


		Camera.main.fieldOfView = Mathf.Lerp (Camera.main.fieldOfView, amountToZoom, fovLerp);
	}

	protected void ChangeWeapon()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
			weaponManager.ChangeWeapon (0);
		else if (Input.GetKeyDown(KeyCode.Alpha2))
			weaponManager.ChangeWeapon (1);
	}

	public void KickCameraUp(float amount)
	{

		if (cameraYrotation + amount < 90f && cameraYrotation + amount > -90f) {
			cameraYrotation += amount;
			Camera.main.transform.rotation *= Quaternion.Euler (Vector3.right * -amount);
		}
	}
}
