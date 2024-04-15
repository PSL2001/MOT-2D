using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Events;

public class UnlockManager : MonoBehaviour
{
	[SerializeField] float UnlockScore;
	[SerializeField] public GameObject noCoins;
	[SerializeField] public GameObject Coins;

	public UnityEvent<float> onUnlockScore;
	public UnityEvent onUnlock;


	public void CheckUnlockCondition()
	{
		if (ScoreManager.Instance.score.CurrentValue >= UnlockScore)
		{
			onUnlockScore.Invoke(UnlockScore);
			onUnlock.Invoke();
			StartCoroutine(UICoins());
		}
		else
		{
			StartCoroutine(UINoCoins());
		}
	}

	private IEnumerator UINoCoins()
	{
		noCoins.SetActive(true);
		yield return new WaitForSecondsRealtime(1.0f);
		noCoins.SetActive(false);	
	}

	private IEnumerator UICoins()
	{
		Coins.SetActive(true);
		yield return new WaitForSecondsRealtime(1.0f);
		Coins.SetActive(false);
	}
}
