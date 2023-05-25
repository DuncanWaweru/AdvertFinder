using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech;
using System.Speech.Recognition;
using NAudio.Wave;
using System.IO;
using System.Speech.AudioFormat;

namespace AudioProcessor
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {

                // You can set to -1 to disable logging messages
               // Vosk.Vosk.SetLogLevel(0);


             

                string largeFilePath = "https://stream.globetrack.co.ke/radio/f587fd1c-3b24-4ebd-a7c3-177731da3dce/2023-04-24/06-43-58.mp3";
                string largeFilePath2 = "inooro_clip.mp3";

                string smallFilePath = "https://globetrack.blob.core.windows.net/default/2023_4_24/8d99ee73-b577-4281-a7b0-bd01d5489dbe/inoorofm_24-04-2023_00-00-00_4ibwjsq0.ttg.mp3";
                ProcessAudio processAudio = new ProcessAudio();
              //  var sourceFile = Path.GetRandomFileName() + ".wav";
              var data =  Mp3ToWav(largeFilePath2);
                Console.WriteLine(data);
               // var result = ReadAudio(smallFilePath);
                //register audios
                //  processAudio.StoreAudioFileFingerprintsInStorageForLaterRetrieval(largeFilePath);
                //processAudio.StoreAudioFileFingerprintsInStorageForLaterRetrieval(largeFilePath2);
                //match audios

                //  Console.WriteLine(await processAudio.matchAudioAsync(smallFilePath));
                Console.ReadLine();
            }
            catch (Exception es)
            {

                throw;
            }
        }
        public static string myEngine(string url)
        {
            return "";

        }

        public static string Mp3ToWav(string mp3File)
        {

            var videoUrl = "https://stream.globetrack.co.ke/radio/f587fd1c-3b24-4ebd-a7c3-177731da3dce/2023-04-24/06-43-58.mp3";// "https://sec.ch9.ms/ch9/0334/cf0bd333-9c8a-431e-bc62-8089aea60334/WhatsCoolFallCreators.mp4";
            var pathFile = Path.GetRandomFileName().Replace(".", "");
            var mp3Path2 = Path.Combine(pathFile,"test2.mp3");
            using (var reader = new MediaFoundationReader(videoUrl))
            {
               MediaFoundationEncoder.EncodeToMp3(reader, mp3Path2);
            }

            using (MediaFoundationReader reader = new MediaFoundationReader(mp3File))
            {
                WaveStream pcmStream = WaveFormatConversionStream.CreatePcmStream(reader);

                try
                {

                    SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
                    Grammar gr = new DictationGrammar();
                    sre.LoadGrammar(gr);
                    sre.SetInputToWaveStream(pcmStream);
                    sre.BabbleTimeout = new TimeSpan(Int32.MaxValue);
                    sre.InitialSilenceTimeout = new TimeSpan(Int32.MaxValue);
                    sre.EndSilenceTimeout = new TimeSpan(100000000);
                    sre.EndSilenceTimeoutAmbiguous = new TimeSpan(100000000);
                    RecognitionResult result = sre.Recognize(new TimeSpan(Int32.MaxValue));
                    return result.Text;
                }
                catch (Exception es)
                {

                    throw;
                }

                // WaveFileWriter.CreateWaveFile(outputFile, pcmStream);

            }
        }
        public static  string ReadAudio(string sourceFile)
        {
            SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
            Grammar gr = new DictationGrammar();
            sre.LoadGrammar(gr);
            sre.SetInputToWaveFile(sourceFile);
            sre.BabbleTimeout = new TimeSpan(Int32.MaxValue);
            sre.InitialSilenceTimeout = new TimeSpan(Int32.MaxValue);
            sre.EndSilenceTimeout = new TimeSpan(100000000);
            sre.EndSilenceTimeoutAmbiguous = new TimeSpan(100000000);
            RecognitionResult result = sre.Recognize(new TimeSpan(Int32.MaxValue));
            return result.Text;
        }
    }
}
