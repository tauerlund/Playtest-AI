using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace HelperMethods {
  /// <summary>
  /// Represents methods to save or load an object in binary format from a file path. 
  /// </summary>
  public class StorageManager {

    /// <summary>
    /// Loads an object from the spefied file path.
    /// </summary>
    /// <typeparam name="T">The object to load.</typeparam>
    /// <param name="filePath">The file path from which the object should be loaded.</param>
    /// <returns></returns>
    public T LoadObject<T>(string filePath) {
      T objectToLoad = default(T);
      using(FileStream filestreamRader = new FileStream(filePath, FileMode.Open, FileAccess.Read)) {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        objectToLoad = (T)binaryFormatter.Deserialize(filestreamRader);
        filestreamRader.Close();
      }
      return objectToLoad;
    }

    /// <summary>
    /// Saves the given object to the specified path.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="objectToSave"></param>
    /// <param name="filePath"></param>
    public void SaveObject<T>(T objectToSave, string filePath) {
      using(FileStream filestreamWriter = new FileStream(filePath, FileMode.Create, FileAccess.Write)) {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(filestreamWriter, objectToSave);
        filestreamWriter.Close();
      }
    }
  }
}
