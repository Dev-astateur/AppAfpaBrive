using System;
using System.Collections.Generic;
using System.Linq;
using AppAfpaBrive.BOL;
using AppAfpaBrive.Web.Layers.StatsLayer;

namespace AppAfpaBrive.Web.Helpers.GraphicsDataHelpers
{
    public class GetData
    {

        public static List<IInsertion> GetGraphData(string filterMois="", string filterYear = "", string filterEtablissement = "", string filterOffreFormation = "")
        {
            GetInsertionDataLayer data = new GetInsertionDataLayer(new AppAfpaBrive.DAL.AFPANADbContext());
            List<IInsertion> datas = GetDatasMois(filterMois, data);
            if (filterYear != "") datas = data.GetOneYearData(datas, Int32.Parse(filterYear));
            if (filterEtablissement != "") datas = data.GetOneEtablissementData(datas, filterEtablissement);
            if (filterOffreFormation != "") datas = data.GetOneFormationData(datas, Int32.Parse(filterOffreFormation));
            return datas;
        }

        private static List<IInsertion> GetDatasMois(string filter, GetInsertionDataLayer data)
        {
            List<IInsertion> datas = new List<IInsertion>();
            switch (filter)
            {
                case "1":
                    datas = data.GetAllInsertion();
                    break;
                case "3":
                    datas = data.GetInsertionTroisMois();
                    break;
                case "6":
                    datas = data.GetInsertionSixMois();
                    break;
                case "12":
                    datas = data.GetInsertionDouzeMois();
                    break;
                default:
                    throw new Exception("Le graphique demandé n'existe pas.");
            }
            return datas;
        }

        public static List<EtablissementAutoComplete> GetEtablissementData(string filter)
        {
            AutoCompleteDataLayer data = new AutoCompleteDataLayer(new AppAfpaBrive.DAL.AFPANADbContext());
            List<EtablissementAutoComplete> datas = data.GetAllEtablissementData();
            if(filter != "") datas = datas.Where(x => x.NomEtablissement.ToLower().Contains(filter.ToLower())).ToList();
            return datas;
        }

        public static List<OffreFormationAutoComplete> GetOffreFormationData(string filter)
        {
            AutoCompleteDataLayer data = new AutoCompleteDataLayer(new AppAfpaBrive.DAL.AFPANADbContext());
            return data.GetAllOffreFormationData().Where(x => x.NomOffreFormation.ToLower().Contains(filter.ToLower())).ToList();
        }



    }
}
