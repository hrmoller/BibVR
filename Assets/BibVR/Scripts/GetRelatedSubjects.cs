/**
 *
 */

using System.Collections;
using SimpleJSON;
using UnityEngine;

public class GetRelatedSubjects : MonoBehaviour {

  [Header("Web Service settings")]
  public string EmnerToEmnerWSURL;

  IEnumerator Start() {
    string URL = EmnerToEmnerWSURL + "integration";
    WWW www = new WWW(URL);
    yield return www;
    Debug.Log("DONE!");
    JSONNode parsedJson = JSONNode.Parse(www.text);

    if(parsedJson["emneord"].Count >= 1){
      parseEmneord(parsedJson["emneord"].AsArray);
    } else {
      Debug.Log("<color=red>No emneord was found.</color>");
    }
  }

  void parseEmneord(JSONArray emeneordArr) {
    for(int i = 0; i < emeneordArr.Count; i++){
      string emneord = emeneordArr[i][0];
      Debug.Log(emneord.Substring(5));
    }
  }
}
