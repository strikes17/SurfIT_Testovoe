using System;
using UnityEngine;

namespace Shop
{
    [Serializable]
    public class DateCompound
    {
        [Range(1,31)]
        [SerializeField] protected int _day;
        [Range(1,12)]
        [SerializeField] protected int _month;
        [Range(1964,2023)]
        [SerializeField] protected int _year;
        public DateCompound(int day, int month, int year)
        {
            _day = day;
            _month = month;
            _year = year;
        }

        public DateCompound()
        {
        }

        public int Day => _day;
        public int Month => _month;
        public int Year => _year;
    }
}