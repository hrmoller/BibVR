using System.Linq;
using Hover.Board;
using Hover.Board.Items;
using Hover.Common.Items;
using UnityEngine;

namespace BibVR.KeyBoard {

  /*================================================================================================*/
  public class BibVRHoverBoardKeyboardListener : MonoBehaviour {

    public BibVRTextField vTextField;


    ////////////////////////////////////////////////////////////////////////////////////////////////
    /*--------------------------------------------------------------------------------------------*/
    public void Awake() {
      vTextField = GameObject.Find("BibVRTextField").GetComponent<BibVRTextField>();
    }

    /*--------------------------------------------------------------------------------------------*/
    public void Start() {
      ItemPanel[] itemPanels = GameObject.Find("Hoverboard")
      .GetComponentInChildren<HoverboardSetup>()
      .Panels
      .Select(x => x.GetPanel())
      .ToArray();

      foreach ( ItemPanel itemPanel in itemPanels ) {
        foreach ( IItemLayout itemLayout in itemPanel.Layouts ) {
          foreach ( IBaseItem item in itemLayout.Items ) {
            ISelectableItem selItem = (item as ISelectableItem);

            if( selItem == null ) {
              continue;
            }

            selItem.OnSelected += HandleItemSelected;
          }
        }
      }
    }


    ////////////////////////////////////////////////////////////////////////////////////////////////
    /*--------------------------------------------------------------------------------------------*/
    private void HandleItemSelected(ISelectableItem pItem) {
      if( pItem.Label == "^" ) {
        return;
      }

      if( pItem.Label.Length == 1 ) {
        vTextField.AddLetter(pItem.Label[0]);
        return;
      }

      if( pItem.Label.ToLower() == "back" ) {
        vTextField.RemoveLatestLetter();
      }

      if( pItem.Label.ToLower() == "enter" ) {
        Debug.Log("enter was pressed - search to be executed...");
        // TODO execute search // hide keyboard
        vTextField.ClearLetters();
      }
    }
  }
}
