﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadExpenceTracker.Persistence.MongoDB.Model
{
    public class ExpenceMongo
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public DateTime Date { get; set; }
        public required string ExpenceType { get; set; }
        public long Amount { get; set; }
    }
}
