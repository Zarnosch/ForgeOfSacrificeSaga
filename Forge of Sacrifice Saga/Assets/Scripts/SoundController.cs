using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {
    public AudioSource Firstblood;
    public AudioSource BiteDust;
    public AudioSource FreshMeat;
    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayFirstBlood()
    {
        Firstblood.Play();
    }
    public void PlayBiteDust()
    {
        BiteDust.Play();
    }
    public void PlayFreshMeat()
    {
        FreshMeat.Play();
    }
}
