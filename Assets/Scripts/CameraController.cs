using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public FixedTouchField CameraJoystick;
    public Transform player;
    protected float CameraAngle;
	protected float CameraAngleSpeed = 0.05f;
	protected bool RotateAroundPlayer = true;
	public float heightOfCamera;

    void FixedUpdate() {
        MoveCamera();
    }

    public void MoveCamera() {
		CameraAngle += CameraJoystick.TouchDist.x * CameraAngleSpeed;

		Camera.main.transform.position = player.position + Quaternion.AngleAxis(CameraAngle, Vector3.up) * new Vector3(0, heightOfCamera, -4);
		Camera.main.transform.rotation = Quaternion.LookRotation(player.position + Vector3.up * 2f - Camera.main.transform.position, Vector3.up);
		
		var CharacterRotation = Camera.main.transform.rotation;
		CharacterRotation.x = 0;
		CharacterRotation.z = 0;
		player.rotation = CharacterRotation;
	}

}
