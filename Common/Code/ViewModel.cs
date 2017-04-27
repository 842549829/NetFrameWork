namespace Common.Code
{
    /// <summary>
    /// ViewModel
    /// </summary>
    /// <typeparam name="C">Condition</typeparam>
    /// <typeparam name="D">Data</typeparam>
    public class ViewModel<C, D>
    {
        /// <summary>
        /// Condition
        /// </summary>
        public C Condition { get; set; }

        /// <summary>
        /// Data
        /// </summary>
        public D Data { get; set; }
    }
}