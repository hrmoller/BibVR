using UnityEngine;

namespace BibVR {
  public class Emneord {
    public string DisplayName;
    public string Key;
    public int Count;
    public int Width;
    public string[] Co;

    public Emneord(string key, int count, int width, string[] co) {
      Key = key;
      Count = count;
      Width = width;
      Co = co;

      setDisplayName();
    }

    private void setDisplayName() {
      DisplayName = Key.Replace("*h", " ");
      DisplayName = DisplayName.Remove(0, 5);
    }
  }
}
