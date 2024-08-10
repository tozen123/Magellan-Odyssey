using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using System;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Linq;
using UnityEditor;

public class User
{
    public string name;


    public int adventure_points = 0;
    public int academic_points = 0;
    public string academic_rank = "Rookie";
    public string chapter_progress = "NONE";

    public User() { }

    public User(string name, 
        int adventure_points = 0, 
        int academic_points = 0, 
        string academic_rank = "Rookie",
        string chapter_progress = "NONE"
        )
    {
        this.name = name;
  
        this.adventure_points = adventure_points;
        this.academic_points = academic_points;
        this.academic_rank = academic_rank;
        this.chapter_progress = chapter_progress;
    }

   

}
