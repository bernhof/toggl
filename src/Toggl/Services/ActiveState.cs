namespace Toggl.Services
{
    /// <summary>
    /// Item state as specified in query criteria
    /// </summary>
    public enum ActiveState
    {
        /// <summary>
        /// Active items
        /// </summary>
        Active,
        /// <summary>
        /// Archived items
        /// </summary>
        Archived,
        /// <summary>
        /// Both active and archived items
        /// </summary>
        Both
    }
}
