﻿using System;
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
        public DateTime Date1 { get => _date1; set => _date1 = value; }
        

        [Required]
        public DateTime Date2 { get => _date2; set => _date2 = value; }
    }

}