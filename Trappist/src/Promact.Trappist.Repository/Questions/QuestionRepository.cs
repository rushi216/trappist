using System.Collections.Generic;
using System.Linq;
using Promact.Trappist.DomainModel.DbContext;
using Promact.Trappist.DomainModel.ApplicationClasses.Question;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Promact.Trappist.DomainModel.Models.Question;
using Promact.Trappist.DomainModel.ApplicationClasses.SingleMultipleAnswerQuestionApplicationClass;
using Promact.Trappist.DomainModel.Enum;
using System;

namespace Promact.Trappist.Repository.Questions
{
    public class QuestionRepository : IQuestionRespository
    {
        private readonly TrappistDbContext _dbContext;
        public QuestionRepository(TrappistDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// Get all questions
        /// </summary>
        /// <returns>Question list</returns>
        public ICollection<SingleMultipleAnswerQuestionApplicationClass> GetAllQuestions()
        {
            var questions = _dbContext.SingleMultipleAnswerQuestion.ProjectTo<SingleMultipleAnswerQuestionApplicationClass>().ToList();
            questions.AddRange(_dbContext.CodeSnippetQuestion.ProjectTo<SingleMultipleAnswerQuestionApplicationClass>().ToList());
            var questionsOrderedByCreatedDateTime = questions.OrderBy(f => f.CreatedDateTime).ToList();
            return questionsOrderedByCreatedDateTime;
        }
        /// <summary>
        /// Add single multiple answer question into model
        /// </summary>
        /// <param name="singleMultipleAnswerQuestion"></param>
        /// <param name="singleMultipleAnswerQuestionOption"></param>
        public void AddSingleMultipleAnswerQuestion(SingleMultipleAnswerQuestion singleMultipleAnswerQuestion, List<SingleMultipleAnswerQuestionOption> singleMultipleAnswerQuestionOption)
        {
            _dbContext.SingleMultipleAnswerQuestion.Add(singleMultipleAnswerQuestion);
            foreach (SingleMultipleAnswerQuestionOption singleMultipleAnswerQuestionOptionElement in singleMultipleAnswerQuestionOption)
            {
                singleMultipleAnswerQuestionOptionElement.SingleMultipleAnswerQuestionID = singleMultipleAnswerQuestion.Id;
                _dbContext.SingleMultipleAnswerQuestionOption.Add(singleMultipleAnswerQuestionOptionElement);
            }
            _dbContext.SaveChanges();
        }
        /// <summary>
        /// Add new code snippet question to the database
        /// </summary>
        /// <param name="codeSnippetQuestion">Code Snippet Question Model</param>
        public async void AddCodeSnippetQuestion(CodeSnippetQuestionApplicationClass codeSnippetQuestionModel)
        {
            CodeSnippetQuestion codeSnippetQuestion = Mapper.Map<CodeSnippetQuestionApplicationClass, CodeSnippetQuestion>(codeSnippetQuestionModel);
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                var question = _dbContext.CodeSnippetQuestion.Add(codeSnippetQuestion);
                await _dbContext.SaveChangesAsync();
                var codingLanguageList = codeSnippetQuestionModel.LanguageList;
                var questionId = question.Entity.Id;
                foreach (var language in codingLanguageList)
                {
                    var languageId = _dbContext.CodingLanguage.Where(x => x.Language == language).Select(x => x.Id).FirstOrDefault();
                    _dbContext.QuestionLanguageMapping.Add(new QuestionLanguageMapping
                    {
                        QuestionId = questionId,
                        LanguageId = languageId
                    });
                }
                await _dbContext.SaveChangesAsync();
                transaction.Commit();
            }
        }

        public ICollection<CodingLanguageApplicationClass> GetAllCodingLanguages()
        {
            var codingLanguageList = new List<CodingLanguageApplicationClass>();
            var languageCodes = _dbContext.CodingLanguage.Select(x => x.Language).ToList();
            ProgrammingLanguage programmingLanguage;

            languageCodes.ForEach((language) =>
            {
                programmingLanguage = (ProgrammingLanguage)language;
                codingLanguageList.Add(new CodingLanguageApplicationClass()
                {
                    LanguageName = programmingLanguage.ToString(),
                    LanguageCode = (int)language
                });
            });

            return codingLanguageList;
        }
    }
}