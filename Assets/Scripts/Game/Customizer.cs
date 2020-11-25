using System;
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
        public List<Skin> skins;
        public Wallet wallet;
        [FormerlySerializedAs("BuyButton")] 
        public GameObject buttonCost;
        
        [FormerlySerializedAs("BuyMenu")] public Transform buyMenu;
        public Skin ActiveSkin => skins[skinIndex];

        //[FormerlySerializedAs("Wallet")] [SerializeField]
        //private Wallet _wallet;
        [FormerlySerializedAs("SkinIndex")] [SerializeField]
        public int skinIndex = 0;

        //[FormerlySerializedAs("SkinIndex")] [SerializeField]
        //private bool testMode;

        private int SkinsCount => skins.Count;
        private GameObject MainPrefab => ActiveSkin.prefab;
        private GameObject _go;
        private DataManager _dataManager;
        
        

        private void Awake()
        {
            _dataManager = GetComponent<DataManager>();
            skins = _dataManager.Skins;
            wallet = _dataManager.Wallet[0];
            skinIndex = _dataManager.SkinIndex;

            //InitSkins();
            //ChangeCoinsUI();
        }

        private void Start()
        {
            InitSkins();
            ChangeCoinsUI();
        }

        private void Update()
        {
           
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
            _go = Instantiate(MainPrefab, new Vector3(0, -196, 0), Quaternion.identity);
            _go.transform.SetParent(buyMenu);
            _go.transform.localPosition = new Vector3(0, -196, 0);
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
            
            _go = Instantiate(MainPrefab, new Vector3(0, -196, 0), Quaternion.identity);

            _go.transform.SetParent(buyMenu);
            _go.transform.localPosition = new Vector3(0, -196, 0);
            ChangeButton();
        }

        public void Accept()
        {
            _dataManager.SkinIndex = skinIndex;
            _dataManager.SaveData();
        }
        
        public void BuySkin()
        {
            if(ActiveSkin.isUnlock)
                return;
        
            switch (ActiveSkin.currency)
            {
                case Currency.Coins:

                    if (ActiveSkin.cost <= wallet.GetCoins())
                    {
                        wallet.SubCoins(ActiveSkin.cost);
                        ActiveSkin.isUnlock = true;
                        ChangeCoinsUI();
                        ChangeButton();
                    }
                    break;
            
                case Currency.Diamonds:
                
                    if (ActiveSkin.cost <= wallet.GetDiamonds())
                    {
                        wallet.SubDiamonds(ActiveSkin.cost);
                        ActiveSkin.isUnlock = true;
                        ChangeCoinsUI();
                        ChangeButton();
                    }
                    break;
            }
            _dataManager.SaveData();
        }
        
        private void InitSkins()
        {
            //Debug.Log(ActiveSkin);
            skinIndex = _dataManager.SkinIndex;
            if (!ActiveSkin.isUnlock) skinIndex = 0;
            _go = Instantiate(MainPrefab, new Vector3(0, -196, 0), Quaternion.identity);
            _go.transform.SetParent(buyMenu);
            _go.transform.localPosition = new Vector3(0, -196, 0);
        }

        private void ChangeCoinsUI() => coinsTMP.SetText(wallet.GetCoins().ToString());
        
        private void ChangeButton()
        {
            if (ActiveSkin.isAchievement || ActiveSkin.isUnlock)
            {
                buttonCost.SetActive(false);
           
            }

            if (ActiveSkin.isAchievement && !ActiveSkin.isUnlock)
            {
                _go.GetComponent<SpriteRenderer>().color = Color.black;
                _go.transform.GetChild(0).gameObject.SetActive(true);
                //buttonCost.SetActive(true);
                buttonCost.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Achievement skin");
                return;
            }
            _go.transform.GetChild(0).gameObject.SetActive(!ActiveSkin.isUnlock);


            
            buttonCost.SetActive(!ActiveSkin.isUnlock);
            
            buttonCost.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(ActiveSkin.cost.ToString());
        }
    }


    
    
    public enum Currency
    {
        Coins,
        Diamonds
    }

    
    
