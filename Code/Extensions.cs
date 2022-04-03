using System.Collections.Generic;
using Code.Data;
using UnityEngine;

namespace Code
{
    public static class Extensions
    {
        public static Vector3 Where(this Vector3 vector3, float? x=null, float? y=null, float? z=null) => 
            new Vector3(x ?? vector3.x, y ?? vector3.y, z ?? vector3.z);

        public static Vector2 Where(this Vector2 vector2, float? x=null, float? y=null) => 
            new Vector2(x ?? vector2.x, y ?? vector2.y);

        public static Vector3 DirectionTo(this Vector3 vector3, Vector3 to) => 
            Vector3.Normalize(to - vector3);

        public static Vector3 AddY(this Vector3 vector3, float value)
        {
            vector3.y += value;
            return vector3;
        }
        
        public static Vector3Data AsVector3Data(this Vector3 vector3) =>
            new Vector3Data(vector3.x, vector3.y, vector3.z);
        
        public static Vector3 AsVector3(this Vector3Data vector3) =>
            new Vector3(vector3.X, vector3.Y, vector3.Z);

        public static T ToDeserialized<T>(this string json) =>
            JsonUtility.FromJson<T>(json);

        public static string ToJson(this object obj) =>
            JsonUtility.ToJson(obj);

        public static T GetRandom<T>(this List<T> self) => 
            self[Random.Range(0, self.Count)];
    }
}
