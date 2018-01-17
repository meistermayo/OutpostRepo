using UnityEngine;
using System.Collections;

public class LineBullet : Bullet
{
	LineRenderer lineRenderer;
	GameObject bullet;

	protected override void Start ()
	{
		base.Start ();
		lineRenderer = GetComponent<LineRenderer> ();
		lineRenderer.SetPositions (new Vector3[2]);
		lineRenderer.SetPosition (0, transform.position);
	}

	public void SetBullet(GameObject _bullet)
	{
		if (lineRenderer == null)
			lineRenderer = GetComponent<LineRenderer> ();
		if (_bullet != null)
			bullet = _bullet;
	}

	void Update()
	{
		if (lineRenderer != null) {
			lineRenderer.SetPosition (0, transform.position);
			if (bullet != null) {
				lineRenderer.SetPosition (1, bullet.transform.position);
			}else
				lineRenderer.SetPosition (1, transform.position);
		}
	}
}

