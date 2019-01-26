
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{

   public class PointWrapper
    {
        public Vector3 pt;
        public bool visited = false;
        public PointWrapper(Vector3 p)
        {
            pt = p;
        }
    }

    public static class Ran
    {
        public static void Shuffle<T>(this System.Random rng, T[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }
    }

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