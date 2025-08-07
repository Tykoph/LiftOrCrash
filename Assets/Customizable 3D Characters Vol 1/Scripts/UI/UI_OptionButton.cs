using Character;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UI_OptionButton : MonoBehaviour
    {
        public CharacterCustomizationOption option;

        public Image icon;
        public Button selectButton;

        [SerializeField]
        private TextMeshProUGUI priceText;
        [SerializeField]
        private Image lockIcon;

        public void Initialize(CharacterCustomizationOption newOption)
        {
            this.option = newOption;
            icon.sprite = newOption.icon;
            SetBuyable(newOption.buyable, newOption.price);
            lockIcon.gameObject.SetActive(newOption.locked);
            selectButton.onClick.AddListener(SelectOption);
        }

        private void SetBuyable(bool isBuyable, float newPrice = 100f)
        {
            option.buyable = isBuyable;
            option.price = newPrice;
            if (option.buyable)
            {
                priceText.text = option.price + "$";
            }
            else
            {
                priceText.text = "";
            }
        }

        private void SelectOption()
        {
            if (option.locked)
            {
                if (!option.buyable) return;

                float money = GameManager.GMInstance.Money;
                if (money >= option.price)
                {
                    GameManager.GMInstance.SpendMoney(option.price);
                    option.Unlock();
                    lockIcon.gameObject.SetActive(false);
                    SetBuyable(false);
                }
                else
                {
                    Debug.Log("Not enough money to buy this option.");
                }
                return;
            }

            UI_CusomizationManager.Instance.SelectOption(option.id);
        }
    }
}
