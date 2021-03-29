using AppAfpaBrive.Web.CustomValidator;
using System;
using System.ComponentModel.DataAnnotations;

namespace AppAfpaBrive.Web.ModelView
{
    public class Date_ModelView
    {
        private int _Iddate;
        private DateTime _date1;
        private DateTime _date2;

        [Key]
        public int Iddate { get => _Iddate; set => _Iddate = value; }

        [Required]
        [DateStart(ErrorMessage = "La date doit être superieur à aujourd'hui")]
        public DateTime Date1 { get => _date1; set => _date1 = value; }
        

        [Required]
        [DateStart(ErrorMessage ="La date doit être superieur à aujourd'hui")]
        public DateTime Date2 { get => _date2; set => _date2 = value; }
    }

}
