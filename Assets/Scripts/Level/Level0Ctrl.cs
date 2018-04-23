using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level0Ctrl : MonoBehaviour
{
	 string text = "王子进入巨石森林已经有{0}天了，我静静坐在森林的入口，森林里没有一丝声音传出来。这种静默的等待渐渐让我意识到，他是真的走丢了，我要去找他。";
	
	public TypeWriter typeWriter;

	public NumberCount numberCount;

	public bool isFinished;
	public RawImage bg;
	[Range(0,1f)]
	public float delay = 0.5f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if ( !isFinished && numberCount.currNum >= numberCount.maxNum)
		{
		
			StartCoroutine(PlayText());
			isFinished = true;
			bg.raycastTarget = true;
		}
	}

	IEnumerator PlayText()
	{
		yield return new WaitForSeconds(delay);
		var tx = string.Format(text, numberCount.maxNum.ToString());
		typeWriter.Write(tx);
	}

	private void OnDestroy()
	{
		StopAllCoroutines();
	}

public	void OnClickBg()
	{
		SceneManager.LoadScene(1);
	}
}
