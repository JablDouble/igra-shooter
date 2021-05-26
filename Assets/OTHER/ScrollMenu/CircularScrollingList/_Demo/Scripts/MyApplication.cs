using UnityEngine;
using UnityEngine.UI;

public class MyApplication : MonoBehaviour
{
    public ListPositionCtrl list;
    public Text[] ListOfButtons;

    public void Update()
    {
        int contentID = list.GetCenteredContentID();
        string centeredContent = list.listBank.GetListContent(contentID);

        foreach (Text button in ListOfButtons)
        {
            if (button.text == centeredContent && button.text != "Continue")
            {
                button.CrossFadeAlpha(1, 0, false);
            } else
            {
                if (button.text == "Continue")
                {
                    button.CrossFadeAlpha(0.2f, 0, false);
                } else
                {
                    button.CrossFadeAlpha(0.5f, 0, false);
                }
            }
        }
    }
}

