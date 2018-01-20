using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public interface ISerializable
{
    
    void Serialize(Stream filestream);
    void DeSerialize(StreamReader reader);


}
