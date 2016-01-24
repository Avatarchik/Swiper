using UnityEngine;
using System.Collections;

public interface IAnimatable
{
    Animator Animator { get; set; }

    void Play(string stateName);
}
