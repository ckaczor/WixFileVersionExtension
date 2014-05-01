using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

using Microsoft.Tools.WindowsInstallerXml;

namespace WixFileVersionExtension
{
    public class WixFileVersionPreprocessorExtension : PreprocessorExtension
    {        
        private static readonly string[] prefixes = { "fileVersion" };

        public override string[] Prefixes
        {
            get { return prefixes; }
        }

        public override string EvaluateFunction(string prefix, string function, string[] args)
        {
            switch (prefix)
            {
                case "fileVersion":
                    // Make sure there actually is a file name
                    if (args.Length == 0 || args[0].Length == 0)
                        throw new ArgumentException("File name not specified");

                    // Make sure the file exists
                    if (!File.Exists(args[0]))
                        throw new ArgumentException(string.Format("File name {0} does not exist", args[0]));

                    // Get the file version information for the given file
                    FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(args[0]);

                    // Get the property that matches the name of the function
                    PropertyInfo propertyInfo = fileVersionInfo.GetType().GetProperty(function);

                    // Make sure the property exists
                    if (propertyInfo == null)
                        throw new ArgumentException(string.Format("Unable to find property {0} in FileVersionInfo", function));

                    // Return the value of the property as a string
                    return propertyInfo.GetValue(fileVersionInfo, null).ToString();
            }

            return null;
        }
    }
}
