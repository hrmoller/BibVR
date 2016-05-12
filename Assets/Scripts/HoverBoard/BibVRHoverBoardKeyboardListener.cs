using System.Linq;
using Hover.Board;
using Hover.Board.Items;
using Hover.Common.Items;
using UnityEngine;

namespace Hover.Demo.BoardKeys {

  /*================================================================================================*/
  public class BibVRHoverBoardKeyboardListener : MonoBehaviour {

    public DemoEnvironment vEnviro;
    public BibVRTextField vTextField;


    ////////////////////////////////////////////////////////////////////////////////////////////////
    /*--------------------------------------------------------------------------------------------*/
    public void Awake() {
      vEnviro = GameObject.Find("BibVRHoverVREnvironment").GetComponent<DemoEnvironment>();
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
      Debug.Log("hest");
      Debug.Log(pItem.Label);
      if( pItem.Label == "^" ) {
        return;
      }

      if( pItem.Label.Length == 1 ) {
        //vEnviro.AddLetter(pItem.Label[0]);
        vTextField.AddLetter(pItem.Label[0]);
        return;
      }

      if( pItem.Label.ToLower() == "back" ) {
        //vEnviro.RemoveLatestLetter();
        vTextField.RemoveLatestLetter();
      }

      if( pItem.Label.ToLower() == "enter" ) {
        Debug.Log(vTextField.GetLetter());
        vTextField.ClearLetters();
      }
    }
  }
}
