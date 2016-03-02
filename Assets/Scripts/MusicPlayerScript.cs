using UnityEngine;
using System.Collections;

public class MusicPlayerScript : MonoBehaviour {

	//Stores the AudioClip we want to play
	AudioSource myAudioSource;


	// Use this for initialization
	void Start () {
		//Load the AudioSource...
		myAudioSource = GetComponent<AudioSource> ();

		//...and make sure it doesn't start right away
		myAudioSource.playOnAwake = false;
	}
	
	// Update is called once per frame
	void Update () {
		//When you press P, start the song
		if (Input.GetKeyDown (KeyCode.P)) {
			myAudioSource.Play ();
		}
		//And when you release P, pause the song
		if (Input.GetKeyUp (KeyCode.P)) {
			myAudioSource.Pause ();
		}
	}
}
