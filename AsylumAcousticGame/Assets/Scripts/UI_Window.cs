using UnityEngine;

public class UI_Window : MonoBehaviour
{
    public int ID;

    public virtual void Open()
    {
        Debug.Log($"Open UI : {ID}");
    }
}