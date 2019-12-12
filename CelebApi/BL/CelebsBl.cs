using CelebApi.DAL;
using Newtonsoft.Json.Linq;

namespace CelebApi.BL
{
    public class CelebsBl
    {
        static CelebsDal dal = new CelebsDal();
        public string CreateJsonFile()
        {
            return dal.CreateJsonFile();
        }

        public string RemoveRecord(int id)
        {
            return dal.RemoveRecord(id);
        }

        public string AddCeleb(JObject celeb)
        {
            return dal.AddRecord(celeb);
        }

        public string UpdateCeleb(int id,JObject celeb)
        {
            return dal.UpdateRecord(id,celeb);
        }

    }
}