using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GreenwichButchers.SystemClasses
{
    [Serializable]
    public class ObjectConvert
    {
        public string ObjectToString64(object Object)
        {
            try
            {
                // Used to hold the memory stream data
                byte[] arrObject; 
                var binaryF = new BinaryFormatter();
                using (var memoryS = new MemoryStream())
                {
                    binaryF.Serialize(memoryS, Object);
                    arrObject = memoryS.ToArray();
                }

                return Convert.ToBase64String(arrObject);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public object String64ToObject(string Base64)
        {
            try
            {
                object Object = null;
                if (Base64 != "")
                {
                    var arrObject = Convert.FromBase64String(Base64);
                    var binaryF = new BinaryFormatter();
                    using (var memoryS = new MemoryStream(arrObject))
                    {
                        Object = binaryF.Deserialize(memoryS);
                    }
                }
                return Object;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}