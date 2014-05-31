using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CortanaTime
{
    static class SettingsManager
    {
        static public bool UseFormalLanguage
        {
            get
            {
                if (IsolatedStorageSettings.ApplicationSettings.Contains("useFormalLanguage"))
                    return (bool)IsolatedStorageSettings.ApplicationSettings["useFormalLanguage"];
                else
                    return false;
            }
            set
            {
                if (IsolatedStorageSettings.ApplicationSettings.Contains("useFormalLanguage"))
                    IsolatedStorageSettings.ApplicationSettings["useFormalLanguage"] = value;
                else
                    IsolatedStorageSettings.ApplicationSettings.Add("useFormalLanguage", value);
                IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }
    }
}
