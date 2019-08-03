// Worst name

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplayInImages : MonoBehaviour
{
    [SerializeField]
    Sprite transparentSprite;

    // This one should absolutely be done another, better way, but super hacky like this works for now
    [SerializeField]
    List<char> fontIndexes;

    [SerializeField]
    List<Sprite> fontImages;

    Dictionary<char, Sprite> font;

    [SerializeField]
    List<Image> images;

    public void Awake()
    {
        font = new Dictionary<char, Sprite>();

        for(int i = 0; i < fontIndexes.Count; i++)
        {
            font.Add(fontIndexes[i], fontImages[i]);
        }
    }

    public void DisplayText(string text)
    {
        if(text.Length > images.Count)
        {
            Debug.LogError($"String {text} too long to display on {this}; it is {text.Length} while only {images.Count} space is available.");
        }

        // Ugh. Better than my first idea, at least. Should produce right-aligned stuff.
        for(int i = 0; i < images.Count; i++)
        {
            if(i < text.Length)
            {
                images[i].sprite = font[text[text.Length - i - 1]];
            }
            else
            {
                images[i].sprite = transparentSprite;
            }
        }
    }

    public void SetColor(Color color)
    {
        foreach(Image image in images)
        {
            image.color = color;
        }
    }
}