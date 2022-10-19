using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	public float speed = 10f;
	private Rigidbody2D rb;
	private bool inAir;
	private Vector3 initialPosition;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		initialPosition = transform.position;
	}
	// Update is called once per frame
	void Update()
	{
		if (Input.GetButton("Horizontal")) MoveHorizontal();
		if (Input.GetKey(KeyCode.Space) && !inAir) Jump();
		CheckOutOfMap();
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
			inAir = false;
	}
	private void MoveHorizontal()
	{
		Vector3 horizontalMovement = transform.right * Input.GetAxis("Horizontal");
		transform.position =
			Vector3.MoveTowards(transform.position, transform.position + horizontalMovement, speed * Time.deltaTime);
	}

	private void Jump()
	{
		inAir = true;
		Vector2 force = new Vector2(10, 10);
		rb.AddForce(transform.up * force, ForceMode2D.Impulse);
	}

	private void CheckOutOfMap()
	{
		if (Math.Abs(transform.position.x) > 9 || transform.position.y < -4)
		{
			transform.position = initialPosition;
		} 
	}

}
