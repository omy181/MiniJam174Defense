using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;
using UnityEngine.EventSystems;
using UnityEditor;

namespace Holylib.Utilities
{
    public static class HolyUtilities
    {
        public static void CopyProperties(this object source, object destination)
        {
            // If any this null throw an exception
            if (source == null || destination == null)
                throw new Exception("Source or/and Destination Objects are null");
            // Getting the Types of the objects
            Type typeDest = destination.GetType();
            Type typeSrc = source.GetType();

            // Iterate the Properties of the source instance and  
            // populate them from their desination counterparts  
            PropertyInfo[] srcProps = typeSrc.GetProperties();
            foreach (PropertyInfo srcProp in srcProps)
            {
                if (!srcProp.CanRead)
                {
                    continue;
                }
                PropertyInfo targetProperty = typeDest.GetProperty(srcProp.Name);
                if (targetProperty == null)
                {
                    continue;
                }
                if (!targetProperty.CanWrite)
                {
                    continue;
                }
                if (targetProperty.GetSetMethod(true) != null && targetProperty.GetSetMethod(true).IsPrivate)
                {
                    continue;
                }
                if ((targetProperty.GetSetMethod().Attributes & MethodAttributes.Static) != 0)
                {
                    continue;
                }
                if (!targetProperty.PropertyType.IsAssignableFrom(srcProp.PropertyType))
                {
                    continue;
                }
                // Passed all tests, lets set the value
                targetProperty.SetValue(destination, srcProp.GetValue(source, null), null);
            }
        }

        public static void CopyPropertiesTo(this object fromObject, object toObject)
        {
            PropertyInfo[] toObjectProperties = toObject.GetType().GetProperties();
            foreach (PropertyInfo propTo in toObjectProperties)
            {
                PropertyInfo propFrom = fromObject.GetType().GetProperty(propTo.Name);
                if (propFrom != null && propFrom.CanWrite)
                    propTo.SetValue(toObject, propFrom.GetValue(fromObject, null), null);
            }
        }
        public static bool isOnUI
        {
            get=> EventSystem.current.IsPointerOverGameObject();
        }

        public static Vector3 GetMouseWorldPos()
        {
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            return pos;
        }

        /// calculate the rotation between two positions
        public static Quaternion LookAtPosition(Vector3 from,Vector3 towards)
        {
            var rot = Quaternion.LookRotation((-towards + from).normalized, Vector3.forward);

            return rot;
        }

        public static List<T>FindAllItemsInAssets<T>() where T : UnityEngine.Object
        {
            List<T> foundItems = new List<T>();
#if UNITY_EDITOR
            string[] allAssetPaths = AssetDatabase.GetAllAssetPaths();

            foreach (string assetPath in allAssetPaths)
            {
                UnityEngine.Object asset = AssetDatabase.LoadAssetAtPath(assetPath, typeof(UnityEngine.Object));

                if (asset is T)
                {
                    foundItems.Add(asset as T);
                }
            }
#endif
            return foundItems;
        }

        public static List<T> FindAllItemsInAssets<T>(string folderPath) where T : UnityEngine.Object
        {
            List<T> foundItems = new List<T>();

#if UNITY_EDITOR
            string[] guids = AssetDatabase.FindAssets("", new[] { folderPath });

            foreach (string guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                UnityEngine.Object asset = AssetDatabase.LoadAssetAtPath(assetPath, typeof(UnityEngine.Object));

                if (asset is T)
                {
                    foundItems.Add(asset as T);
                }
            }          
#endif

            return foundItems;
        }

        public static List<T> FindAllItemsInResources<T>(string folderPath) where T : UnityEngine.Object
        {
            return Resources.LoadAll<T>(folderPath).ToList();
        }

        public static void ChangeMaterialColor(MeshRenderer renderer, Color color)
        {
            MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();

            renderer.GetPropertyBlock(propertyBlock);

            propertyBlock.SetColor("_BaseColor", color);

            renderer.SetPropertyBlock(propertyBlock);
        }
    }
}
