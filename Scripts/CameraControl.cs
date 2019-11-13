using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
	void Start () {
		
	}
	
	void Update () {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        scroll *= 100;

        Vector3 movement = new Vector3(horizontal, -scroll, vertical);

        movement = movement * 150;

        transform.position += movement * Time.deltaTime;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -700, 700), Mathf.Clamp(transform.position.y, 50, 700), Mathf.Clamp(transform.position.z, -700, 700));

    }
}
