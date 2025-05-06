namespace ContosoAcai.Infrastructure.Azure.Speech;

public class SpeechTranscription
{
    public long Duration { get; set; }
    public List<CombinedPhrase> CombinedPhrases { get; set; } = new();
}

public class CombinedPhrase
{
    public int Channel { get; set; }
    public string Text { get; set; } = null!;
}