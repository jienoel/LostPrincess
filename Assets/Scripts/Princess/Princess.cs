﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Princess:MonoBehaviour
{
	private NavMeshAgent _navMeshAgent;
	private bool walking;
	void Awake()
	{
		_navMeshAgent = GetComponent<NavMeshAgent>();

	}

	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		CheckInput();
	}

	void CheckInput()
	{
		Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
		RaycastHit hit;
		if( Input.GetButtonDown( "Fire1" ) )
		{
			if( Physics.Raycast( ray, out hit, 1000 ) )
			{
				Debug.Log( "Fire1 " +hit.point);
				_navMeshAgent.SetDestination( hit.point );
				_navMeshAgent.isStopped = false;
				walking = true;
			}
		}

		if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance) {
			if (!_navMeshAgent.hasPath || Mathf.Abs (_navMeshAgent.velocity.sqrMagnitude) < float.Epsilon)
				walking = false;
		} else {
			walking = true;
		}
	}
}