﻿using Shared.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyModule.Domain.Entities
{
    public class Appointment : BaseEntity
    {
        public string Description { get; set; }
        public Timeslot Timeslot { get; set; }
        public Company Company { get; set; }
        public List<Attachment> Attachments { get; set; } = new List<Attachment>();

        public Appointment() {}
    }
}
