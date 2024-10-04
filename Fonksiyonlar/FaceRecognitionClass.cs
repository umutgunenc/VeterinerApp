using FaceRecognitionDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;
using Image = FaceRecognitionDotNet.Image;

namespace VeterinerApp.Fonksiyonlar
{

    //TODO daha hızlı çalıştır

    public class FaceRecognitionClass
    {
        private readonly FaceRecognition _faceRecognition;
        private const string ModelDirectory = "wwwroot/models";
        private const string ShapePredictorPath = "wwwroot/models/shape_predictor_68_face_landmarks.dat";
        private const string FaceRecognitionModelPath = "wwwroot/models/dlib_face_recognition_resnet_model_v1.dat";

        public List<FaceEncoding> ValidFacesEncodingList { get; set; }
        private List<Location> FaceLocations { get; set; }
        private Image Image { get; set; }

        public FaceRecognitionClass()
        {
            _faceRecognition = FaceRecognition.Create(ModelDirectory);
        }


        public async Task<(List<FaceEncoding>, bool)> DetectFacesAsync(IFormFile[] filePhotos)
        {
            ValidFacesEncodingList = new();
            FaceLocations = new();
            if (filePhotos.Length <= 10)
                return (ValidFacesEncodingList, false);

            foreach (var photo in filePhotos)
            {
                var memoryStream = new MemoryStream();
                await photo.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                // Resmi yükle ve bitmap'e dönüştür
                var bitmap = new Bitmap(memoryStream);
                Image = FaceRecognition.LoadImage(bitmap);

                // Yüz tespiti yap
                FaceLocations = _faceRecognition.FaceLocations(Image).ToList();

                foreach (var faceLocation in FaceLocations)
                {
                    var faceEncoding = _faceRecognition.FaceEncodings(Image, new[] { faceLocation }).FirstOrDefault();
                    if (faceEncoding != null)
                        ValidFacesEncodingList.Add(faceEncoding);

                }

            }
            if (ValidFacesEncodingList.Count <= 10)
                return (ValidFacesEncodingList, false);
            return (ValidFacesEncodingList, true);
        }

        public async Task<(bool, AppUser User)> CompareFaces(List<FaceEncoding> userFaceEncodingList, VeterinerDBContext context)
        {

            var dbFaceEncodingListByte = await context.UserFaces
                                                    .Select(uf => uf.FaceData)
                                                    .ToListAsync();

            var dbFaceEncodingList = new List<double[]>();
            foreach (var item in dbFaceEncodingListByte)
            {
                double[] encodingDouble = Convertor.ConvertByteArrayToDoubleArray(item);
                dbFaceEncodingList.Add(encodingDouble);
            }

            foreach (var dbFaceEncodig in dbFaceEncodingList)
            {
                var dbEncoding = FaceRecognition.LoadFaceEncoding(dbFaceEncodig);

                foreach (var userFaceEncoding in userFaceEncodingList)
                {
                    bool result = FaceRecognition.CompareFace(userFaceEncoding, dbEncoding, 0.5);
                    if (result)
                    {
                        byte[] faceData = Convertor.ConvertDoubleArrayToByteArray(dbFaceEncodig);

                        int userFaceId = await context.UserFaces
                                                    .Where(uf => uf.FaceData == faceData)
                                                    .Select(uf => uf.UserId)
                                                    .FirstOrDefaultAsync();
                        AppUser appUser = await context.AppUsers
                                                    .Where(au => au.Id == userFaceId)
                                                    .FirstOrDefaultAsync();
                        return (true, appUser);
                    }
                }
            }
            return (false, null);
        }

        public async Task SaveFaceEncodingToDatabaseAsync(int userId, FaceEncoding validFaceEncoding, VeterinerDBContext context)
        {
            var userFace = new UserFace
            {
                UserId = userId,
                // FaceEncoding vektörünü byte[] tipine çevir
                FaceData = Convertor.ConvertDoubleArrayToByteArray(validFaceEncoding.GetRawEncoding())
            };
            await context.UserFaces.AddAsync(userFace);
            await context.SaveChangesAsync();
        }
    }
}
