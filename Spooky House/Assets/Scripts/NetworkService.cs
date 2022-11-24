using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class NetworkService
{
  private const string xmlApi = "https://api.openweathermap.org/data/3.0/onecall?lat=33.44&lon=-94.04&exclude=hourly,daily&appid=78b76430208b7d409fdf2845b8d082b3";
  private const string webImage = "http://upload.wikimedia.org/wikipedia/commons/c/c5/Moraine_Lake_17092005.jpg";

  private IEnumerator CallAPI(string url, Action<string> callback)
  {
    // the using statement makes sure that after the code gets executed the "request" object is deleted from memory
    using (UnityWebRequest request = UnityWebRequest.Get(url))
    {
      yield return request.Send();

      if (request.isNetworkError)
      {
        Debug.LogError("network problem: " + request.error);
      } else if (request.responseCode != (long)System.Net.HttpStatusCode.OK)
      {
        Debug.LogError("response error: " + request.responseCode);
      } else
      {
        callback(request.downloadHandler.text);
      }
    }
  }

  public IEnumerator DownloadImage(Action<Texture2D> callback)
  {
    UnityWebRequest request = UnityWebRequestTexture.GetTexture(webImage);
    yield return request.Send();
    callback(DownloadHandlerTexture.GetContent(request));
  }


  public IEnumerator GetWeatherXML(Action<string> callback)
  {
    return CallAPI(xmlApi, callback);
  }
}
