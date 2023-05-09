using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
	public float axisX;
	public float axisY;
	public Transform focus;
	float xRotate;
	float yRotate;

	private void Start()
	{
		// this allows me to lock cursor to a fixed point and hides it so it doesn't show on the screen while the game runs
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	private void Update()
	{
		//this is mouse feedback
		float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * axisX;
		float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * axisY;
		
		// this allows me to clamp the camera angle to 90 so there's a limit to how much i can move the camera
		yRotate += mouseX;
		xRotate -= mouseY;
		xRotate = Mathf.Clamp(xRotate, -90f, 90f);

		// allows me to rotate camera in any direction through the X and Y axis with the use of Quaternion
		transform.rotation = Quaternion.Euler(xRotate, yRotate, 0);
		focus.rotation = Quaternion.Euler(0, yRotate, 0);

	}
}
