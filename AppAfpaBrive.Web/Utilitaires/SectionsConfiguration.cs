using System;
using System.Collections.Generic;
using System.Text;

namespace AppAfpaBrive.Web.Utilitaires
{

    public class MailerSettings
    {
        public string NameFrom { get; set; }
        public string MailFrom { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpRelay { get; set; }
        public string ServiceApiKey { get; set; }
        public string Credentials_Id { get; set; }
        public string Credentials_Pw { get; set; }
    }
    public class ImportDataOffreBeneficiaires
    {
        public string ListeColonnes { get; set; }
    }
    public class AdministrateurSettings
    {
        public string UserName { get; set; }
        public string Mail { get; set; }
        public string InitialPassWord { get; set; }
    }
    public class RolesSettings
    {
        public string Liste { get; set; }
    }
}
