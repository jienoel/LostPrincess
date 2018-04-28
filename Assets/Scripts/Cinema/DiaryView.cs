using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class DiaryView : MonoBehaviour
{

    public RawImage textBg;
    public InputField text;
    public bool enableTypeWriter;
    public TypeWriter typeWriter;
    private DiaryItem _diaryItem;
    public void ShowDialog(DiaryItem _diary, bool enableTypeWriter = false)
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);
        if (this.enableTypeWriter)
        {
            typeWriter.Write(_diary.text);
        }
        else
        {
            this.text.text = _diary.text;
        }

        _diaryItem = _diary;
    }

    public void OnClickBg()
    {
        if(typeWriter.isTyping)
            return;
        gameObject.SetActive(false);
       
        Messenger.Broadcast(MessageName.MN_Diary_Read, false);
        Messenger.Broadcast(MessageName.MN_Trigger_OS,_diaryItem.osID);
    }
}
