using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bliss.Data;
using Bliss.Data.Provider;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

public class MongoDataProviderTests
{
    private MongoDataProvider _provider;
    private IDatabaseSettings settings = new DatabaseSettings();
    
    [SetUp]
    public void Setup(){
        IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).
        AddJsonFile("appsettings.json").Build();
        configuration.GetSection(nameof(DatabaseSettings)).Bind(settings);

        _provider = new MongoDataProvider(settings);
    }

    [TearDown]
    public async Task TearDown(){
        await _provider.DropCollection();
    }

    [Test]
    public async Task PostShouldSaveQuestion(){
        SavedQuestion question = new SavedQuestion(){
                                        Choices = new[]{ new ChoicesVotes(){Choice = Choices.ObjectiveC, Votes = 0}} , 
                                        ImageUrl="", 
                                        QuestionText="Sample Question", 
                                        ThumbUrl= ""};

        var result = await _provider.Save(question);

        Assert.NotNull(result);
    }

    [Test]
    public async Task GetByIdShouldGetQuestion()
    {
        SavedQuestion question = new SavedQuestion()
        {
            Choices = new[] { new ChoicesVotes() { Choice = Choices.ObjectiveC, Votes = 0 } },
            ImageUrl = "",
            QuestionText = "Sample Question",
            ThumbUrl = ""
        };

        var result = await _provider.Save(question);

        var savedQuestion = await _provider.GetById(result);

        QuestionsAreEqual(question, savedQuestion);
    }

    [Test]
    public async Task PostShouldUpdateQuestion(){
        const string NewQuestion = "Updated Sample Question";
        SavedQuestion question = new SavedQuestion(){
                                        Choices = new[]{ new ChoicesVotes(){Choice = Choices.ObjectiveC, Votes = 0}} , 
                                        ImageUrl="", 
                                        QuestionText="Sample Question", 
                                        ThumbUrl= ""};

        var result = await _provider.Save(question);

        Assert.NotNull(result);

        question.QuestionText = NewQuestion;

        var newResult = await _provider.Save(question);

        var savedQuestion = await _provider.GetById(newResult);

        Assert.AreEqual(result, newResult);
        Assert.AreEqual(NewQuestion, savedQuestion.QuestionText);
    }

[   Test]
    public async Task GetShouldRetrieveAllQuestions()
    {
        await SetupTestQuestions();

        var result = await _provider.Get(-1, 0, null);

        Assert.AreEqual(4, result.Count);

    }

     [Test]
    public async Task GetShouldRetrieveFilteredQuestionsByQuestionName()
    {
        await SetupTestQuestions();

        var result = await _provider.Get(-1, 0, "Filter");

        Assert.AreEqual(2, result.Count);

    }

    [Test]
    public async Task GetShouldRetrieveFilteredQuestionsByChoiceName()
    {
        await SetupTestQuestions();

        var result = await _provider.Get(-1, 0, Choices.Ruby);

        Assert.AreEqual(1, result.Count);

    }

    [Test]
    public async Task GetShouldRetrieveLimitFilteredQuestions()
    {
        await SetupTestQuestions();

        var result = await _provider.Get(1, 0, "Filter");

        Assert.AreEqual(1, result.Count);

    }

    [Test]
    public async Task GetShouldRetrieveOffsetFilteredQuestions()
    {
        await SetupTestQuestions();

        var result = await _provider.Get(-1, 1, "Filter");

        Assert.AreEqual(1, result.Count);

    }

    [Test]
    public async Task GetShouldIgnoreLimitIfExceedsTotalQuestions()
    {
        await SetupTestQuestions();

        var result = await _provider.Get(5, 0, null);

        Assert.AreEqual(4, result.Count);
    }

     [Test]
    public async Task GetShouldIgnoreOffsetIfExceedsTotalQuestions()
    {
        await SetupTestQuestions();

        var result = await _provider.Get(-1, 5, null);

        Assert.AreEqual(4, result.Count);
    }

    private async Task SetupTestQuestions()
    {
        SavedQuestion question = new SavedQuestion()
        {
            Choices = new[] { new ChoicesVotes() { Choice = Choices.ObjectiveC, Votes = 0 } },
            ImageUrl = "",
            QuestionText = "Sample Question",
            ThumbUrl = ""
        };

        await _provider.Save(question);
        question.Id = null;
        await _provider.Save(question);
        question.Id = null;
        question.QuestionText = "Filter Question";
        await _provider.Save(question);
        question.Id = null;
        question.QuestionText = "Filter Question";
        question.Choices = new [] { new ChoicesVotes(){ Choice = Choices.Ruby, Votes = 0}};
        await _provider.Save(question);
    }

    private static void QuestionsAreEqual(SavedQuestion original, SavedQuestion retrieved)
    {
        Assert.AreEqual(original.Id, retrieved.Id);
        Assert.AreEqual(original.ImageUrl, retrieved.ImageUrl);
        Assert.AreEqual(original.ThumbUrl, retrieved.ThumbUrl);
        Assert.AreEqual(original.Choices.First().Choice, retrieved.Choices.First().Choice);
        Assert.AreEqual(original.Choices.First().Votes, retrieved.Choices.First().Votes);
    }

}