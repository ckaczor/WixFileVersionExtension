using Microsoft.Tools.WindowsInstallerXml;

namespace WixFileVersionExtension
{
    public class WixFileVersionExtension : WixExtension
    {
        private WixFileVersionPreprocessorExtension _versionPreprocessorExtension;

        public override PreprocessorExtension PreprocessorExtension
        {
            get
            {
                // If we haven't created the preprocessor then do it now
                if (_versionPreprocessorExtension == null)
                    _versionPreprocessorExtension = new WixFileVersionPreprocessorExtension();

                // Return the preprocessor
                return _versionPreprocessorExtension;
            }
        }
    }
}
