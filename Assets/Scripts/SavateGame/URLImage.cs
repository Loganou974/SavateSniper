using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class URLImage : MonoBehaviour
{
    
   public void SetImage(string url)
    {
        StartCoroutine(GetText(url));
    }

    IEnumerator GetText(string url)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                // Get downloaded asset bundle
                var texture = DownloadHandlerTexture.GetContent(uwr);
                GetComponent<RawImage>().texture = texture;
            }
        }

      
    }
}
