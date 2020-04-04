using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using AupLauncher.Properties;
using Microsoft.WindowsAPICodePack.ShellExtensions;

namespace AupLauncher
{

    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("aupfile")]
    [Guid("6BAC8A97-69A2-C15C-6946-AF37B09E4229")]
    [ThumbnailProvider("AUPThumbnailer", ".aup", ThumbnailAdornment = ThumbnailAdornment.Default)]
    public class AupThumbnailProvider: ThumbnailProvider, IThumbnailFromStream, IThumbnailFromFile
    {

        private static byte[] _aviutl_signature = new byte[] {
            0x41, 0x76, 0x69, 0x55, 0x74, 0x6C, 0x20, 0x50, 0x72, 0x6F, 0x6A, 0x65, 0x63, 0x74, 0x46, 0x69,
            0x6C, 0x65, 0x20, 0x76, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x20, 0x30, 0x2E, 0x31, 0x38, 0x00
        };
        #region IThumbnailFromStream Members
        public Bitmap ConstructBitmap(Stream stream, int sideSize)
        {
            using (var br = new BinaryReader(stream, Encoding.UTF8))
            using (var sr = new StreamReader(stream, Encoding.UTF8))
            {
                if (stream.Length == 0)
                {
                    return Properties.Resources.AupFile.ToBitmap();
                }

                byte[] sig = br.ReadBytes(_aviutl_signature.Length);
                if (sig.Length == _aviutl_signature.Length)
                {
                    for (int i = 0; i < _aviutl_signature.Length; ++i)
                    {
                        if (sig[i] != _aviutl_signature[i])
                        {
                            goto audacity;
                        }
                    }
                    Icon icon = new Icon(Resources.Aviutl, sideSize,sideSize);
                    return new Bitmap(icon.ToBitmap(), sideSize, sideSize);
                }
            audacity:
                stream.Seek(0, SeekOrigin.Begin);
                string sig2 = sr.ReadLine().TrimStart();
                if (sig2.StartsWith("<?xml"))
                {

                    Icon icon = new Icon(Resources.Audacity, sideSize, sideSize);
                    return new Bitmap(icon.ToBitmap(),sideSize,sideSize);
                }
                else
                {
                    Icon icon = new Icon(Resources.AupFile, sideSize, sideSize);
                    return new Bitmap(icon.ToBitmap(), sideSize, sideSize);
                }
            }
        }

        #endregion

        #region IThumbnailFromFile Members

        public Bitmap ConstructBitmap(FileInfo info, int sideSize)
        {
            using (FileStream stream = new FileStream(info.FullName, FileMode.Open, FileAccess.Read))
            {
                return ConstructBitmap(stream, sideSize);
            }
        }

        #endregion
    }
}
