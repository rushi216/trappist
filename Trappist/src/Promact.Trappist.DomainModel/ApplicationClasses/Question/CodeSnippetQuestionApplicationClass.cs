using Promact.Trappist.DomainModel.Enum;
using Promact.Trappist.DomainModel.Models.Question;
using System.Collections.Generic;

namespace Promact.Trappist.DomainModel.ApplicationClasses.Question
{
    /// <summary>
    ///Application class for code snippet question 
    /// </summary>
    public class CodeSnippetQuestionApplicationClass : CodeSnippetQuestion
    {
        public ICollection<ProgrammingLanguage> LanguageList { get; set; }
    }
}
