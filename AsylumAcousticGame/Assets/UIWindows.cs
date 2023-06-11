using System.Linq;
using UnityEngine;

public class UIWindows : MonoBehaviour
{
    public static UIWindows Instance;

    private void Awake()
    {
        Instance = this;
    }

    public static UI_Window GetWindow(int id)
    {
        var windowComps = Instance.gameObject.GetComponentsInChildren<UI_Window>();
        return windowComps.FirstOrDefault(windowComp => windowComp.ID == id);
    }
}