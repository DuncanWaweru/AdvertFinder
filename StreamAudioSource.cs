using NAudio.Wave;

namespace AudioProcessor
{
    internal class StreamAudioSource
    {
        private WaveFormatConversionStream resampledStream;

        public StreamAudioSource(WaveFormatConversionStream resampledStream)
        {
            this.resampledStream = resampledStream;
        }
    }
}