using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private float typingSpeed = 20f;
    private float startTypingSpeed;
    private bool doPunctuationCheck = true;
    public bool IsRunning { get; private set; }

    // characters to pause for
    private readonly List<Punctualtion> punctuations = new List<Punctualtion>() {

        new Punctualtion(new HashSet<char>() {'.', '!', '?'}, 0.6f),
        new Punctualtion(new HashSet<char>() {',', ';', ':'}, 0.3f)
    };
    private Coroutine typingCoroutine;

    public void Run(string textToType, TMP_Text textLabel)
    {
        typingCoroutine = StartCoroutine(TypeText(textToType, textLabel));
        startTypingSpeed = typingSpeed;
    }

    public void Stop()
    {
        StopCoroutine(typingCoroutine);
        IsRunning = false;
    }

    private IEnumerator TypeText(string textToType, TMP_Text textLabel)
    {
        // clear text at the beginning
        IsRunning = true;
        textLabel.text = string.Empty;

        float t = 0;
        int charIndex = 0;

        // iterate over every char in the text to type
        while (charIndex < textToType.Length)
        {
            int lastCharIndex = charIndex;

            // increase charIndex over time
            t += Time.deltaTime * typingSpeed;
            charIndex = Mathf.FloorToInt(t);

            // charIndex never bigger than length of textToType
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);

            // for keeping frameRate consistency
            for (int i = lastCharIndex; i < charIndex; i++)
            {
                // check if at final character
                bool isLast = i >= textToType.Length - 1;

                // show labeltext up until current charIndex
                textLabel.text = textToType.Substring(0, i + 1);

                // TODO find better solution for ignoring typewritingEffect on tags
                if (textToType[i] == '<')
                {
                    typingSpeed = 1000;
                    doPunctuationCheck = false;
                }
                else if (textToType[i] == '>')
                {
                    typingSpeed = startTypingSpeed;
                    doPunctuationCheck = true;
                }

                // wait only if current character is specified character, not last character and next character is no specified character
                if (doPunctuationCheck && IsPunctuation(textToType[i], out float waitTime) && !isLast && !IsPunctuation(textToType[i + 1], out _))
                {
                    yield return new WaitForSeconds(waitTime);
                }


            }
            yield return null;
        }

        IsRunning = false;
    }

    private bool IsPunctuation(char character, out float waitTime)
    {

        // check if a given character is listed in one of the defined hashSets
        // if yes, set waitTime to the value listed in that hashSet
        foreach (Punctualtion punctuationCategory in punctuations)
        {
            if (punctuationCategory.Punctuations.Contains(character))
            {
                waitTime = punctuationCategory.WaitTime;
                return true;
            }
        }

        waitTime = default;
        return false;
    }

    private readonly struct Punctualtion
    {
        public readonly HashSet<char> Punctuations;
        public readonly float WaitTime;

        public Punctualtion(HashSet<char> punctuations, float waitTime)
        {
            Punctuations = punctuations;
            WaitTime = waitTime;
        }
    }
}
