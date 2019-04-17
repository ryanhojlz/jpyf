using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    //private class AudioFile
    //{
    //    string name;
    //    AudioClip audio;

    //    public AudioFile(string _name, AudioClip _audio)
    //    {
    //        name = _name;
    //        audio = _audio;
    //    }

    //    public void setName(string _name) { name = _name; }
    //    public void setAudio(AudioClip _audio) { audio = _audio; }

    //    public string GetName() { return name; }
    //    public AudioClip GetAudio() { return audio; }
        
    //}

    //[SerializeField]
    public AudioClip TNK_attack;
    public AudioClip NRKB_attack;
    public AudioClip TNG_attack;
    //List<AudioFile> AudioList;

    //// Use this for initialization
    //void Start()
    //{
    //    AudioList.Add(new AudioFile("tnk_attack", TNK_attack));
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    //AudioClip FindAudioByName(string name)
    //{
    //    AudioClip ToReturn = null;

    //    for (int i = 0; i < AudioList.Count; ++i)
    //    {
    //        if (AudioList[i].GetName() == name)
    //        {
    //            ToReturn = AudioList[i].GetAudio();
    //            break;
    //        }
    //    }

    //    return ToReturn;
    //}
}
