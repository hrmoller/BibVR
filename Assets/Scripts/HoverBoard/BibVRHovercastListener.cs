using System.Linq;
using Hover.Board;
using Hover.Board.Items;
using Hover.Cast;
using Hover.Cast.State;
using UnityEngine;

namespace BibVR {

  /*================================================================================================*/
  public class BibVRHovercastListener : MonoBehaviour {

    private GameObject vTextField;
    private HoverboardSetup vHoverboardSetup;
    private HovercastSetup vHovercastSetup;
    private ItemPanel[] vKeyboardItemPanels;
    private bool vPrevEnableKey;


    ////////////////////////////////////////////////////////////////////////////////////////////////
    /*--------------------------------------------------------------------------------------------*/
    public void Awake() {
      vTextField = GameObject.Find("BibVRTextField");
      vHoverboardSetup = GameObject.Find("Hoverboard").GetComponent<HoverboardSetup>();
      vHovercastSetup = GameObject.Find("Hovercast").GetComponent<HovercastSetup>();

      vPrevEnableKey = true;
    }

    /*--------------------------------------------------------------------------------------------*/
    public void Start() {
      vKeyboardItemPanels = vHoverboardSetup.Panels
      .Select(x => x.GetPanel())
      .ToArray();
    }

    /*--------------------------------------------------------------------------------------------*/
    public void Update() {
      IHovercastState state = vHovercastSetup.State;
      bool enableKey = (state.Menu.DisplayStrength <= 0);

      if( vPrevEnableKey == enableKey ) {
        return;
      }

      vTextField.SetActive(enableKey);

      foreach ( ItemPanel itemPanel in vKeyboardItemPanels ) {
        itemPanel.IsEnabled = enableKey;
      }

      vPrevEnableKey = enableKey;
    }

  }

}
