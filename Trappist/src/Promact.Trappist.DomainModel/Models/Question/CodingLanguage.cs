﻿using Promact.Trappist.DomainModel.Enum;
using System.Collections.Generic;

namespace Promact.Trappist.DomainModel.Models.Question
{
    public class CodingLanguage: BaseModel
    { 
        public ProgrammingLanguage Language { get; set; }

        public virtual ICollection<QuestionLanguageMapping> QuestionLanguangeMapping { get; set; }
    }
}
