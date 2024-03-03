
using System;
using System.Collections;
using UnityEngine;

public class FusionArcane : Fusion{
    private IEnumerator StartArcaneFusionRoutine(CardArcane arcane1, CardArcane arcane2){
        yield return new WaitForSeconds(1);
        Debug.Log("Implement Arcane Fusion");
    }
    public void ArcaneFusion(CardArcane arcane1, CardArcane arcane2){
        StartCoroutine(StartArcaneFusionRoutine(arcane1, arcane2));
    }
}