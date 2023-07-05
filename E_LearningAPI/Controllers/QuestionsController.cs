using E_Learning_Library.DataTransferObjects;
using E_Learning_Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_LearningAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuestionsController : ControllerBase
{
    private readonly ELearningDbContext _context;

    public QuestionsController(ELearningDbContext context)
    {
        _context = context;
    }

    // GET: api/Questions
    [HttpGet]
    public ActionResult<IEnumerable<Question>> Get()
    {
        var questions = _context.Questions
            .Include(a => a.Answers)
            .ToList();
        return Ok(questions);
    }

    // GET api/Questions/final 
    [HttpGet("final")]
    public ActionResult<IEnumerable<Question>> GetFinalQuizQuestions()
    {
        // Γεμίζω μία λίστα με μόνο τα distinct QuizID
        var distinctQuizIds = _context.Questions
            .Select(q => q.QuizId)
            .Distinct()
            .ToList();

        // Αρχικοποιώ την τελική λίστα που θα στείλω μέσω αυτού του Endpoint
        List<Question> finalQuzQuestions = new List<Question>();

        // Κάποιες απαραίτητες αρχικοποιήσεις
        Random rnd = new Random();
        int num;
        int index;

        foreach (var q in distinctQuizIds)
        {
            // Τυχαίος αριθμός απο το 1 έως το 3
            num = rnd.Next(1,4);

            // Καλώ απο τη βάση μόνο όσα Questions έχουν ως Quiz Id το τωρινό 
            // εξεταζόμενο (δηλαδή το q) και γεμίζω αυτή τη προσωρινή λίστα
            var questionsToAdd = _context.Questions
                .Where(qid => qid.QuizId.Equals(q))
                .Include(a => a.Answers)
                .ToList();

            // Τρέχω ένα forLoop τόσες φόρές όσο ο τυχαίος αριθμός που βρήκα πριν (το num)
            for (int i = 0; i < num; i++)
            {
                // Διαλέγω έναν τυχαίο αριθμό Από το 1 μέχρι το Χ,
                // Όπου το Χ = το μέγεθος της λίστας με τα Questions που
                // έχουν ίδιο Quiz ID
                index = rnd.Next(questionsToAdd.Count);

                // Προσθέτω στην τελική λίστα το τυχαίο αντικείμενο της
                // προσωρινής λίστας που διάλεξα μέσω της μεταβλητής index
                finalQuzQuestions.Add(questionsToAdd[index]);

                // Το αφαιρώ απο την προσωρινή λίστα για να μην μπορώ να το ξανα-επιλέξω
                questionsToAdd.Remove(questionsToAdd[index]);
            }
        }

        // Για λόγους testing
        Console.WriteLine(finalQuzQuestions.Count);

        //Κάνω Randomize την τελική λίστα πρωτού τη στείλω
        for (int i = finalQuzQuestions.Count - 1; i > 0; i--)
        {
            var k = rnd.Next(i + 1);
            var value = finalQuzQuestions[k];
            finalQuzQuestions[k] = finalQuzQuestions[i];
            finalQuzQuestions[i] = value;
        }

        // Στέλνω μέσω αυτού του Endpoint την τελική τυχαία λίστα
        return Ok(finalQuzQuestions);
    }

    // GET api/Questions/5
    [HttpGet("{id}")]
    public ActionResult<Question> Get(int id)
    {
        // TODO: Προσθέτω τα Include() Που έχει και από πάνω
        var question = _context.Questions
            .SingleOrDefault(x => x.QuestionId == id);

        if (question is null)
        {
            return NotFound("Question Not Found");
        }
        else
        {
            return Ok(question);
        }
    }

    // GET api/Questions/someQuestions/5
    [HttpGet("someQuestions/{id}")]
    public ActionResult<Question> GetSomeQuestions(int id)
    {
        var question = _context.Questions
            .Where(i => i.QuizId == id)
            .Include(a => a.Answers);

        if (question is null)
        {
            return NotFound("Question Not Found");
        }
        else
        {
            return Ok(question);
        }
    }

    // POST api/Questions
    [HttpPost]
    public ActionResult Post(QuestionCreateDTO questionCreateDTO)
    {
        var quiz = _context.Quizzes
            .Where(q => q.QuizId == questionCreateDTO.QuizId)
            .FirstOrDefault();

        if (quiz is null)
        {
            return BadRequest();
        }
        else
        {
            Question question = new Question();
            question.QuizId = questionCreateDTO.QuizId;
            question.QuestionText = questionCreateDTO.QuestionText;
            //question.Quiz = quiz;

            _context.Questions.Add(question);
            _context.SaveChanges();

            return Ok(question);
        }
    }

    // PUT api/Questions/5
    [HttpPut("{id}")]
    public ActionResult Put(int id, QuestionEditDTO questionEditDTO)
    {
        var question = _context.Questions.Find(id);

        if (question != null)
        {
            try
            {
                _context.Questions
                .Where(i => i.QuestionId == id)
                .ExecuteUpdate(s => s
                    .SetProperty(t => t.QuestionText, t => questionEditDTO.QuestionText)
                );

                return Ok(questionEditDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }
        else
        {
            return BadRequest();
        }
    }

    // DELETE api/Questions/5
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var question = _context.Questions.Find(id);

        if (question == null)
        {
            return NotFound("Question Not Found");
        }
        else
        {
            _context.Questions
                .Where(i => i.QuestionId.Equals(id))
                .ExecuteDelete();

            return Ok("Deleted Successfully");
        }
    }
}
