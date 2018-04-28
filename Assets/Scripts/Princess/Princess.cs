using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Princess:MonoBehaviour
{
	public enum status 
	{
		attack,
		walk,
		showInnerOs
	}
	private NavMeshAgent _navMeshAgent;
	private PrincessAnimController _princessAnimController;
	private bool walking;
	private DialogBubble _dialogBubble;
	private bool isClicked = false;
	private float clickTime = 0f;
	private Vector3 clickPos = Vector3.zero;
	public LayerMask groundLayer;
	public SpriteFlipCtrl flipCtrl;
	public float thresholdTime = 0.1f;
	public float thresholdDistance = 0.01f;
	public bool isAttcking;
	[Header("Attack")]
	public float maxDistance = 10f;
	// 最大手指/鼠标拖动范围
	public float maxDegree = 45;
	void Awake()
	{
		_navMeshAgent = GetComponent<NavMeshAgent>();
		_princessAnimController = GetComponent<PrincessAnimController>();
		_dialogBubble = GetComponent<DialogBubble>();
	}

	// Use this for initialization
	void Start()
	{
		Messenger.AddListener<int>(MessageName.MN_Trigger_OS,ShowInnerOS);
	}

	// Update is called once per frame
	void Update()
	{
		if (Game.Instance.isReadDiary)
		{
			_navMeshAgent.isStopped = true;

			StopMove();
		}
		else if (Game.Instance.operateType == Game.OperateType.ClickToMove)
		{
			if (isClicked)
			{
				var currPoint = GetGroundPoint();
				if (clickPos != Vector3.down && currPoint != Vector3.down)
				{
					flipCtrl.Flip(clickPos , currPoint);
				}
				//var currDir = CalculateDirection(currPoint - clickPos);
				//DisplayLine(true, currDir);
				if (Input.GetMouseButtonUp(0))
				{
					Debug.Log(" Mouse Button Up " + Time.time.ToString() + "  " + clickTime.ToString());
					var releaseTime = Time.time;
					if (releaseTime - clickTime < thresholdTime)
					{
					}
					else
					{
						if (clickPos != Vector3.down && currPoint != Vector3.down)
						{
                       
							isAttcking = true;
							Debug.Log("set animator bool attack true");
							//animator.SetTrigger(AnimatorParam.triggerAttack);
							//var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
							//bullet.GetComponent<Rigidbody>().AddForce(currDir * force, ForceMode.Impulse);
							//DisplayLine(false, currDir);
                       
						}
					}
					isClicked = false;
					clickTime = Time.time;
					//animator.SetBool(AnimatorParam.BoolAttackPre, false);
					transform.rotation = Quaternion.identity;
				}
			}
			else
			{
				CheckInput();
			}
			
			ChangeAnimation();
		}
	}
	
	
	private Vector3 CalculateDirection(Vector3 dir)
	{
		var direction = -dir.normalized;
		var magnitude = Mathf.Clamp(dir.magnitude, 0, maxDistance);
		var anotherVec = Mathf.Lerp(0, Mathf.Tan(Mathf.Deg2Rad * maxDegree), magnitude / maxDistance) * Vector3.up;
		return (direction + anotherVec).normalized;
	}
	
	private Vector3 GetGroundPoint()
	{
		RaycastHit hit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, groundLayer))
		{
			return hit.point;
		}
		return Vector3.down;
	}

	private void OnMouseDown()
	{
		isClicked = true;
		Debug.Log("On Mouse Down " + Time.time.ToString()); 
		isClicked = true;
		StopMove();
		clickTime = Time.time;
		clickPos = GetGroundPoint();
	}

	void StopMove()
	{
		walking = false;
		ChangeAnimation();
	}

	void ShowInnerOS(int id)
	{
		InnerOSItem osItem = Game.Instance.gameModel.levelConfig.GetInnerOsItem(id);
		if (osItem == null)
		{
			Debug.LogError("No InnerOS");
			return;
		}
		_dialogBubble.vBubble[0].vMessage = osItem.text ;
		_dialogBubble.ShowBubble(_dialogBubble);
	}

	bool CloseInnerOS()
	{
		return  _dialogBubble.CloseBubble(_dialogBubble); 
	}

	void CheckInput()
	{
	
		if( Input.GetMouseButtonDown(0) )
		{
			if (!CloseInnerOS())
			{
				Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
				RaycastHit hit;
				if( Physics.Raycast( ray, out hit, 1000 ) )
				{
					Debug.Log( "Fire1 " +hit.point.ToString());
					_navMeshAgent.SetDestination( hit.point );
					_navMeshAgent.isStopped = false;
					walking = true;
				}
			}
		}

		if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance) {
			if (!_navMeshAgent.hasPath || Mathf.Abs (_navMeshAgent.velocity.sqrMagnitude) < float.Epsilon)
				walking = false;
		} else {
			walking = true;
		}
	}

	void ChangeAnimation()
	{
		if (walking)
		{
			_princessAnimController.SetRun();
		}
		else
		{
			_princessAnimController.SetWait();
		}
	}

	private void OnDestroy()
	{
		Messenger.RemoveListener<int>(MessageName.MN_Trigger_OS,ShowInnerOS);
	}
}
