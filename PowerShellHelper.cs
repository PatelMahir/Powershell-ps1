using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Management.Automation;
using System.Reflection;
using System.Text;
using NUnit.Framework;

namespace Utils
{
    public class PowerShellHelper
    {
        public void Execute(string command)
        {
            using (var ps = PowerShell.Create())
            {
                var results = ps.AddScript(command).Invoke();
                foreach (var result in results)
                {
                    Debug.Write(result.ToString());
                }
            }
        }

        public void RunPowerShellScript(string resourceName)
        {
            var content = ReadEmbeddedResourceContent(resourceName);
            new PowerShellHelper().Execute(content);
        }

        private string ReadEmbeddedResourceContent(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var streamNames = assembly.GetManifestResourceNames();
            foreach (var streamName in streamNames)
            {
                if (streamName.EndsWith(resourceName))
                {
                    using (var stream = assembly.GetManifestResourceStream(streamName))
                    using (var reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            throw new ApplicationException($"Could not find resource {resourceName}");
        }
    }
}
