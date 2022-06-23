using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
   public int power = 1;
   [SerializeField] private Renderer m_Renderer;
   [SerializeField] private Color firstColor;
   [SerializeField] private Color secondColor;
   [SerializeField] private TextMeshPro text;
   
   private void Start()
   {
      text.text = (-power).ToString();
      
      if (power == 1)
      {
         m_Renderer.material.color = firstColor;
      }
      else if (power == 2)
      {
         m_Renderer.material.color = secondColor;
      }
   }
}
