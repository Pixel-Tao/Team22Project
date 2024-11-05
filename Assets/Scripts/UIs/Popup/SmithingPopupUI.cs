using Defines;
using UnityEngine.UI;

namespace Assets.Scripts.UIs.Popup
{
    internal class SmithingPopupUI : UIPopup
    {
        /*public Button createWeaponButton;
        public Button createAromorButton;
        public Button exitPanelButton;*/
        public void CreateWeaponButton()
        {
            CreateButtonHelper(EquipType.Weapon);
        }

        public void CreateArmorButton()
        {
            CreateButtonHelper(EquipType.Helmet);
        }

        public void CreateButtonHelper(EquipType type)
        {
            ItemSO item = GameManager.Instance.GetRandomEquip(type);
            CharacterManager.Instance.AddItem(item);
        }
    }
}
