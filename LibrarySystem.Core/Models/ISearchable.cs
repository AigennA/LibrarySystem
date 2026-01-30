namespace LibrarySystem.Core.Models
{
    /// <summary>
    /// Interface för sökbar funktionalitet. Möjliggör polymorfisk sökning
    /// över olika entitetstyper (t.ex. böcker och medlemmar).
    /// </summary>
    public interface ISearchable
    {
        /// <summary>
        /// Kontrollerar om objektet matchar den angivna söktermen.
        /// </summary>
        /// <param name="searchTerm">Söktermen att matcha mot.</param>
        /// <returns>True om objektet matchar söktermen, annars false.</returns>
        bool Matches(string searchTerm);
    }
}
