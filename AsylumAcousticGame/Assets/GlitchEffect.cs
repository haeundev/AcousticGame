using System.Collections;
using TMPro;
using UnityEngine;

public class GlitchEffect : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float glitchChance = 0.05f;
    public float glitchDuration = 0.1f;

    private string _originalText;

    private void Start()
    {
        if (textComponent == null) textComponent = GetComponent<TextMeshProUGUI>();
        _originalText = textComponent.text;
        StartCoroutine(GlitchText());
    }

    private IEnumerator GlitchText()
    {
        while (true)
        {
            var glitchedText = _originalText;
            for (var i = 0; i < glitchedText.Length; i++)
                if (Random.value < glitchChance)
                {
                    var randomChar = (char)('a' + Random.Range(0, 26));
                    glitchedText = glitchedText.Substring(0, i) + randomChar + glitchedText.Substring(i + 1);
                }

            textComponent.text = glitchedText;
            yield return new WaitForSeconds(glitchDuration);
            textComponent.text = _originalText;
            yield return new WaitForSeconds(Random.Range(0.05f, 0.2f));
        }
    }
}