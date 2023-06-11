using System.Collections;
using UnityEngine;

public class ColorScreenFadeInCamera : MonoBehaviour
{
    
    [Tooltip("How fast should the texture be faded out?")]
    public float fadeTime = 3;

    [Tooltip("How long will the screen stay black?")]
    public float blackScreenDuration = 2;

    [Tooltip("Choose the color, which will fill the screen.")]
    public Color fadeColor = Color.black;

    private float _alpha = 1.0f;
    private Texture2D _texture;

    private float _passedBlackScreenTime;

    private void OnEnable()
    {
        _texture = new Texture2D(1, 1);
        _texture.SetPixel(0, 0, new Color(fadeColor.r, fadeColor.g, fadeColor.b, _alpha));
        _texture.Apply();

        StartCoroutine(WaitAndOff());
    }


    public void OnGUI()
    {
        // If the texture is no more visible, we are done.
        if (_alpha <= 0.0f)
        {
            return;
        }

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), _texture);

        if (_passedBlackScreenTime < blackScreenDuration)
        {
            _passedBlackScreenTime += Time.deltaTime;
            return;
        }

        CalculateTexture();
    }

    private void CalculateTexture()
    {
        _alpha -= Time.deltaTime / fadeTime;
        _texture.SetPixel(0, 0, new Color(fadeColor.r, fadeColor.g, fadeColor.b, _alpha));
        _texture.Apply();
    }
    
    private IEnumerator WaitAndOff()
    {
        yield return new WaitForSeconds(fadeTime + blackScreenDuration);
        Destroy(this);
    }
}