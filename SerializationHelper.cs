using Assignment3.Utility;
using System.Runtime.Serialization;

namespace Assignment3.UnitTests
{
    public static class SerializationHelper
    {
        /// <summary>
        /// Serializes (encodes) users
        /// </summary>
        /// <param name="users">List of users</param>
        /// <param name="fileName"></param>
        public static void SerializeUsers(ILinkedListADT users, string fileName)
        {
            // uncomment once we you have implemented the SLL class
            DataContractSerializer serializer = new DataContractSerializer(typeof(SLL));
            using (FileStream stream = File.Create(fileName))
            {
               serializer.WriteObject(stream, users);
            }
            
        }

        /// <summary>
        /// Deserializes (decodes) users
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>List of users</returns>
        public static ILinkedListADT DeserializeUsers(string fileName)
        {
            // uncomment once we you have implemented the SLL class
            DataContractSerializer serializer = new DataContractSerializer(typeof(SLL));
            using (FileStream stream = File.OpenRead(fileName))
            {
               return (ILinkedListADT)serializer.ReadObject(stream);
            }
        }
    }
}
