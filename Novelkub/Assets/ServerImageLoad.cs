using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class ServerImageLoad : MonoBehaviour
{
    public RawImage img;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TextureLoad());
    }

   IEnumerator TextureLoad()
    {
        string ur1 = "";
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(ur1);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success )
        {
            Debug.LogError(www.error);

        }
        else
        {
            img.texture = ((DownloadHandlerTexture)www.downloadHandler).texture;

        }
    }
}
