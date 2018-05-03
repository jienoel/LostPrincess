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
	public float force = 10f;
	public LaunchArcMesh hint;
	public GameObject mousePosObj;
	public LineRenderer lineRenderer;
	public GameObject bulletPrefab;
	public float centerY = 0.5f;
	public float speed = 1;
	public Vector3 gravity;
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
		Messenger.AddListener<Transform>(MessageName.MN_Mouse_Down, OnMouseDown);
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
				var currDir = CalculateDirection(currPoint, clickPos);
				DisplayLine(true, currDir);
				Debug.DrawLine(clickPos,currPoint);
				lineRenderer.SetPosition(0,clickPos);
				lineRenderer.SetPosition(1,currPoint);
				mousePosObj.transform.position = currPoint;

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
							var bullet = Instantiate(bulletPrefab, clickPos, Quaternion.identity,transform);
							Parabola parabola = bullet.GetComponent<Parabola>();
							parabola.Ta = transform.InverseTransformPoint(clickPos);
							parabola.Tb = transform.InverseTransformPoint(currPoint);
							parabola.relative = transform;
							//parabola.Tb.y = clickPos.y;
							Debug.Log(string.Format("Ta:{0} , Tb:{1}, direction:{2}",parabola.Ta.ToString(), parabola.Tb.ToString(), (parabola.Tb - parabola.Ta).ToString()));
							DisplayLine(false, currDir);
                       
						}
					}
					isClicked = false;
					clickTime = Time.time;
					//animator.SetBool(AnimatorParam.BoolAttackPre, false);
					transform.localRotation = Quaternion.identity;
				}
			}
			else
			{
				CheckInput();
			}
			
			ChangeAnimation();
		}
	}
	
	private void DisplayLine(bool canDisplay, Vector3 dir)
	{
		if (canDisplay)
		{
			//hint.gameObject.SetActive(true);
			//hint.Show();
			dir.y = 360 - Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg -90 ;
			dir.x = 0;
			dir.z = 0;
			//hint.transform.localEulerAngles = dir;
		}
		else
		{
			//hint.gameObject.SetActive(false);
		}
	}
	
	private Vector3 CalculateDirection(Vector3 endPos, Vector3 startPos)
	{
		startPos = mousePosObj.transform.InverseTransformPoint(startPos);
		endPos = mousePosObj.transform.InverseTransformPoint(endPos);
		startPos.y = 0;
		endPos.y = 0;
		return -endPos + startPos;
	}
	
	private Vector3 GetGroundPoint()
	{
		RaycastHit hit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000, groundLayer))
		{
			return hit.point;
		}
		return Vector3.down;
	}

	private void OnMouseDown(Transform trans)
	{
		isClicked = true;
		Debug.Log("On Mouse Down " + Time.time.ToString() + "  "+trans.name); 
		StopMove();
		_princessAnimController.SetAttack();
		clickTime = Time.time;
		clickPos = transform.position;
		clickPos.y += centerY;
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
	
		if( Input.GetMouseButtonDown(0))
		{
			if (!CloseInnerOS())
			{
				Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
				RaycastHit hit;
				if( Physics.Raycast( ray, out hit, 1000 , groundLayer) )
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
		Messenger.RemoveListener<Transform>(MessageName.MN_Mouse_Down, OnMouseDown);
	}
}
