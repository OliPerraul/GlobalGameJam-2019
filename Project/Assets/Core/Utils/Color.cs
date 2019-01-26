
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utils
{

    public class Color
    {
        public static void SetR(ref Image im, float r)
        {
            var c = im.color;
            c.r = r;
            im.color = c;
        }

        public static void SetG(ref Image im, float g)
        {
            var c = im.color;
            c.g = g;
            im.color = c;
        }

        public static void SetB(ref Image im, float b)
        {
            var c = im.color;
            c.b = b;
            im.color = c;
        }


        public static void SetA(ref Image im, float a)
        {
            var c = im.color;
            c.a = a;
            im.color = c;
        }
    }

}