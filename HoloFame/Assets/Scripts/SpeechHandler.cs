﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

public class SpeechHandler : MonoBehaviour
{
    public string ResetSceneCmd = "reset scene";

    private KeywordRecognizer _keywordRecognizer;

    void Start()
    {
        _keywordRecognizer = new KeywordRecognizer(new[] { ResetSceneCmd });
        _keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        _keywordRecognizer.Start();
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        var cmd = args.text;
        if (cmd == ResetSceneCmd)
        {
            SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
        }
    }

    private void OnDestroy()
    {
        if (_keywordRecognizer != null)
        {
            if (_keywordRecognizer.IsRunning)
            {
                _keywordRecognizer.Stop();
            }
            _keywordRecognizer.Dispose();
        }
    }
}
