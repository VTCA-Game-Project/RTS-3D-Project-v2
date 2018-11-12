using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

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
        if (EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                SoundEffect.clip = SoundEffectList[4];
                SoundEffect.Play();
               
            }
        }
    }
    public void PlayEffect(int num)
    {
        SoundEffect.clip = SoundEffectList[num];
        SoundEffect.Play();
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
