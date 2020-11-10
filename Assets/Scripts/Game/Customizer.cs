using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

    public class Customizer : MonoBehaviour
    {
        [FormerlySerializedAs("CoinsText")] public TextMeshProUGUI coinsTMP;
        [FormerlySerializedAs("List Skins")] 
        [SerializeField] private List<Skin> skins = new List<Skin>();
        [SerializeField] private List<Wallet> wallet = new List<Wallet>();
        [FormerlySerializedAs("BuyButton")] 
        public GameObject buttonCost;
        
        [FormerlySerializedAs("BuyMenu")] public Transform buyMenu;
        public Skin ActiveSkin => skins[skinIndex];

        //[FormerlySerializedAs("Wallet")] [SerializeField]
        //private Wallet _wallet;
        [FormerlySerializedAs("SkinIndex")] [SerializeField]
        private int skinIndex = 0;

        //[FormerlySerializedAs("SkinIndex")] [SerializeField]
        //private bool testMode;

        private int SkinsCount => skins.Count;
        private GameObject MainPrefab => ActiveSkin.prefab;
        private GameObject _go;
        
        

        private void Awake()
        {
            
            Debug.Log(skins[0]);
            InitSkins();
            ChangeCoinsUI();
        }

        
        public void Next()
        {
            if(skinIndex < SkinsCount)
            {
                skinIndex++;
                //return;
            }

            if(skinIndex == SkinsCount)
            {
                skinIndex = 0;
                //return;
            }
            Destroy(_go);
            _go = Instantiate(MainPrefab, Vector3.zero, Quaternion.identity);
            _go.transform.SetParent(buyMenu);
            ChangeButton();
        }

        public void Prev()
        {
            if(skinIndex == 0) 
            {
                skinIndex = SkinsCount;
                //return;
            }
        
            if(skinIndex > 0)
            {
                skinIndex--;
                //return;
            }   
            
            Destroy(_go);
            _go = Instantiate(MainPrefab, Vector3.zero, Quaternion.identity);
            _go.transform.SetParent(buyMenu);
            ChangeButton();

        }

        public void Accept()
        {
            PlayerPrefs.SetInt("skinIndex", skinIndex);
            PlayerPrefs.Save();
        }
        
        public void BuySkin()
        {
            if(ActiveSkin.isBuying)
                return;
        
            switch (ActiveSkin.currency)
            {
                case Currency.Coins:
                
                    if (ActiveSkin.cost <= wallet[0].GetCoins())
                    {
                        wallet[0].SubCoins(ActiveSkin.cost);
                    }
                    break;
            
                case Currency.Diamonds:
                
                    if (ActiveSkin.cost <= wallet[0].GetDiamonds())
                    {
                        wallet[0].SubDiamods(ActiveSkin.cost);
                    }

                    break;
            }
            
            ActiveSkin.isBuying = true;
            ChangeCoinsUI();
            ChangeButton();
        }
        
        private void InitSkins()
        {
            skinIndex = PlayerPrefs.GetInt("skinIndex");
            if (!ActiveSkin.isBuying) skinIndex = 0;
            _go = Instantiate(MainPrefab, Vector3.zero, Quaternion.identity);
            _go.transform.SetParent(buyMenu);
        }

        private void ChangeCoinsUI() => coinsTMP.SetText(wallet[0].GetCoins().ToString());
        
        private void ChangeButton()
        {
            buttonCost.SetActive(!ActiveSkin.isBuying);
            buttonCost.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(ActiveSkin.cost.ToString());
        }
    }


    [CreateAssetMenu(fileName = "WalletData", menuName = "Customizer/Wallet")]
    public class Wallet: ScriptableObject
    {
        [SerializeField]
        private int coins;
        [SerializeField]
        private int diamonds;

        public int GetCoins() => coins;
        public int GetDiamonds() => diamonds;
        public void AddCoins(int value) => coins += value;
        public void SubCoins(int value) => coins -= value;
        public void AddDiamods(int value) => diamonds += value;
        public void SubDiamods(int value) => diamonds -= value;
    }
    
    public enum Currency
    {
        Coins,
        Diamonds
    }

    [CreateAssetMenu(fileName = "SkinData", menuName = "Customizer/Skin")]
    public class Skin: ScriptableObject
    {

        public Currency currency;
        [FormerlySerializedAs("Prefab")] public GameObject prefab;
        [FormerlySerializedAs("IsBuying")] public bool isBuying;
        [FormerlySerializedAs("Cost")] public int cost;
        //public int Index;
    }
