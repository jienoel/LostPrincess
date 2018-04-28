using UnityEngine;

[CreateAssetMenu(fileName = "InnerOSConfig",menuName = "Level/InnerOSConfig", order = 30)]
public class InnerOSConfig : ScriptableObject
{
   public InnerOSItem[] items;
   
   public InnerOSItem GetOsItem(int id)
   {
      if (items == null)
         return null;
      for (int i = 0; i < items.Length; i++)
      {
         InnerOSItem item = items[i];
         if (item.id == id)
            return item;
      }

      return null;
   }
}
