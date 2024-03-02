using UnityEngine;
using UnityEngine.UI;

public class ButtonCanvasController : MonoBehaviour
{
    public ScrollRect scrollRectToShow;
    public ScrollRect[] scrollRectsToHide;

    public void ShowAssignedScrollRect()
    {
        if (scrollRectToShow != null)
        {
            scrollRectToShow.gameObject.SetActive(true);
        }

        foreach (ScrollRect scrollRect in scrollRectsToHide)
        {
            if (scrollRect != null)
            {
                scrollRect.gameObject.SetActive(false);
            }
        }
    }
}
