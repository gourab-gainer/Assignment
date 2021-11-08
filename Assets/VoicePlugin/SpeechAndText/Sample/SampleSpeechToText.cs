using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TextSpeech;
using  TMPro;
using UnityEngine.Video;

public class SampleSpeechToText : MonoBehaviour
{
//    public GameObject loading;
    public InputField inputLocale;
    public TMP_InputField inputText;
    public float pitch;
    public float rate;

    public TextMeshProUGUI txtLocale;
    public TextMeshProUGUI txtPitch;
    public TextMeshProUGUI txtRate;
    [SerializeField] private VideoPlayer videoPlayer;
    private string[] UserSpeechStatus = new string[2] {"You are correct", "You are wrong"};
    private string correctSpeech = "D";
    void Start()
    {
        Setting("en-US");
      //  loading.SetActive(false);
        SpeechToText.instance.onResultCallback = OnResultSpeech;
        CheckVideoStatus();

    }

    void CheckVideoStatus()
    {
        videoPlayer.Play();
        videoPlayer.loopPointReached += checkOver;
    }

    IEnumerator StartRec()
    {
        StartRecording();
        yield return new WaitForSeconds(5.0f);
        StopRecording();
    }
    private void checkOver(VideoPlayer _videoPlayer)
    {
        long playerCurrentFrame = _videoPlayer.frame;
        long playerFrameCount = Convert.ToInt64(_videoPlayer.frameCount);
     
        if(playerCurrentFrame < playerFrameCount)
        {
            print ("VIDEO IS PLAYING");
            StartCoroutine(StartRec());
        }
        else
        {
            print ("VIDEO IS OVER");
            
            //Do w.e you want to do for when the video is done playing.
       
            //Cancel Invoke since video is no longer playing
           // CancelInvoke("checkOver");
        }
    }
    
    

    public void StartRecording()
    {
#if UNITY_EDITOR
#else
        SpeechToText.instance.StartRecording("Speak any");
#endif
    }

    public void StopRecording()
    {
#if UNITY_EDITOR
        OnResultSpeech("Not support in editor.");
#else
        SpeechToText.instance.StopRecording();
#endif
#if UNITY_IOS
        loading.SetActive(true);
#endif
    }
    void OnResultSpeech(string _data)
    {
        inputText.text = _data;
        if (_data.ToLower() == correctSpeech.ToLower())
        {
            TextToSpeech.instance.StartSpeak(UserSpeechStatus[0]);
        }
        else
        {
            TextToSpeech.instance.StartSpeak(UserSpeechStatus[1]);
        }
#if UNITY_IOS
        loading.SetActive(false);
#endif
    }
    public void OnClickSpeak()
    {
      //  TextToSpeech.instance.StartSpeak(inputText.text);
    }
    public void  OnClickStopSpeak()
    {
      //  TextToSpeech.instance.StopSpeak();
    }
    public void Setting(string code)
    {
        TextToSpeech.instance.Setting(code, pitch, rate);
        SpeechToText.instance.Setting(code);
        txtLocale.text = "Locale: " + code;
        txtPitch.text = "Pitch: " + pitch;
        txtRate.text = "Rate: " + rate;
    }
    public void OnClickApply()
    {
        Setting(inputLocale.text);
    }
}
