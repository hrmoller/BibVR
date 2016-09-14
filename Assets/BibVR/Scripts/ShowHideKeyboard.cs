using UnityEngine;

public class ShowHideKeyboard : MonoBehaviour {

  public Vector3 shownPosition;
  public Vector3 hiddenPosition = new Vector3(0f, -0.8f, 0.575f);
  public bool Toggler = true;

  private bool toggled = true;

  void Update(){
    if(Toggler != toggled){
      toggleKeyboard(Toggler);
    }
  }

  public void toggleKeyboard(bool show) {
    toggled = show;
    Toggler = toggled;
    if(show){
//      transform.localPosition = shownPosition;
      iTween.MoveTo(gameObject, transform.TransformVector(shownPosition), 0.3f);
    } else {
//      transform.localPosition = hiddenPosition;
      iTween.MoveTo(gameObject, transform.TransformVector(hiddenPosition), 0.3f);
    }
  }
}
*