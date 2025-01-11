using System.Text;

namespace Raiqub.T4Template
{
    /// <summary>Base class for T4 templates.</summary>
    /// <typeparam name="T">The type of the model associated with the template.</typeparam>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Raiqub.T4Template", "1.0.0.0")]
    public abstract class T4TemplateBase<T> : T4TemplateBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T4TemplateBase{T}"/> class.
        /// </summary>
        /// <param name="charsPerIndentation">The number of characters per indentation level.</param>
        protected T4TemplateBase(int charsPerIndentation = CharsPerIndentation)
            : base(charsPerIndentation)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T4TemplateBase{T}"/> class with a <see cref="StringBuilder"/>.
        /// </summary>
        /// <param name="builder">The StringBuilder object used for building the template.</param>
        /// <param name="charsPerIndentation">The number of characters per indentation level.</param>
        protected T4TemplateBase(StringBuilder builder, int charsPerIndentation = CharsPerIndentation)
            : base(builder, charsPerIndentation)
        {
        }

        /// <summary>Gets or sets the model associated with the template.</summary>
        public T Model { get; protected set; } = default!;
    }
}
