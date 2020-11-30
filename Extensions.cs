using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Ssepan.Utility;
using System.Diagnostics;
using System.Reflection;

namespace Ssepan.Conversion
{
    /// <summary>
    /// Convert items to and from Byte[].
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Convert Object to Byte[]. To use with custom classes, add [Serializable] attribute to custom class.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Byte[] ToByteArray(this Object objectInstance)
        {
            Byte[] returnValue = default(Byte[]);
            try
            {
                if (objectInstance == null)
                {
                    returnValue = null;
                }
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                MemoryStream memoryStream = new MemoryStream();
                binaryFormatter.Serialize(memoryStream, objectInstance);
                
                returnValue = memoryStream.ToArray();
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
                        
                throw;
            }
            return returnValue;
        }

        /// <summary>
        /// Convert Byte[] to Object. Caller must explicitly cast returned value to sub-class of Object.
        /// </summary>
        /// <param name="arrBytes"></param>
        /// <returns></returns>
        public static Object ToObject(this Byte[] bytes)
        {
            Object returnValue = default(Object);
            try
            {
                MemoryStream memoryStream = new MemoryStream();
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                memoryStream.Write(bytes, 0, bytes.Length);
                memoryStream.Seek(0, SeekOrigin.Begin);
                Object objectInstance = (Object)binaryFormatter.Deserialize(memoryStream);
                
                returnValue = objectInstance;
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
                        
                throw;
            }
            return returnValue;
        }

        /// <summary>
        /// Convert String to Byte[].
        /// </summary>
        /// <param name="stringInstance"></param>
        /// <returns></returns>
        public static Byte[] ToByteArray(this String stringInstance)
        {
            Byte[] returnValue = default(Byte[]);
            try
            {
                returnValue = ToByteArray(stringInstance, Encoding.Default);
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
                        
                throw;
            }
            return returnValue;
        }

        /// <summary>
        /// Convert String to Byte[], using encoding.
        /// </summary>
        /// <param name="stringInstance"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static Byte[] ToByteArray(this String stringInstance, Encoding encoding)
        {
            Byte[] returnValue = default(Byte[]);
            try
            {
                returnValue = encoding.GetBytes(stringInstance);
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
                        
                throw;
            }
            return returnValue;
        }

        /// <summary>
        /// Convert Byte[] to String. Alternative to built-in ToString().
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static String ToString2(this Byte[] bytes)
        {
            String returnValue = default(String);
            try
            {
                returnValue = ToString2(bytes, Encoding.Default);
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
                        
                throw;
            }
            return returnValue;
        }
 
        /// <summary>
        /// Convert Byte[] to String, using encoding. 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static String ToString2(this Byte[] bytes, Encoding encoding)
        {
            String returnValue = default(String);
            try
            {
                returnValue = encoding.GetString(bytes);
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
                        
                throw;
            }
            return returnValue;
        }
   }
}
