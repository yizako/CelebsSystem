using CelebApi.BL;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Web.Http;

namespace CelebApi.Controllers
{
    public class CelebController : ApiController
    {
        static CelebsBl bl = new CelebsBl();

        [Route("api/celebs")]
        public string Get()
        {
            return bl.CreateJsonFile();
        }

        [Route("api/celebs/reset")]
        [HttpGet]
        public string ResetAll()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"Celebs.json";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            return bl.CreateJsonFile();
        }

        [Route("api/celeb/add")]
        [HttpPost]
        public string Add(JObject celeb)
        {
            return bl.AddCeleb(celeb);
        }

        [Route("api/celeb/update")]
        public string Put(int id, JObject celeb)
        {
            return bl.UpdateCeleb(id, celeb);
        }

        [Route("api/celeb/remove/{id}")]
        [HttpGet]

        public string Remove(int id)
        {
            return bl.RemoveRecord(id);
        }


    }
}
