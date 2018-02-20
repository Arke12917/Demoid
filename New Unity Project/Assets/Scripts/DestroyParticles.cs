using UnityEngine;
using System.Collections;

public class DestroyParticles : MonoBehaviour
{

	private IEnumerator Start()
	{
		yield return new WaitForSeconds(0.5f);
		Destroy(gameObject); 
	}

}