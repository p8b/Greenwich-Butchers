using System;

namespace GreenwichButchers.SystemClasses
{
    public class faviconController
    {
        public string getIcon()
        {
            int iconNum = new Random().Next(1, 5);

            return "favicon " + iconNum + ".ico";
        }
    }
}