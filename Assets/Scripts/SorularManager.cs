using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class SorularManager : MonoBehaviour
{
   [SerializeField] private List<SoruData> sorularList;

   [SerializeField] private Transform cevaplarPanel;
   [SerializeField] private GameObject kontrolEtBtn;
   [SerializeField] private TextMeshProUGUI soruTxt;
   [SerializeField] private GameObject sonucPanel;
   [SerializeField] private TextMeshProUGUI sonucTxt;

   private int kacinciSoru;
   private int toplamSoru;

   private bool sonucDogrumu;

   private string butonAdi;
   private int cevapAdet;
   private string dogruCevap;
   private int dogruAdet;

   private void Start()
   {
       kacinciSoru = 0;
       toplamSoru = sorularList.Count;

       foreach (Transform cevapBtn in cevaplarPanel)
       {
           cevapBtn.GetComponent<Button>().onClick.AddListener(()=>ButonaBasildi());
       }
       
       
       SorulariveCevaplariYerlestir();
   }

   void SorulariveCevaplariYerlestir()
   {
       soruTxt.GetComponent<CanvasGroup>().alpha = 0f;
       kontrolEtBtn.GetComponent<CanvasGroup>().alpha = 0f;

       foreach (Transform cevap in cevaplarPanel)
       {
           cevap.GetComponent<CanvasGroup>().alpha = 0f;
       }
       
       DigerleriniKapat();
       
       soruTxt.text = sorularList[kacinciSoru].soru;

       for (int i = 0; i < cevaplarPanel.childCount; i++)
       {
           cevaplarPanel.GetChild(i).Find("cevapTxt").GetComponent<TextMeshProUGUI>().text =
               sorularList[kacinciSoru].cevaplar[i];
       }

       dogruCevap = sorularList[kacinciSoru].dogruCevap;
       StartCoroutine(CevaplariAcRoutine());
   }

   IEnumerator CevaplariAcRoutine()
   {
       yield return new WaitForSeconds(.5f);

       soruTxt.GetComponent<CanvasGroup>().DOFade(1, .2f);

       cevapAdet = 0;

       while (cevapAdet<4)
       {
           cevaplarPanel.GetChild(cevapAdet).GetComponent<CanvasGroup>().DOFade(1, .2f);
           cevapAdet++;
           yield return new WaitForSeconds(.1f);
       }
       
       kontrolEtBtn.GetComponent<CanvasGroup>().DOFade(1, .2f);

   }

   void ButonaBasildi()
   {
       butonAdi = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.name;
       
       DigerleriniKapat();
       
       UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.Find("secilmisResim").gameObject.SetActive(true);
       UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.Find("ust").gameObject.SetActive(false);
       if (butonAdi == dogruCevap)
       {
           sonucDogrumu = true;
       }
       else
       {
           sonucDogrumu = false;
       }
   }

   void DigerleriniKapat()
   {
       foreach (Transform cevap in cevaplarPanel)
       {
           if(cevap.Find("secilmisResim").transform.gameObject.activeInHierarchy)
               cevap.Find("secilmisResim").transform.gameObject.SetActive(false);
           
           if(!cevap.Find("ust").transform.gameObject.activeInHierarchy)
               cevap.Find("ust").transform.gameObject.SetActive(true);
       }
   }

   public void SonucuKontrolEt()
   {
       if (sonucDogrumu)
       {
           print("sonucDogru");
           dogruAdet++;
       }
       else
       {
           print("sonuc yanlış");
       }

       kacinciSoru++;
       butonAdi = "";
       if (kacinciSoru < toplamSoru)
       {
           Invoke(nameof(SorulariveCevaplariYerlestir),1f);
       }
       else
       {
           
           sonucPanel.gameObject.SetActive(true);
           sonucTxt.text = "Toplam " + toplamSoru + " adet sorudan " + dogruAdet + " adet soru doğru bildin";
       }
   }
}
