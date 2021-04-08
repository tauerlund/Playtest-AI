using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace HelperMethods {
  /// <summary>
  /// Represents a way to make a deep of an object by serialization. 
  /// </summary>
  public class BinaryCopier {

    /// <summary>
    /// Makes a deep copy of the given object. NB: Use with caution if performance is important. 
    /// </summary>
    /// <typeparam name="T">The type of the object to copy.</typeparam>
    /// <param name="objectToCopy">The object that should be copied.</param>
    /// <returns></returns>
    public T GetCopy<T>(T objectToCopy) {

      T objectCopy = default(T);

      MemoryStream memoryStream = new MemoryStream();
      BinaryFormatter binaryFormatter = new BinaryFormatter();

      using(memoryStream) {
        //  Serialize this agent into the memory stream.
        binaryFormatter.Serialize(memoryStream, objectToCopy);
        // Set the position in memory stream to the beginning.
        memoryStream.Seek(0, SeekOrigin.Begin);
        // Dezerialize the memory stream into the agent copy. 
        objectCopy = (T)binaryFormatter.Deserialize(memoryStream);
        memoryStream.Close();
      }
      return objectCopy;
    }
  }
}
