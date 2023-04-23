using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioProcessor
{
    class Program
    {
        static async Task Main(string[] args)
        {

            string largeFilePath = "sample1.mp3";
            string largeFilePath2 = "sample2.mp3";

            string smallFilePath = "trim1.mp3";
            ProcessAudio processAudio = new ProcessAudio();
            //register audios
          //  processAudio.StoreAudioFileFingerprintsInStorageForLaterRetrieval(largeFilePath);
            //processAudio.StoreAudioFileFingerprintsInStorageForLaterRetrieval(largeFilePath2);
            //match audios
            Console.WriteLine(await processAudio.matchAudioAsync(smallFilePath));
            Console.ReadLine();
        }
    }
}
