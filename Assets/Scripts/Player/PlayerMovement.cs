﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AimMode { Mouse, Joystick }

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {

	private PlayerMain main;
	private Rigidbody rigid;

	public float moveSpeed = 10f;

	private InputData input;
	public float maxGroundCheckDistance = 10f;
	public LayerMask groundCheckMask;

	void Awake() {
		main = GetComponent<PlayerMain>();
		rigid = GetComponent<Rigidbody>();
	}

	void FixedUpdate() {
		InputData input = VirtualControlManager.SampleInput(main.playerNumber);
		Vector3 moveVector = new Vector3(input.Horizontal, 0f, input.Vertical);
		moveVector = Vector3.ClampMagnitude(moveVector, 1f);
		rigid.velocity = moveVector * moveSpeed;
	}

	void Update() {
		RaycastHit hit;
		if(Physics.Raycast(transform.position, Vector3.down, out hit, maxGroundCheckDistance, groundCheckMask)) {
			transform.position = hit.point + Vector3.up;
		}
	}
}
