
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public enum DataTypes
    {
        Double,
        Float,
        Int,
        Long,
        String,
        Char,
        Bool
    }

    public enum ComparisonOperators
    {
        GreaterThan,
        GreaterThanEqual,
        LesserThan,
        LesserThanEqual,
        Equal        
    }


    public class Misc
    {

    }

    public class Mathf2
    {
        private const float tolerance = 0.1f;
        public static bool CloseEnough(float a, float b)
        {
            return (Mathf.Abs(a - b) < tolerance);
        }

    }

}