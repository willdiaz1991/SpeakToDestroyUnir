using System;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceCommandManager : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private string[] keywords = { "izquierda", "derecha", "salta", "detener" };

    public static event Action<string> OnCommandRecognized;

    void Start()
    {
        keywordRecognizer = new KeywordRecognizer(keywords);
        keywordRecognizer.OnPhraseRecognized += OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log("Comando reconocido: " + args.text);
        OnCommandRecognized?.Invoke(args.text.ToLower());
    }

    void OnDestroy()
    {
        if (keywordRecognizer != null && keywordRecognizer.IsRunning)
        {
            keywordRecognizer.Stop();
            keywordRecognizer.Dispose();
        }
    }
}
