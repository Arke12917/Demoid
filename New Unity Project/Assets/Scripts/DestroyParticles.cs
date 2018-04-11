using UnityEngine;
using System.Collections;

public class DestroyParticles : MonoBehaviour
{

	public ParticleSystem.EmissionModule PSS;
	public ParticleSystem PS;
	public void Awake(){
		PSS = PS.emission;
	}
	public void OnEnable(){
		StartCoroutine (Start ());
	}
	private IEnumerator Start()
	{
		PS.Play();
		PSS.enabled=true;
		yield return new WaitForSeconds(0.5f);
		PS.Stop();
		PSS.enabled=false;
		gameObject.SetActive(false); 
	}

}