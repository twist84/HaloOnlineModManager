using System;
using System.Diagnostics;
using System.IO;
using Xdelta.Instructions;

namespace Xdelta.Patch
{
    class PatchClass
    {
        internal static void Main(string p1, string patchFile, string p2, string datFileNameExt)
        {
            Stopwatch watcher = Stopwatch.StartNew();

            using (FileStream source = OpenForRead(p1))
            using (FileStream patch = OpenForRead(patchFile))
            using (FileStream target = CreateForWriteAndRead(p2))
                new Xdelta.Decoder(source, patch, target).Run();
            watcher.Stop();
        }

        private static FileStream OpenForRead(string filePath)
        {
            return new FileStream(
                filePath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read,
                4096,   // Default buffer, it seems bigger does not affect time
                FileOptions.RandomAccess);
        }

        private static FileStream CreateForWriteAndRead(string filePath)
        {
            return new FileStream(
                filePath,
                FileMode.Create,
                FileAccess.ReadWrite,
                FileShare.Read,
                4096,   // Default buffer, it seems bigger does not affect time
                FileOptions.RandomAccess);
        }
    }
}