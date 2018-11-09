using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;

namespace KamiJal.CoupleMatch.Api
{
    public class FaceApiUser
    {
        private const string FaceEndpoint = "https://westcentralus.api.cognitive.microsoft.com";
        private const string ApiKey = "{YOUR_FACE_API_TOKEN}";
        private readonly FaceAttributeType[] _faceAttributes = {FaceAttributeType.Age, FaceAttributeType.Gender};

        private readonly FaceClient _faceClient;

        public FaceApiUser()
        {
            _faceClient = new FaceClient(
                new ApiKeyServiceClientCredentials(ApiKey))
            {
                Endpoint = FaceEndpoint
            };
        }

        public async Task<DetectedFace> MakeAnalysisRequest(string url)
        {
            var faces = await _faceClient.Face.DetectWithUrlAsync(url, false, false, _faceAttributes);

            if (faces == null || !faces.Any() || faces.Count > 1)
                throw new APIErrorException();

            return faces.Single();
        }
    }
}