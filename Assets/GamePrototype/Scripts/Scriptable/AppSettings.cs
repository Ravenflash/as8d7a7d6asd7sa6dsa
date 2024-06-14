using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ravenflash.GamePrototype
{
    [CreateAssetMenu(fileName = "New App Settings", menuName = "Ravenflash/App Settings", order = 20)]
    public class AppSettings : ScriptableObject
    {
        public int fps = 30;
        // other app settings ?
    }
}
