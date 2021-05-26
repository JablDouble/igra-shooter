using UnityEngine;
using UnityEngine.UI;

public class MyApplication : MonoBehaviour
{
    public ListPositionCtrl list;
    public Text[] ListOfButtons;
    public Image[] ListOfImages;

    public void Update()
    {
        int contentID = list.GetCenteredContentID();
        string centeredContent = list.listBank.GetListContent(contentID);


        int i = 0;
        foreach (Text button in ListOfButtons)
        {
            if (button.text == centeredContent && button.text != "Continue")
            {
                button.CrossFadeAlpha(0.9f, 0, false);
                ListOfImages[i].CrossFadeAlpha(0.9f, 0, false);
            } else
            {
                if (button.text == "Continue")
                {
                    button.CrossFadeAlpha(0.2f, 0, false);
                    ListOfImages[i].CrossFadeAlpha(0.2f, 0, false);
                } else
                {
                    button.CrossFadeAlpha(0.4f, 0, false);
                    ListOfImages[i].CrossFadeAlpha(0.4f, 0, false);
                }
            }

            i++;
        }
    }
}

