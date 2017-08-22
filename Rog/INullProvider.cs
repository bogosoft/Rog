namespace Rog
{
    /// <summary>
    /// Indicates that an implementation can provider null values.
    /// </summary>
    public interface INullProvider
    {
        /// <summary>
        /// Get or set the percentage chance that a null can be provided.
        /// </summary>
        Percentage NullChance { get; set; }
    }
}