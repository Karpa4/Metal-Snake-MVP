using System.Collections.Generic;
using UnityEngine;
using System;

namespace Game.PlayerConstructor
{
    [CreateAssetMenu(fileName = "New AllDetails", menuName = "Details/All Details")]
    public class AllDetails : ScriptableObject
    {
        [SerializeField] private List<DetailsList> allDetails;
        public List<DetailsList> AllDetailsLists { get => allDetails; }
    }

    [Serializable]
    public class DetailsList
    {
        [SerializeField] private List<Detail> details;
        public List<Detail> Details { get => details; }
    }
}
