using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class NumberCount : MonoBehaviour
{

	public Sprite[] numbers;

	public Image[] images;
	
	public int maxNum = 368;
	public int currNum;

	[Range(1,100)]
	public int firstPart = 10;
	[Range(0.01f,1)]
	public float firstCD = 0.5f;
	[Range(0.01f,1)]
	public float secondCD = 1f;
	[Range(1,100)]
	public int lastPart = 5;
	[Range(0.01f,1)]
	public float lastCD = 0.5f;

	public float currCD = 0;
	public int currPart = 0;
	public float cd;

	[Range(1,100)]
	public float speed = 3;

	[Range(20,100)]
	public float speedFactor = 30;
	private void Update()
	{
		if (currNum < maxNum)
		{
			cd += Time.deltaTime;
			if (cd >= currCD)
			{
				currNum++;
				cd = 0;
				DoCount();
			}
		}
	}

	void DoCount()
	{
		var index = currNum % 10;
		images[0].overrideSprite = numbers[index];
		if (!images[0].enabled)
			images[0].enabled = true;
		SetSecondNum(currNum);
		SetThirdNum(currNum);
		if (currNum < firstPart)
		{
			currPart = 1;
			currCD = firstCD;
		}
		else if (maxNum - currNum > lastPart)
		{
			currPart = 2;
			speed = Mathf.Lerp(0, 100, Time.deltaTime * speedFactor);
			currCD = Mathf.Lerp(firstCD, secondCD, Time.deltaTime * speed);
		}
		else
		{
			currPart = 3;
			currCD = Mathf.Lerp(secondCD,lastCD, Time.deltaTime * speed);
		}
	}
	
	void SetSecondNum(int num)
	{
		bool isActive = num > 9;
		var	index =( num % 100) / 10;
		if (isActive != images[1].enabled)
		{
			images[1].enabled = isActive;
		}
		if (isActive)
		{
			images[1].overrideSprite = numbers[index];
		}
	}

	void SetThirdNum(int num)
	{
		var index = num % 1000 / 100;
		bool isActive = num >= 100;
		if (isActive != images[2].enabled)
		{
			images[2].enabled = isActive;
		}
		if (isActive)
		{
			images[2].overrideSprite = numbers[index];
		}
	}

	void CalculateLerp()
	{
		
	}
}
