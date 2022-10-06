using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Yeni Soru Oluştur",menuName = "Sorular/Soru")]
public class SoruData : ScriptableObject
{
    public string soru;
    public string[] cevaplar;
    public string dogruCevap;
}
