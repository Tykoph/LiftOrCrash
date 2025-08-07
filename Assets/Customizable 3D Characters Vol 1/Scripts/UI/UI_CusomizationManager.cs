 using Character;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UI_CusomizationManager : MonoBehaviour
    {
        public static UI_CusomizationManager Instance;

        private void Awake()
        {
            Instance = this;
        }

        public GameObject categoryButtonPrefab;
        public Transform categoryButtonContainer;

        public CharacterCustomizationManager customizationManager;
        public CharacterCustomizationCategory selectedCategory;
        public GameObject optionsPanel;
        public GameObject wardrobeBase;
        public Button closeButton;

        private bool initialized;

        private void Start()
        {
            closeButton.onClick.AddListener(HideWardrobe);
            HideWardrobe();
        }

        private void HideWardrobe()
        {
            wardrobeBase.SetActive(false);
            optionsPanel.SetActive(false);
        }
        
        [ContextMenu("Initialize")]
        public void Initialize(CharacterCustomizationManager customizationManager)
        {
            if (initialized) { return; }
            this.customizationManager = customizationManager;
            foreach (CharacterCustomizationCategory category in customizationManager.categories)
            {
                GameObject categoryPrefab = Instantiate(categoryButtonPrefab, categoryButtonContainer);

                categoryPrefab.GetComponent<UI_CategoryButton>().Initialize(category);
            }
            initialized = true;
        }

        public void SelectCategory(CharacterCustomizationCategory category)
        {
            selectedCategory = category;
            optionsPanel.SetActive(true);
        }

        public void SelectOption(string optionID)
        {
            customizationManager.SelectOption(selectedCategory.id, optionID);
        }
    }
}