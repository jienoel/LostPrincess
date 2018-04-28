using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILit
{
	float radius { get; }
	bool isValid { get; }
	int id { get; }
	bool CheckValid();
	Vector3 position { get; }
}

public class BoneFire : MonoBehaviour , ILit
{
	public static int num = 0;

	[SerializeField] private int m_id;
	public int id
	{
		get { return m_id; }
	}

	[SerializeField]
	float m_radius;
	public float radius
	{
		get { return m_radius; }
	}

	[SerializeField]
	private bool m_isValid;
	public bool isValid {
		get { return m_isValid; }
	}

	public Transform CenterTransform;

	public Vector3 position
	{
		get { return CenterTransform.position; }
	}

	private void Awake()
	{
		m_id = num++;
	}

	// Use this for initialization
	void Start ()
	{
		m_isValid = true;
		Messenger.Broadcast(MessageName.MN_CHARACTOR_BORN, (ILit)this);
	}

	public bool CheckValid()
	{
		return m_isValid;
	}
}
