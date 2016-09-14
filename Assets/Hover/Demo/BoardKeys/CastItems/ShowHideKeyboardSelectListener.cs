using Hover.Board.Custom;
using Hover.Common.Items;
using Hover.Common.Items.Types;
using UnityEngine;

namespace Hover.Demo.BoardKeys.CastItems {

  /*================================================================================================*/
  public class ShowHideKeyboardSelectListener : DemoBaseListener<ICheckboxItem> {

    public GameObject Keyboard;

    private InteractionSettings vInteractSett;
    private ShowHideKeyboard keyboardToggleComponent;

    ////////////////////////////////////////////////////////////////////////////////////////////////
    /*--------------------------------------------------------------------------------------------*/
    protected override void Setup() {
      Debug.Log("Setup :: ShowHideKeyboardSelectListener");
      base.Setup();

      vInteractSett = HoverboardSetup.InteractionSettings.GetSettings();
      Item.OnValueChanged += HandleValueChanged;
      keyboardToggleComponent = Keyboard.GetComponent<ShowHideKeyboard>();
    }

    /*--------------------------------------------------------------------------------------------*/
    protected override void BroadcastInitialValue() {
      HandleValueChanged(Item);
    }


    ////////////////////////////////////////////////////////////////////////////////////////////////
    /*--------------------------------------------------------------------------------------------*/
    private void HandleValueChanged(ISelectableItem<bool> pItem) {
      Debug.Log("HandleValueChanged");
      keyboardToggleComponent.toggleKeyboard(pItem.Value);
    }
  }
}
