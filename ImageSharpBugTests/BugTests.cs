using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Diagnostics;
using System.IO;
using Xunit;

namespace ImageSharpBugTests
{
    public class BugTests
    {
        [Fact]
        public void SaveAsPngAndReload()
        {
            var bytes = File.ReadAllBytes("bgra32-612x858.raw");


            using (var memoryStream = new MemoryStream())
            {
                using (var fromRaw = Image.LoadPixelData<Bgra32>(bytes, 612, 858))
                    fromRaw.SaveAsPng(memoryStream);
                memoryStream.Position = 0;

                // This load throws on net6, but works on net5
                using (var fromMemory = Image.Load(memoryStream))
                {
                    Debug.WriteLine("Test passed!");
                }
            }
        }
    }
}