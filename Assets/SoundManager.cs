using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour {

    public static SoundManager instanece { get; private set; }
    public AudioSource MainMusic;
    public List<AudioClip> Musiclist;
    public AudioSource SoundEffect;
    public List<AudioClip> SoundEffectList;
    bool changMusic  = false;
    void Awake ()
    {
        if (instanece == null)
            instanece = this;
        else
            Destroy(instanece.gameObject);


        DontDestroyOnLoad(this.gameObject);
	}
    private void Start()
    {
        MainMusic.clip = Musiclist[0];
        MainMusic.Play();
        
    }
    // Update is called once per frame
    void Update ()
    {    
    }

    public  void ChangeMusic(int num)
    {
        int index= num;
        if (num == 3)
            index = 2;
        MainMusic.Stop();
        MainMusic.clip = Musiclist[index];
        MainMusic.Play();
    }
}
