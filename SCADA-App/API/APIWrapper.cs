using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_App.API
{
    class APIWrapper
    {
        #region GetLampen

        public static async Task<APIResponseLampen> GetLampen()
        {
            APIResponseLampen ResponseLampen = null;

            string sUrl = "https://api.summa.1ku.nl/examen/178723/lampen.php";

            try
            {
                using (HttpClient hcClient = new HttpClient())
                {
                    //JSON ophalen uit string.
                    string sJson = await hcClient.GetStringAsync(sUrl);

                    ResponseLampen = JsonConvert.DeserializeObject<APIResponseLampen>(sJson);
                }
            }
            catch (Exception)
            {
                // DOE NIKS;
            }
            return ResponseLampen;
        }
        #endregion

        #region GetSproeiers

        public static async Task<APIResponseSproeiers> GetSproeiers()
        {
            APIResponseSproeiers ResponseSproeiers = null;

            string sUrl = "https://api.summa.1ku.nl/examen/178723/sproeiers.php";

            try
            {
                using (HttpClient hcClient = new HttpClient())
                {
                    //JSON ophalen uit string.
                    string sJson = await hcClient.GetStringAsync(sUrl);

                    ResponseSproeiers = JsonConvert.DeserializeObject<APIResponseSproeiers>(sJson);
                }
            }
            catch (Exception)
            {
                // DOE NIKS;
            }
            return ResponseSproeiers;
        }

        #endregion

        #region GetIndividualLamp

        public static async Task<APIResponseLampen> GetIndividualLamp(int iTeeltbed, string sHoog, string sLaag)
        {
            APIResponseLampen ResponseLampenIndividual = null;

            string sUrl = "https://api.summa.1ku.nl/examen/178723/lampen.php?teeltbed={iTeeltbed}&hoog={sHoog}&laag={sLaag}";
            try
            {
                using (HttpClient hcClient = new HttpClient())
                {
                    //JSON ophalen uit string.
                    string sJson = await hcClient.GetStringAsync(sUrl);

                    ResponseLampenIndividual = JsonConvert.DeserializeObject<APIResponseLampen>(sJson);
                }

            }
            catch (Exception)
            {
                // DOE NIKS;
            }
            return ResponseLampenIndividual;
        }

        #endregion
    }
}
