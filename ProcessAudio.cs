using SoundFingerprinting;
using SoundFingerprinting.Audio;
using SoundFingerprinting.Audio.NAudio;
using SoundFingerprinting.Builder;
using SoundFingerprinting.DAO.Data;
using SoundFingerprinting.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioProcessor
{
    public class ProcessAudio
    {
        private readonly IModelService modelService = new SqlModelService(); // SQL back end
        private readonly IAudioService audioService = new NAudioService(); // use NAudio audio processing library
        private readonly IFingerprintCommandBuilder fingerprintCommandBuilder = new FingerprintCommandBuilder();

        public void StoreAudioFileFingerprintsInStorageForLaterRetrieval(string pathToAudioFile)
        {
            TrackData track = new TrackData(Guid.NewGuid().ToString(), "Adele", "Skyfall", "Skyfall", 2012, 290);

            // store track metadata in the database
            var trackReference = modelService.InsertTrack(track);
            
            // create sub-fingerprints and its hash representation
            var hashedFingerprints = fingerprintCommandBuilder
                                        .BuildFingerprintCommand()
                                        .From(pathToAudioFile)
                                        .UsingServices(audioService)
                                        .Hash()
                                        .Result;

            // store sub-fingerprints and its hash representation in the database 
            modelService.InsertHashDataForTrack(hashedFingerprints, trackReference); // insert in SQL backend
        }
        public async Task<string> matchAudioAsync(string pathToAudioFile)
        {
            var trackReference = modelService.ReadAllTracks();

            var queryResult = await QueryCommandBuilder.Instance
                                             .BuildQueryCommand()
                                             .From(pathToAudioFile)
                                             .UsingServices(modelService, audioService)
                                              .Query();
            if (queryResult.ContainsMatches)
            {
                var result = queryResult.ResultEntries.ToArray().ToString();
                return result;
            }
            else
            {
                return "no match";
            }
        }
    }
}
