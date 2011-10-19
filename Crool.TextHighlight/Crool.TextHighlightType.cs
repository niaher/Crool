using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Crool.TextHighlight
{
    internal static class Crool.TextHighlightClassificationDefinition
    {
        /// <summary>
        /// Defines the "Crool.TextHighlight" classification type.
        /// </summary>
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("Crool.TextHighlight")]
        internal static ClassificationTypeDefinition Crool.TextHighlightType = null;
    }
}
