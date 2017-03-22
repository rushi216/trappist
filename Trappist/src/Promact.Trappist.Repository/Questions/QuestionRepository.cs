using System.Collections.Generic;
using System.Linq;
using Promact.Trappist.DomainModel.DbContext;
using Promact.Trappist.DomainModel.ApplicationClasses.Question;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Promact.Trappist.DomainModel.Models.Question;
using Promact.Trappist.DomainModel.ApplicationClasses.SingleMultipleAnswerQuestionAC;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Promact.Trappist.Repository.Questions
{
    public class QuestionRepository : IQuestionRespository
    {
        private readonly TrappistDbContext _dbContext;
        public QuestionRepository(TrappistDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #region GetAllQuestions
        /// <summary>
        ///The undermentioned method fetches all the questions from the database
        /// </summary>
        /// <returns>Question list</returns>
        public async Task<ICollection<SingleMultipleAnswerQuestionAC>> GetAllQuestions()
        {
            var questions = await _dbContext.Question.ProjectTo<SingleMultipleAnswerQuestionAC>().ToListAsync();

            foreach (var question in questions)
            {
                var singleMultipleAnswerQuestionId =await _dbContext.SingleMultipleAnswerQuestion.Where(x => x.QuestionId == question.Id).Select(x => x.Id).FirstOrDefaultAsync();
                question.SingleMultipleAnswerQuestionOption = await _dbContext.SingleMultipleAnswerQuestionOption.Where(x => x.SingleMultipleAnswerQuestionID == singleMultipleAnswerQuestionId).ToListAsync();
            }
            var questionsOrderedByCreatedDateTime = questions.OrderBy(f => f.CreatedDateTime).ToList();
            return questionsOrderedByCreatedDateTime;
        }
        #endregion
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
                //To-Do Change according to new model singleMultipleAnswerQuestionOptionElement.SingleMultipleAnswerQuestionID = singleMultipleAnswerQuestion.Id;
                _dbContext.SingleMultipleAnswerQuestionOption.Add(singleMultipleAnswerQuestionOptionElement);
            }
            _dbContext.SaveChanges();
        }
        /// <summary>
        /// Add new code snippet question to the database
        /// </summary>
        /// <param name="codeSnippetQuestion">Code Snippet Question Model</param>
        public void AddCodeSnippetQuestion(CodeSnippetQuestionDto codeSnippetQuestionModel)
        {
            CodeSnippetQuestion codeSnippetQuestion = Mapper.Map<CodeSnippetQuestionDto, CodeSnippetQuestion>(codeSnippetQuestionModel);
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                var question = _dbContext.CodeSnippetQuestion.Add(codeSnippetQuestion);
                _dbContext.SaveChanges();
                var codingLanguageList = codeSnippetQuestionModel.LanguageList;
                //To-Do Change according to new model var questionId = question.Entity.Id; 
                foreach (var language in codingLanguageList)
                {
                    var languageId = _dbContext.CodingLanguage.Where(x => x.Language == language).Select(x => x.Id).FirstOrDefault();
                    _dbContext.QuestionLanguageMapping.Add(new QuestionLanguageMapping
                    {
                        //To-Do Change according to new model QuestionId = questionId,
                        LanguageId = languageId
                    });
                }
                _dbContext.SaveChanges();
                transaction.Commit();
            }
        }
    }
}