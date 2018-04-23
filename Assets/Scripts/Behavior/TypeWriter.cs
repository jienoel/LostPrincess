using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriter : MonoBehaviour
{

	public string story;

	public InputField txt;
	[Range(0,0.5f)]
	public  float delay= 0.05f;

	public bool isTyping;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ClearText()
	{
		txt.text = "";
	}

	public void Write(string text)
	{
		if (isTyping)
		{
			txt.text = story;
			StopAllCoroutines();
			isTyping = false;
		}

		story = text;
		StartCoroutine(PlayText());
	}
	IEnumerator PlayText()
	{
		foreach (char c in story) 
		{
			txt.text += c;
			yield return new WaitForSeconds (delay);
		}

		isTyping = false;
	}

	private void OnDestroy()
	{
		StopAllCoroutines();
	}
}
