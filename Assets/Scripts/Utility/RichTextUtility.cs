using System.Collections;
using UnityEngine;


public static class RichTextUtility
{
    public static string FillColor(this bool self)
    {
        string text = "";
        if(!self)
        {
            text = $"<color=red>{self}</color>";
        }
        else
        {
            text = $"<color=green>{self}</color>";
        }
        return text;
    }
    public static string ParseDir(this bool self)
    {
        string text = "";
        if (!self)
        {
            text = $"->";
        }
        else
        {
            text = $"<-";
        }
        return text;
    }
}
