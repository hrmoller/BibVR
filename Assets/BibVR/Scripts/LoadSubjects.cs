using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Assertions;

namespace BibVR {
  public class LoadSubjects : MonoBehaviour {
    public TextAsset EmneOrdsJson;
    public bool deletePlayerPrefsAtStartup;

    [SerializeField]
    private Dictionary<string, Emneord> subjectsDict = new Dictionary<string, Emneord>();

    private string storedJson = null;

    void Awake() {
      Assert.IsNotNull(EmneOrdsJson);

      if(deletePlayerPrefsAtStartup) {
        PlayerPrefs.DeleteAll();
      }

      string playerPrefs_bibvr_json = PlayerPrefs.GetString("bibvr_json");
      if(playerPrefs_bibvr_json.Length > 1) {
        storedJson = playerPrefs_bibvr_json;
      }
    }

    void Start() {
      Debug.Log("Start");
      if(storedJson != null) {
        Debug.Log("Loading stored JSON");
        JSONObject result = new JSONObject(storedJson);
        parseStoredJSON(result);
      } else {
        StartCoroutine(LoadData());
      }

      Debug.Log("done - start");
    }

    void parseStoredJSON(JSONObject json) {
      for(int i = 0; i < json.list.Count; i++) {
        string key = json.keys[i];
        JSONObject j = json.list[i];

        int count = -1;
        int width = -1;

        j.GetField(ref count, "Count");
        j.GetField(ref width, "Width");

        string[] co = parseArray(j.GetField("Co"));

        Emneord emneord = new Emneord(key, count, width, co);
        subjectsDict.Add(key, emneord);
      }

      Debug.Log("SubjectsDict: " + subjectsDict.Count);
    }

    IEnumerator LoadData() {
      Debug.Log("LoadData");
      string data = EmneOrdsJson.text;
      data = data.Replace("\\u00f8", "ø");
      data = data.Replace("\\u00e6", "æ");

      object lockHandle = new System.Object();
      bool done = false;
      JSONObject result = null;

      var myThread = new System.Threading.Thread(() => {
        Debug.Log("MyThread");
        result = new JSONObject(data);
        lock (lockHandle) {
          done = true;
        }
      });

      myThread.Start();

      Debug.Log(myThread.IsAlive);
      while(true) {
        Debug.Log("while");
        Debug.Log(done);
        yield return null;
        lock (lockHandle) {
          if(done) {
            break;
          }
        }
      }

      Debug.Log("Done");
//      Debug.Log(result.type);
      parseJSON(result);
      result = null;
      storeDictionary();
      myThread.Abort();
    }

    void parseJSON(JSONObject json) {
      for(int i = 0; i < json.list.Count; i++) {
        string key = json.keys[i];
        JSONObject j = json.list[i];

        int count = -1;
        int width = -1;

        j.GetField(ref count, "count");
        j.GetField(ref width, "width");

        string[] co = parseArray(j.GetField("co"));
//        Debug.Log(string.Join(", ", co));

        Emneord emneord = new Emneord(key, count, width, co);
        subjectsDict.Add(key, emneord);
      }

      json = null;
      Debug.Log("SubjectsDict: " + subjectsDict.Count);
    }

    private string[] parseArray(JSONObject list) {
      ArrayList subjects = new ArrayList();

      foreach (JSONObject l in list.list) {
        subjects.Add(l.ToString());
      }

      return (string[])subjects.ToArray(typeof(string));
    }

    public void storeDictionary() {
      string jsonString = JsonConvert.SerializeObject(subjectsDict);

      PlayerPrefs.SetString("bibvr_json", jsonString);
      PlayerPrefs.Save();
    }

    [ObsoleteAttribute("Method is deprecated. It is only kept for reference and is soon to be removed. Use parseJSON instead.")] void accessData(JSONObject obj) {
      Debug.Log(obj.type);
      switch(obj.type) {
        case JSONObject.Type.OBJECT:
          for(int i = 0; i < obj.list.Count; i++) {
            string key = (string)obj.keys[i];
            JSONObject j = (JSONObject)obj.list[i];

            int count = -1;
            int width = -1;

            j.GetField(ref count, "count");
            j.GetField(ref width, "width");

            string[] arr = parseArray(j.GetField("co"));
            Debug.Log(string.Join(" ", arr));

            //            Emneord hest = new Emneord(key, count, width);
            //            Debug.Log(hest);
          }
          break;
        case JSONObject.Type.ARRAY:
          Debug.Log(obj);
          /*foreach(JSONObject j in obj.list){
            accessData(j);
          }*/
          break;
        case JSONObject.Type.STRING:
          Debug.Log(obj.str);
          break;
        case JSONObject.Type.NUMBER:
          Debug.Log(obj.n);
          break;
        case JSONObject.Type.BOOL:
          Debug.Log(obj.b);
          break;
        case JSONObject.Type.NULL:
          Debug.Log("NULL");
          break;

      }
    }
  }
}