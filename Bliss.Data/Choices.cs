using System;

public enum Choices{ Swift, Python, ObjectiveC, Ruby }

public class ChoicesVotes {
    public Choices Choice {get;set;}
    public int Votes { get; set;}
}