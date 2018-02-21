using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    private static AudioManager audioManager;

    private void Awake() {
        DontDestroyOnLoad(this);

        if (audioManager == null) {
            audioManager = this;
        } else {
            DestroyObject(gameObject);
        }
    }
}
