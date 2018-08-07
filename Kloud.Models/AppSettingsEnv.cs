using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kloud.Models
{
    public class AppSettingsEnv
    {
        #region Private
        //singleton
        private static AppSettingsEnv instance;
        private string GetVariableValue(string variableName)
        {
            return Environment.GetEnvironmentVariable(variableName, EnvironmentVariableTarget.Process);
        }
        #endregion

        public string ApiUrl { get { return this.GetVariableValue("ApiUrl"); } }
        
        public static AppSettingsEnv Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AppSettingsEnv();
                }
                return instance;
            }
        }
    }
}
