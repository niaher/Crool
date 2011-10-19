using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Crool.TextHighlight
{
    #region Format definition
    /// <summary>
    /// Defines an editor format for the Crool.TextHighlight type that has a purple background
    /// and is underlined.
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "Crool.TextHighlight")]
    [Name("Crool.TextHighlight")]
    [UserVisible(true)] //this should be visible to the end user
    [Order(Before = Priority.Default)] //set the priority to be after the default classifiers
    internal sealed class Crool.TextHighlightFormat : ClassificationFormatDefinition
    {
        /// <summary>
        /// Defines the visual format for the "Crool.TextHighlight" classification type
        /// </summary>
        public Crool.TextHighlightFormat()
        {
            this.DisplayName = "Crool.TextHighlight"; //human readable version of the name
            this.BackgroundColor = Colors.BlueViolet;
            this.TextDecorations = System.Windows.TextDecorations.Underline;
        }
    }
    #endregion //Format definition
}
