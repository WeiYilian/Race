using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class UIFaceManager : MonoBehaviour
{
   public static UIFaceManager Instance;
   
   public List<GameObject> UIFaceList;

   private void Awake()
   {
      if (Instance == null)
         Instance = this;
   }
}
